using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunState : StateTemplate<PlayerCtrl> {



    private Vector3 MoveDir = Vector3.zero;
    private float speed = 6f;
    private float gravity = 20f;

    public RunState(int id, PlayerCtrl p) : base(id, p)
    {
        Debug.Log("注册跑动");
    }

    public override void OnEnter(params object[] args)
    {
        base.OnEnter(args);
        owner.switchingState = false;
    }
    public override void OnStay(params object[] args)
    {        
        base.OnStay(args);

        Run();
    }
    //public override void OnExit(params object[] args)
    //{
    //    base.OnExit(args);
    //}

    void Run()
    {        
        if (owner.cc.isGrounded)
        {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");

            if (Mathf.Abs(h) > 0.1f || Mathf.Abs(v) > 0.1)
            {
                MoveDir = new Vector3(h,0,v);;
                MoveDir *= speed;
                Quaternion look = Quaternion.LookRotation(MoveDir);
                Quaternion lookLerp = Quaternion.Slerp(owner.transform.rotation, look, Time.deltaTime * 8f);
                owner.transform.rotation = lookLerp;
                owner.anim.SetBool("IsRunning", true);
            }
            else
            {
                owner.playerState = PlayerState.Idle;
                owner.switchingState = true;
                owner.anim.SetBool("IsRunning", false);
                MoveDir = Vector3.zero;
            }
        }

        MoveDir.y -= gravity * Time.deltaTime;
        owner.cc.Move( MoveDir * Time.deltaTime);
    }

}
