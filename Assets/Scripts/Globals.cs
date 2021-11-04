using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Globals : MonoBehaviour
{
    public static Globals Instance;

    private float BoostTime = 5f;
    private float CarSpeed = 50;
    private float CarDirectionSpeed = 2;


    private float BotSpeed = 2000;
    private float BotDirectionSpeed = 2000;
    private float BotSpeedChangeRatio = 1.1f;
    private int CarCount = 10;
    private float CarSpacing = 700;
    private int MotorTypeCount = 3;


    private float ForwardDetectionRange = 80;
    private float ForwardDetectionLength = 2;
    private float LeftDetectionRange = 15;
    private float RightDetectionRange = 15;


    private float BackRoadLimit = 250;
    private float ForwardRoadLimit = 1000;


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

    public float GetCarDirectionSpeed()
    {
        return CarDirectionSpeed;
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

    public float GetForwardDetectionLength()
    {
        return ForwardDetectionLength;
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

    public float GetBackRoadLimit()
    {
        return BackRoadLimit;
    }

    public float GetForwardRoadLimit()
    {
        return ForwardRoadLimit;
    }

    public float GetBotSpeed()
    {
        return BotSpeed;
    }

    public float GetBotDirectionSpeed()
    {
        return BotDirectionSpeed;
    }

    public float GetBotSpeedRatio()
    {
        return BotSpeedChangeRatio;
    }
}
