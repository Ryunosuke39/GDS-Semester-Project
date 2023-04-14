using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//How to make a camera followw system by code monkey. for making this more smooth watch this.
public class CameraFollow : MonoBehaviour
{
    private Func<Vector3> GetCameraFollowPositionFunc;

    public void SetUp(Func<Vector3> GetCameraFollowPositionFunc)
    {
        this.GetCameraFollowPositionFunc = GetCameraFollowPositionFunc;
    }
    // Update is called once per frame
    void Update()
    {

        if(GetCameraFollowPositionFunc == null)
        {
            return;
        }


        Vector3 cameraFollowPosition = GetCameraFollowPositionFunc();
        if (cameraFollowPosition == Vector3.zero)
        {
            return;
        }
        cameraFollowPosition.z = transform.position.z;
        transform.position = cameraFollowPosition;
    }
}
