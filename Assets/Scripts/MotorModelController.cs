﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MotorType
{
    Scooter,
    Chopper,
    Sport
}

public class MotorModelController : MonoBehaviour
{
    [SerializeField] private Animator AnimatorControl;
    [SerializeField] private GameObject DriverBody;
    [SerializeField] private MotorProperties[] MotorValues;
    [SerializeField] private MeshRenderer[] MotorBodyMaterials;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetSelectedMotor(MotorType motorType, bool isPlayer, Color selectedColor)
    {
        DriverBody.SetActive(!isPlayer);
        AnimatorControl.SetFloat("MotorType", (float)motorType);
        for(int i=0; i<MotorValues.Length; i++)
        {
            bool isActive = i == (int)motorType;

            MotorValues[i].MotorObject.SetActive(isActive);
            MotorValues[i].MotorHand.SetActive(isActive && isPlayer);
        }

        foreach(MeshRenderer renderer in MotorBodyMaterials)
        {
            List<Material> mats = new List<Material>();
            renderer.GetMaterials(mats);

            foreach(Material mat in mats)
            {
                if(mat.name.Contains("Base"))
                {
                    mat.color = selectedColor;
                    break;
                }
            }

            
        }
    }

    public void SetDirection(float direction)
    {
        float dir = (direction + 1) / 2f;
        AnimatorControl.SetFloat("Direction", dir);
    }

    public void SetAcceleration(float acceleration)
    {
        float accel = (acceleration + 1) / 2f;
        AnimatorControl.SetFloat("Acceleration", accel);
    }
}

[System.Serializable]
public class MotorProperties
{
    public GameObject MotorObject;
    public GameObject MotorHand;

}
