using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controlmove : MonoBehaviour
{
    public float speed;
    public Vector3 RayOrigin;
    private Player1 player;
    private Rigidbody rig;

    Ray ray;


    // Use this for initialization
    void Start()
    {
        speed = 1.0f;
        player = GetComponent<Player1>();
        rig = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        RayOrigin = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z) + this.transform.up * 2 + this.transform.forward * speed;
        ray.origin = RayOrigin;
        ray.direction = new Vector3(0, -1, 0);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Debug.DrawLine(ray.origin, hit.point, Color.red);
            Debug.DrawLine(transform.position, hit.point, Color.green);           
        }

        if(player.isRunning==true&&hit.transform.name== "Stairs")
        {
            rig.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
            this.transform.Translate(0, -this.transform.position.y + hit.point.y, Time.deltaTime * speed);
            Debug.Log(transform.position-hit.point);
        }
       

        //if (Input.GetKey(KeyCode.UpArrow))
        //{
        //    Debug.Log(hit.point);
        //    this.transform.Translate(0, -this.transform.position.y + hit.point.y, Time.deltaTime * speed);
        //}
        //if (Input.GetKey(KeyCode.DownArrow))
        //    this.transform.Translate(0, 0, -Time.deltaTime);
        //if (Input.GetKey(KeyCode.LeftArrow))
        //    this.transform.Rotate(0, -1, 0);
        //if (Input.GetKey(KeyCode.RightArrow))
        //    this.transform.Rotate(0, 1, 0);




    }
 
}
