using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class MotorBikeControl : MonoBehaviour
{

    public enum SpeedMultiplierType
    {
        Near,
        Boost
    }

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


    private Rigidbody MotorRigidbody;
    private Transform BodyParent;

    void Start()
    {
        MotorRigidbody = transform.GetComponent<Rigidbody>();
        BodyParent = transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        Movement();
    }

    void Movement()
    {
        CurrentSpeed = Mathf.SmoothDamp(CurrentSpeed, TargetSpeed, ref SpeedSmoothVel, 0.5f);
        CurrentDirection = Mathf.SmoothDamp(CurrentDirection, TargetDirection, ref DirectionSmoothVel, 0.1f);
        CurrentAngle = Mathf.SmoothDamp(CurrentAngle, TargetAngle, ref AngleSmoothVel, 0.2f);

        MotorRigidbody.AddForce((Vector3.forward * CurrentSpeed * Time.deltaTime) + (Vector3.right * CurrentDirection * Time.deltaTime),ForceMode.Force);
        //MotorRigidbody.velocity = (Vector3.forward * CurrentSpeed * Time.deltaTime) + (Vector3.right * CurrentDirection * Time.deltaTime);

        BodyParent.localRotation = Quaternion.Euler(0, 0, -CurrentAngle * 35);


    }

    public void SetSpeed(float newSpeed)
    {
        TargetSpeed = newSpeed;
    }

    public void SetDirection(float direction)
    {
        TargetDirection = direction;

        TargetAngle = Mathf.Sign(direction);
        if(direction == 0) TargetAngle = 0;
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


}
