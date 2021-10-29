using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Globals : MonoBehaviour
{
    public static Globals Instance;

    private float BoostTime = 5f;
    private float CarSpeed = 10;
    private int CarCount = 10;
    private float CarSpacing = 50;
    private int MotorTypeCount = 3;

    private float ForwardDetectionRange = 25;
    private float LeftDetectionRange = 3;
    private float RightDetectionRange = 3;


    void Awake()
    {
        Instance = this;

    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float GetBoostTime()
    {
        return BoostTime;
    }

    public float GetCarSpeed()
    {
        return CarSpeed;
    }

    public int GetCarCount()
    {
        return CarCount;
    }

    public float GetCarSpacing()
    {
        return CarSpacing;
    }

    public int GetMotorCount()
    {
        return MotorTypeCount;
    }

    public float GetForwardRange()
    {
        return ForwardDetectionRange;
    }

    public float GetRightRange()
    {
        return RightDetectionRange;
    }

        public float GetLeftRange()
    {
        return LeftDetectionRange;
    }
}
