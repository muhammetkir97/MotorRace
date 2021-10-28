using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private MotorBikeControl MotorBike;

    float CurrentSpeed = 0;
    float BaseSpeed = 1000;

    void Start()
    {
        
        InvokeRepeating("CameraShake",0,0.3f);
        MotorBike.Init(false,MotorType.Chopper);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        float speedInput = Input.GetAxisRaw("Vertical");
        float directionInput = Input.GetAxisRaw("Horizontal");
        CurrentSpeed =  speedInput * BaseSpeed;
        MotorBike.SetSpeed(CurrentSpeed);
        MotorBike.SetAcceleration(speedInput);
        MotorBike.SetDirection(directionInput * 1000);
    }

    void CameraShake()
    {
        float shakeRate = CurrentSpeed / BaseSpeed;
        iTween.ShakeRotation(Camera.main.gameObject,Random.insideUnitSphere.normalized * (0.25f * shakeRate),0.3f);
    }
}
