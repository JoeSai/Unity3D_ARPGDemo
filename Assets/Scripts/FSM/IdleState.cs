using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : StateTemplate<PlayerCtrl> {

    private Vector3 MoveDir = Vector3.zero;
    private float gravity = 20f;

    public IdleState(int id, PlayerCtrl p) : base(id, p)
    {
        Debug.Log("开始");
    }

    public override void OnEnter(params object[] args)
    {
        owner.switchingState = false;

        owner.anim.SetBool("IsRunning", false);
        owner.isAttack = false;
        
        base.OnEnter(args);       
    }
    public override void OnStay(params object[] args)
    {
        base.OnStay(args);      
        if (owner.cc.isGrounded == false)
        {
            MoveDir.y -= gravity * Time.deltaTime;
            owner.cc.Move(MoveDir * Time.deltaTime);
        }
    }
    public override void OnExit(params object[] args)
    {
      
        base.OnExit(args);
    }

}
