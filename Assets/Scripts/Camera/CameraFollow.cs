using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public float Camera_Height;
    public float Camera_Distance;
    // Use this for initialization

   
    private Transform PlayerTrans;



    void Start()
    {
        PlayerTrans = GameObject.FindGameObjectWithTag("Player").transform;

    }


    private void LateUpdate()
    {
        transform.position = new Vector3(PlayerTrans.position.x,

          PlayerTrans.position.y + this.Camera_Height,

          PlayerTrans.position.z - this.Camera_Distance);
        transform.LookAt(PlayerTrans.position);

    }


    
}
