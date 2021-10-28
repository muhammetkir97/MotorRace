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

    void Start()
    {
        MotorBike.Init(true,MotorType.Scooter);
    }

    void FixedUpdate()
    {

        
        DetectMovement();

    }

    void DetectMovement()
    {
        SpeedInput = 1;

        RaycastHit forwardHit;
        bool isForwardHit = false;

        RaycastHit leftHit;
        bool isLeftHit = false;

        RaycastHit rightHit;
        bool isRightHit = false;

        isForwardHit = Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out forwardHit, 15);
        isLeftHit = Physics.Raycast(transform.position, transform.TransformDirection(Vector3.left) , out leftHit, 5);
        isRightHit = Physics.Raycast(transform.position, transform.TransformDirection(Vector3.right) , out rightHit, 5);

        float forwardDistance = forwardHit.distance;
        if(!isForwardHit) forwardDistance = 15;

        float leftDistance = leftHit.distance;
        if(!isLeftHit) leftDistance = 5;

        float rightDistance = rightHit.distance;
        if(!isRightHit) rightDistance = 5;

        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 15,Color.red,1);
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.left) * 5,Color.green,1);
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.right) * 5,Color.yellow,1);


  
        if (forwardDistance < 12)
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
            if(leftDistance < 3 && rightDistance < 3)
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
        MotorBike.SetDirection(DirectionInput * 1000);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
