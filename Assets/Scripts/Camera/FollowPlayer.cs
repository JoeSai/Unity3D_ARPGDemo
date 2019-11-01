using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
     private Transform player;
    private Vector3 offsetPosition;    
    private bool isRotating = false;
    //private Ray ray;
    //RaycastHit hit;
    //private GameObject selectModel;
    //private MeshRenderer targetMeshs;

   // private Shader shader;

    public float rotateSpeed = 2;
    //public Material replaceMat;
    //public Material origMat;
    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player").transform;
         transform.position = new Vector3(player.position.x,
          player.position.y + 5,
          player.position.z - 5);
          
        transform.LookAt(player.position);
        offsetPosition = transform.position - player.position;

        
       // ray = Camera.main.ScreenPointToRay(transform.position);
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = offsetPosition + player.position;
        //处理视野的旋转
        RotateView();

        //ChangeAlpha();


    }
   
    void RotateView()
    {
        //Input.GetAxis("Mouse X")鼠标在水平方向的滑动
        //Input.GetAxis("Mouse Y")鼠标在垂直方向的滑动
        if (Input.GetMouseButtonDown(1))
        {
            isRotating = true;
        }
        if (Input.GetMouseButtonUp(1))
        {
            isRotating = false;
        }
        if (isRotating)
        {
            transform.RotateAround(player.position, player.up, rotateSpeed * Input.GetAxis("Mouse X"));

            Vector3 originalPos = transform.position;
            Quaternion originalRotation = transform.rotation;

            transform.RotateAround(player.position, transform.right, -rotateSpeed * Input.GetAxis("Mouse Y"));
            float x = transform.eulerAngles.x;
            if (x < 10 || x > 80)
            {
                transform.position = originalPos;
                transform.rotation = originalRotation;
            }         
        }
        offsetPosition = transform.position - player.position;
    }



    //void ChangeAlpha()
    //{
    //    if (Physics.SphereCast(transform.position, 0.5f, transform.forward, out hit, 500))
    //    {
    //        if (hit.collider.gameObject.tag == "Wall")
    //        {
    //            selectModel = hit.collider.gameObject;
    //            if (selectModel != null)
    //            {
    //                targetMeshs = hit.collider.transform.GetComponent<MeshRenderer>();
    //                targetMeshs.material = replaceMat;
    //            }
    //        }
    //        else
    //        {
    //            if (selectModel != null)
    //            {
    //                targetMeshs.material = origMat;
    //                selectModel = null;
    //            }
                    
    //        }
    //    }
       
    //}
}
