using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;




public class MotorBikeControl : MonoBehaviour
{

    public enum SpeedMultiplierType
    {
        Near,
        Boost
    }
    private float DirectionSpeed = 1;
    private float SpeedMultiplier = 1;

    private float CurrentSpeed = 0;
    private float TargetSpeed = 0;
    private float SpeedSmoothVel = 0;

    private float CurrentDirection = 0;
    private float TargetDirection = 0;
    private float DirectionSmoothVel = 0;

    private float CurrentAngle = 0;
    private float TargetAngle = 0;
    private float AngleSmoothVel = 0;

    private float CurrentAcceleration = 0;
    private float TargetAcceleration = 0;
    private float AccelerationSmoothVel = 0;

    bool IsInitilaized = false;
    bool IsMoving = false;
    private bool IsBot = false;


    private Rigidbody MotorRigidbody;
    private Transform BodyParent;
    private MotorModelController MotorModel;

    [SerializeField] private GameObject[] RagdollComponents;
    private List<Vector3> RagdollPositions = new List<Vector3>();
    [SerializeField] private Transform BotInfo;
    [SerializeField] private BotInfoControl BotInfoController;

    private Vector3 MotorPosition;

    public UnityAction CrashStarted;
    public UnityAction CrashEnded;

    void Start()
    {

    }

    public void Init(bool isBot,MotorType playerMotorType)
    {
        IsBot = isBot;
        DirectionSpeed = Globals.Instance.GetCarDirectionSpeed();
        SetRagdollStatus(false);
        SaveRagdollPosition();
        MotorModel = transform.GetChild(0).GetChild(0).GetComponent<MotorModelController>();
        MotorRigidbody = transform.GetComponent<Rigidbody>();
        BodyParent = transform.GetChild(0);

        MotorType motorType = playerMotorType;
        Color selectedColor = Color.red;
        if(isBot)
        {
            motorType = (MotorType)Random.Range(0,Globals.Instance.GetMotorCount()); 
            selectedColor = new Color(Random.Range(0f,1f),Random.Range(0f,1f),Random.Range(0f,1f));
        }
        MotorModel.SetSelectedMotor(motorType, !isBot, selectedColor);

        IsMoving = true;
        IsInitilaized = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if(IsInitilaized && IsMoving)
        {
            MotorPosition = transform.position;
            Movement();
            if(IsBot)
            {
                BotInfo.position = MotorPosition;
                BotInfoController.SetBotOrder(GameSystem.Instance.GetMotorOrder(MotorPosition.z).ToString());
            }
        }
        
    }

    void Movement()
    {
        CurrentSpeed = Mathf.SmoothDamp(CurrentSpeed, TargetSpeed, ref SpeedSmoothVel, 0.5f);
        CurrentDirection = Mathf.SmoothDamp(CurrentDirection, TargetDirection, ref DirectionSmoothVel, 0.1f);
        CurrentAngle = Mathf.SmoothDamp(CurrentAngle, TargetAngle, ref AngleSmoothVel, 0.5f);
        CurrentAcceleration = Mathf.SmoothDamp(CurrentAcceleration, TargetAcceleration, ref AccelerationSmoothVel, 0.2f);

        MotorRigidbody.AddForce((Vector3.forward * CurrentSpeed * Time.deltaTime) + (Vector3.right * CurrentDirection * DirectionSpeed * Time.deltaTime),ForceMode.Force);
        //MotorRigidbody.velocity = (Vector3.forward * CurrentSpeed * Time.deltaTime) + (Vector3.right * CurrentDirection * Time.deltaTime);

        BodyParent.localRotation = Quaternion.Euler(0, 0, -CurrentAngle * 12);
        
        MotorModel.SetDirection(CurrentAngle);
        MotorModel.SetAcceleration(CurrentAcceleration);

    }

    public void SetSpeed(float newSpeed)
    {
        TargetSpeed = newSpeed;
    }


    float timer = 0;
    public void SetDirection(float direction)
    {
        TargetDirection = direction;

        TargetAngle = Mathf.Sign(direction);
        if(Mathf.Abs(direction)  < 0.05f)
        {
            timer += 0.00015f;
            Vector3 velocity = MotorRigidbody.velocity;
            float newVelocity = Mathf.Lerp(velocity.x, 0, timer);

            velocity.x = newVelocity;

            if(timer > 1) timer = 0;

            MotorRigidbody.velocity = velocity;
            TargetDirection = 0;
            TargetAngle = 0;  
        } 
    }

    public void SetAcceleration(float newAcceleration)
    {
        TargetAcceleration = newAcceleration;
    }



    public void SetSpeedMultiplier(SpeedMultiplierType multiplier)
    {
        CancelInvoke("ResetSpeedMultiplier");

        switch(multiplier)
        {
            case SpeedMultiplierType.Near:
                SpeedMultiplier = 1.5f;
                break;
            case SpeedMultiplierType.Boost:
                SpeedMultiplier = 2f;
                break;
        }

        Invoke("ResetSpeedMultiplier",Globals.Instance.GetBoostTime());
    }

    void ResetSpeedMultiplier()
    {
        SpeedMultiplier = 1;
    }

    /*
    public void SetSelectedMotorBody(int body)
    {
        int motorCount = BodyParent.childCount - 1;

        for(int i=0; i<motorCount; i++)
        {
            if(i == body)
            {
                BodyParent.GetChild(i).gameObject.SetActive(true);
            }
            else
            {
                BodyParent.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
    */

    void OnCollisionEnter(Collision collision)
    {

        if(collision.transform.name.Contains("Car") && IsMoving)
        {
            StartCrash(collision.contacts[0].point);
        }
    }

    void StartCrash(Vector3 crashPos)
    {
        if(CrashStarted != null) CrashStarted();
        IsMoving = false;
        MotorModel.SetAnimatorStatus(false);

        SetRagdollStatus(true);
        AddRagdollForce(crashPos);

        if(!IsBot) MotorModel.SetHandBodyStatus(false);
        Invoke("ResetCrash",5);

    }

    void ResetCrash()
    {
        
        IsMoving = true;
        MotorModel.SetAnimatorStatus(true);
        SetRagdollStatus(false);
        ResetRagdollPosition();
        if(!IsBot) MotorModel.SetHandBodyStatus(true);
        if(CrashStarted != null) CrashEnded();
    }


    void SaveRagdollPosition()
    {
        for(int i=0; i<RagdollComponents.Length; i++)
        {
            RagdollPositions.Add(RagdollComponents[i].transform.position);
        }

    }

    void ResetRagdollPosition()
    {
        for(int i=0; i<RagdollComponents.Length; i++)
        {
            RagdollComponents[i].transform.position = RagdollPositions[i];
        }
    }


    public void SetRagdollStatus(bool status)
    {
        foreach(GameObject comps in RagdollComponents)
        {
            comps.GetComponent<Collider>().enabled = status;
            comps.GetComponent<Rigidbody>().isKinematic  = !status;

            if(comps.GetComponent<CharacterJoint>() != null) comps.GetComponent<CharacterJoint>().enableCollision = status;
            
            if(status)
            {
                comps.GetComponent<Rigidbody>().AddForce(Vector3.up * 8,ForceMode.Impulse);
            }
        }
    }

    void AddRagdollForce(Vector3 contactPoint)
    {
        foreach(GameObject comps in RagdollComponents)
        {
            comps.GetComponent<Rigidbody>().AddForce((transform.position - contactPoint).normalized * 8,ForceMode.Impulse);
        }  
    }

    public Vector3 GetMotorPosition()
    {
        return MotorPosition;
    }

    

}


