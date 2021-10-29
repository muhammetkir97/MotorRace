using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotController : MonoBehaviour
{
    [SerializeField] private MotorBikeControl MotorBike;

    float CurrentSpeed = 0;
    float BaseSpeed = 1000;

    float SpeedInput = 0;
    float DirectionInput = 0;

    float ForwardDetectionRange = 0;
    float LeftDetectionRange = 0;
    float RightDetectionRange = 0;

    void Start()
    {
        ForwardDetectionRange = Globals.Instance.GetForwardRange();
        LeftDetectionRange = Globals.Instance.GetLeftRange();
        RightDetectionRange = Globals.Instance.GetRightRange();

        MotorBike.Init(true,MotorType.Scooter);
    }

    void FixedUpdate()
    {

        
        DetectMovement();

    }

    void DetectMovement()
    {
        SpeedInput = 1;

        RaycastHit tmpHit1, tmpHit2, forwardHit = new RaycastHit();
        bool isForwardHit = false;

        RaycastHit leftHit;
        bool isLeftHit = false;

        RaycastHit rightHit;
        bool isRightHit = false;

        bool forwardHit1 = Physics.Raycast(transform.position + transform.TransformDirection(Vector3.right) * 0.3f, transform.TransformDirection(Vector3.forward), out tmpHit1, ForwardDetectionRange);
        bool forwardHit2 = Physics.Raycast(transform.position + transform.TransformDirection(Vector3.left) * 0.3f, transform.TransformDirection(Vector3.forward), out tmpHit2, ForwardDetectionRange);


        if(forwardHit1 || forwardHit2)
        {
            isForwardHit = true;

            float dist1 = tmpHit1.distance;
            if(!forwardHit1) dist1 = ForwardDetectionRange;

            float dist2 = tmpHit2.distance;
            if(!forwardHit2) dist2 = ForwardDetectionRange;

            if(tmpHit1.distance < tmpHit2.distance)
            {
                forwardHit = tmpHit1;
            }
            else
            {
                forwardHit = tmpHit2;
            }
        }
        else
        {
            isForwardHit = false;
        }

        //isForwardHit = Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out forwardHit, ForwardDetectionRange);
        isLeftHit = Physics.Raycast(transform.position, transform.TransformDirection(Vector3.left) , out leftHit, LeftDetectionRange);
        isRightHit = Physics.Raycast(transform.position, transform.TransformDirection(Vector3.right) , out rightHit, RightDetectionRange);

        float forwardDistance = forwardHit.distance;
        if(!isForwardHit) forwardDistance = ForwardDetectionRange;

        float leftDistance = leftHit.distance;
        if(!isLeftHit) leftDistance = LeftDetectionRange;

        float rightDistance = rightHit.distance;
        if(!isRightHit) rightDistance = RightDetectionRange;

        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * ForwardDetectionRange,Color.red,1);
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.left) * LeftDetectionRange,Color.green,1);
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.right) * RightDetectionRange,Color.yellow,1);


  
        if (isForwardHit)
        {
            if(leftDistance < rightDistance)
            {
                DirectionInput = 1;
            }
            else
            {
                DirectionInput = -1;
            }

        }
        else
        {
            if(isLeftHit || isRightHit)
            {
                if(leftDistance < rightDistance)
                {
                    DirectionInput = 1;
                }
                else
                {
                    DirectionInput = -1;
                }
            }
            else
            {
                DirectionInput = 0;
            }


            
        }
        SetMovement();
    }

    void SetMovement()
    {
        CurrentSpeed =  SpeedInput * BaseSpeed;
        MotorBike.SetSpeed(CurrentSpeed);
        MotorBike.SetAcceleration(SpeedInput);
        MotorBike.SetDirection(DirectionInput * 1500);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
