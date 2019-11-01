using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceState : StateTemplate<PlayerCtrl>
{

    private float time = 0;

    public ForceState(int id, PlayerCtrl p) : base(id, p)
    {
        
    }

    public override void OnEnter(params object[] args)
    {
        base.OnEnter(args);
        owner.switchingState = false;
       
        owner.gamePanel.Hp -= owner.reduceHpValue;
        owner.anim.SetTrigger("GetHit");
        owner.anim.SetInteger("Attack", 0);
        owner.anim.SetBool("IsRunning", false);
        owner.isAttack = false;
        owner.mHitCount = 0;

        if (owner.gamePanel.Hp <= 0)
        {
            owner.isDeath = true;
            switch (owner.Weapon)
            {
                case 0:
                    owner.anim.SetTrigger("IsDeath_Unarmed");
                    break;
                case 1:
                    owner.anim.SetTrigger("IsDeath_Sword");
                    break;
                case 2:
                    owner.anim.SetTrigger("IsDeath_Sickle");
                    break;
            }
        }
        owner.isGetHit = false;
        owner.isForce = false;
    }

    public override void OnStay(params object[] args)
    {
        base.OnStay(args);       
            Force();
    }

    public void Force()
    {
        time += Time.deltaTime;
        owner.cc.SimpleMove((owner.transform.position-owner.forceVec)  * 80 * Time.deltaTime);
        if (time > 0.55f)
        {        
            time = 0;
            owner.playerState = PlayerState.Idle;
            owner.switchingState = true;          
        }
    }
  
}
