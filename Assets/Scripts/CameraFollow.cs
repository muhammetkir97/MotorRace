using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform TargetObject;
    private float Smooth = 0.01f;
    private Vector3 Offset;
    private Vector3 FollowSmooth = Vector3.zero;

    void Awake()
    {
        Offset = transform.position - TargetObject.position;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        //transform.position = TargetObject.position + Offset;
        //transform.position = Vector3.Slerp(transform.position, TargetObject.position + Offset,Time.time * Smooth);
        //transform.position = Vector3.SmoothDamp(transform.position, TargetObject.position + Offset, ref FollowSmooth, 0.001f); 
        //transform.rotation = TargetObject.rotation;
    }

    void LateUpdate()
    {
        float angle = TargetObject.rotation.eulerAngles.z;

        if(angle > 180) angle = angle - 360;

        float dir = Mathf.Sign(angle);
        if(angle == 0) dir = 0;

        transform.position = TargetObject.position + Offset + (Vector3.right * angle * -0.005f);
        //transform.position = Vector3.Slerp(transform.position, TargetObject.position + Offset,Time.time * Smooth);
        //transform.position = Vector3.SmoothDamp(transform.position, TargetObject.position + Offset, ref FollowSmooth, 0.001f); 
        //transform.rotation = TargetObject.rotation;
    }


}
