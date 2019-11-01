using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollState : StateTemplate<PlayerCtrl>
{

    private bool roll=false;

    public RollState(int id, PlayerCtrl p) : base(id, p)
    {
        Debug.Log("注册翻滚，按键空格");
    }
    public override void OnEnter(params object[] args)
    {
        owner.switchingState = false;
        roll = true;
        owner.anim.SetTrigger("RollTrigger");
        base.OnEnter(args);
    }


    public override void OnStay(params object[] args)
    {
        base.OnStay(args);
        Roll();
    }
    private void Roll()
    { 
        Vector3 forward = owner.transform.TransformDirection(Vector3.forward);
        owner.cc.SimpleMove(forward * owner.Rollpow*Time.deltaTime);
        if (roll)
        {
            owner.StartCoroutine(DelayFuc(() => {
                owner.playerState = PlayerState.Idle;
                owner.switchingState = true;
                owner.isRoll = false;
                owner.isGetHit = false;
            }, 0.36f));
            roll = false;
        }
       
        //transform.Translate(transform.forward * force * Time.deltaTime, Space.World);
        // rig.AddForce(transform.forward * force * Time.deltaTime, ForceMode.VelocityChange);       
    }
    private IEnumerator DelayFuc(Action action, float delaySeconds)
    {
        yield return new WaitForSeconds(delaySeconds);
        action();
    }

}
