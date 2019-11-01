using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove2 : MonoBehaviour
{
    private CharacterController cc;
    public float speed = 6;   
    public float gravity = 20.0f;
    private Vector3 MoveDir = Vector3.zero;
    private Animator anim;

    void Start()
    {
        cc = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }


    void FixedUpdate()
    {
        if(cc.isGrounded)
        {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
            if (Mathf.Abs(h) > 0.1f || Mathf.Abs(v) > 0.1)
            {
               MoveDir = new Vector3(h, 0, v);
                MoveDir *=speed;
                Quaternion look = Quaternion.LookRotation(MoveDir);
                Quaternion lookLerp = Quaternion.Slerp(transform.rotation, look, Time.deltaTime * 8f);
                transform.rotation = lookLerp;
                anim.SetBool("IsRunning", true);
            }
            else
            {
                anim.SetBool("IsRunning", false);
                MoveDir = Vector3.zero;
            }
        }
       
        MoveDir.y -= gravity * Time.deltaTime;
        cc.Move(MoveDir * Time.deltaTime);
    }
}
