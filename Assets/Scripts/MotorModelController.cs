using System.Collections;
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
    private MotorType SelectedMotorType;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetSelectedMotor(MotorType motorType, bool isPlayer, Color selectedColor)
    {
        SelectedMotorType = motorType;
        
        AnimatorControl.SetFloat("MotorType", (float)motorType);

        SetHandBodyStatus(isPlayer);
        foreach(MeshRenderer renderer in MotorBodyMaterials)
        {
            List<Material> mats = new List<Material>();
            renderer.GetMaterials(mats);

            foreach(Material mat in mats)
            {
                if(mat.name.Contains("Base") || mat.name.Contains("Body") )
                {
                    mat.SetColor("Color_9AA280EA",selectedColor);
                    break;
                }
            } 
        }
    }

    public void SetHandBodyStatus(bool status)
    {
        DriverBody.SetActive(!status);

        for(int i=0; i<MotorValues.Length; i++)
        {
            bool isActive = i == (int)SelectedMotorType;

            MotorValues[i].MotorObject.SetActive(isActive);
            MotorValues[i].MotorHand.SetActive(isActive && status);
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

    public void SetAnimatorStatus(bool status)
    {
        AnimatorControl.enabled = status;
    }
}

[System.Serializable]
public class MotorProperties
{
    public GameObject MotorObject;
    public GameObject MotorHand;

}
