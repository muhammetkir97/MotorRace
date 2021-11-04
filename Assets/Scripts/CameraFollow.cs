using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform TargetObject;
    private float FollowSmooth = 0.00001f;
    private Vector3 FollowVel = Vector3.zero;

    private float CrashSmooth = 5;
    private Vector3 Offset;
    private bool CrashStatus = false;

    private Vector3 StartRotation;
    

    void Awake()
    {
        Offset = transform.position - TargetObject.position;
    }
    void Start()
    {
        StartRotation = transform.rotation.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {


         
    }

    void LateUpdate()
    {

               if(!CrashStatus)
        {
            Follow();
        } 
    }

    void Follow()
    {
        float angle = TargetObject.rotation.eulerAngles.z;

        if(angle > 180) angle = angle - 360;

        float dir = Mathf.Sign(angle);
        if(angle == 0) dir = 0;

        Vector3 targetPos = TargetObject.position + Offset + (Vector3.right * angle * -0.005f);
        transform.position = targetPos;
        //transform.position = Vector3.Slerp(transform.position, TargetObject.position + Offset,Time.time * Smooth);
        //transform.position = Vector3.SmoothDamp(transform.position, TargetObject.position + Offset, ref FollowSmooth, 0.001f); 
        //transform.rotation = TargetObject.rotation;
    }



    public void SetCrashStatus(bool status)
    {
        
        if(status)
        {
            CrashStatus = status;
            SetCameraPos(TargetObject.position + Offset + (Vector3.up * 20), new Vector3(30,0,0));
        }
        else
        {
            SetCameraPos(TargetObject.position + Offset, StartRotation);
            Invoke("SetStatusLate",0.3f);
        }
    }

    void SetCameraPos(Vector3 targetPos, Vector3 targetRotation)
    {
            iTween.RotateTo(gameObject, targetRotation, 0.3f);
            iTween.MoveTo(gameObject, targetPos, 0.3f);
    }

    void SetStatusLate()
    {
        CrashStatus = false;
    }






}
