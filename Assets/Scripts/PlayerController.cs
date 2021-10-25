using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private MotorBikeControl MotorBike;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        MotorBike.SetSpeed(Input.GetAxisRaw("Vertical") * 1000);
        MotorBike.SetDirection(Input.GetAxisRaw("Horizontal") * 500);
    }
}
