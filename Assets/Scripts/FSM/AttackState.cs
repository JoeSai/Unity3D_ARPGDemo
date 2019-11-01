using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : StateTemplate<PlayerCtrl> {

    private AnimatorStateInfo mStateInfo;
    //定义状态常量值
    private const string UnarmedIdleState = "Unarmed-Idle";
    private const string UnarmedAttack1State = "Unarmed-Attack01";
    private const string UnarmedAttack2State = "Unarmed-Attack02";
    private const string SwordIdleState = "Sword-Idle";
    private const string SwordAttack1State = "Sword-Attack01";
    private const string SwordAttack2State = "Sword-Attack02";
    private const string SickleIdleState = "Sickle-Idle";
    private const string SickleAttack1State = "Sickle-Attack01";
    private const string SickleAttack2State = "Sickle-Attack02";
    
    
   

    public AttackState(int id, PlayerCtrl p) : base(id, p)
    {
        Debug.Log("注册攻击");
    }

    public override void OnEnter(params object[] args)
    {
        owner.switchingState = false;
        
        base.OnEnter(args);       
    }
    public override void OnStay(params object[] args)
    {

        base.OnStay(args);

        mStateInfo = owner.anim.GetCurrentAnimatorStateInfo(0);

        if (!mStateInfo.IsTag("Idle") && mStateInfo.normalizedTime > 1F)
        {
            owner.anim.SetInteger("Attack", 0);
            owner.mHitCount = 0;
            owner.isAttack = false;
            owner.playerState = PlayerState.Idle;
            owner.switchingState = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            switch (owner.Weapon)
            {
                case 0:
                    UnarmedAttack();
                    break;
                case 1:
                    SwordAttack();
                    break;
                case 2:
                    SickleAttack();
                    break;
                default:
                    break;
            }
        }
    }
    public override void OnExit(params object[] args)
    {
        base.OnExit(args);
      
    }



    void UnarmedAttack()
    {
        owner.isAttack = true;
        //如果处于Idle状态且攻击次数为0，则按照攻击招式1攻击，否则按照攻击招式2攻击，否则按照攻击招式3攻击
        if (mStateInfo.IsName(UnarmedIdleState) && owner.mHitCount == 0 && mStateInfo.normalizedTime > 0)
        {
            owner.anim.SetInteger("Attack", 1);
            owner.mHitCount = 1;
        }
        else if (mStateInfo.IsName(UnarmedAttack1State) && owner.mHitCount == 1 && mStateInfo.normalizedTime > 0)
        {
            owner.anim.SetInteger("Attack", 2);
            owner.mHitCount = 2;
        }
        else if (mStateInfo.IsName(UnarmedAttack2State) && owner.mHitCount == 2 && mStateInfo.normalizedTime > 0)
        {
            owner.anim.SetInteger("Attack", 3);
            owner.mHitCount = 3;
        }
    }
    void SwordAttack()
    {
         owner.isAttack = true;
        //如果玩家处于Idle状态且攻击次数为0，则玩家按照攻击招式1攻击，否则按照攻击招式2攻击，否则按照攻击招式3攻击
        if (mStateInfo.IsName(SwordIdleState) && owner.mHitCount == 0 && mStateInfo.normalizedTime > 0)
        {
            owner.anim.SetInteger("Attack", 1);
            owner.mHitCount = 1;
        }
        else if (mStateInfo.IsName(SwordAttack1State) && owner.mHitCount == 1 && mStateInfo.normalizedTime > 0)
        {
            owner.anim.SetInteger("Attack", 2);
            owner.mHitCount = 2;
        }
        else if (mStateInfo.IsName(SwordAttack2State) && owner.mHitCount == 2 && mStateInfo.normalizedTime > 0)
        {
            owner.anim.SetInteger("Attack", 3);
            owner.mHitCount = 3;
        }
    }

    void SickleAttack()
    {
        owner.isAttack = true;
        //如果处于Idle状态且攻击次数为0，则按照攻击招式1攻击，否则按照攻击招式2攻击，否则按照攻击招式3攻击
        if (mStateInfo.IsName(SickleIdleState) && owner.mHitCount == 0 && mStateInfo.normalizedTime > 0)
        {
            owner.anim.SetInteger("Attack", 1);
            owner.mHitCount = 1;
        }
        else if (mStateInfo.IsName(SickleAttack1State) && owner.mHitCount == 1 && mStateInfo.normalizedTime > 0)
        {
            owner.anim.SetInteger("Attack", 2);
            owner.mHitCount = 2;
        }
        else if (mStateInfo.IsName(SickleAttack2State) && owner.mHitCount == 2 && mStateInfo.normalizedTime > 0)
        {
            owner.anim.SetInteger("Attack", 3);
            owner.mHitCount = 3;
        }
    }

}
