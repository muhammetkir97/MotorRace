using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    bool IsInit = false;
    int Direction = 1;
    int Lane = 0;
    float CurrentSpeed = 0;
    float DefaultSpeed = 0;


    void Start()
    {
        
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
        if(IsInit)
        {
            transform.Translate(Vector3.forward * Direction * CurrentSpeed * Time.deltaTime,Space.World);
        }

    
    }

    public void InitCar(int lane, int dir, float speed, Vector3 position)
    {
        transform.position = position;
        Direction = dir;
        Lane = lane;

        CurrentSpeed = speed;
        DefaultSpeed = speed;
        IsInit = true;

        if(Direction == -1)
        {
            transform.rotation = Quaternion.Euler(0,0,0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0,180,0);
        }

    }

    public void DisableCar()
    {
        IsInit = false;
    }

    public int GetDirection()
    {
        return Direction;
    }
}
