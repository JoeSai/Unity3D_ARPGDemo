using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {


    private Animator anim;
    private Rigidbody rig;
    public float velocity = 4; //速度
    public float force = 5;
    public bool isIdle = false;
    public bool isRun = false;
    public bool isRoll = false;
    public bool isAttack = false;
    //private AnimatorStateInfo mStateInfo;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        rig = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Unarmed-Idle") &&
            !anim.GetCurrentAnimatorStateInfo(0).IsName("Unarmed-Run")
            )
        {
            
        }
        
        //mStateInfo = anim.GetCurrentAnimatorStateInfo(0);
        //if (Input.GetKeyDown("space")|| mStateInfo.IsName("Unarmed-Roll"))
        //{           
        //    Roll();

        //    if (mStateInfo.normalizedTime>0.45F)
        //    {
        //        anim.SetBool("Roll", false);
        //    }
        //}
        //if (mStateInfo.IsName("Unarmed-Roll"))
        //{
        //    Debug.Log(mStateInfo.normalizedTime);
        //}


        if (Input.GetKeyDown("space")&& isAttack==false)
        {
            isRoll = true;
            Roll();           
        }
        if (isAttack == false&&isRoll==false)
        {
            Move();
        }            
    }
    void Move()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 nowVel = rig.velocity;
        if (Mathf.Abs(h) > 0.5f || Mathf.Abs(v) > 0.5f)
        {
            Vector3 dir = new Vector3(h, nowVel.y, v);
            rig.isKinematic = false;
            anim.SetBool("IsRuning", true);

            rig.velocity = dir * velocity;
            //transform.Translate(new Vector3(h, 0, v) * velocity * Time.deltaTime, Space.World);
            Quaternion look = Quaternion.LookRotation(dir);
            Quaternion lookLerp = Quaternion.Slerp(transform.rotation, look, Time.deltaTime * 8f);
            transform.rotation = lookLerp;
        }
        else
        {           
            anim.SetBool("IsRuning", false);
            rig.velocity = new Vector3(0, nowVel.y, 0);
        }

    }
    void Roll()
    {       
        anim.SetTrigger("RollTrigger");
        //transform.Translate(transform.forward * force * Time.deltaTime, Space.World);       
        rig.AddForce(transform.forward * force * Time.deltaTime, ForceMode.VelocityChange);

    }

}
