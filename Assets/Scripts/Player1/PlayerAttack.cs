using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

    //Mecanim动画组件
    private Animator mAnimator = null;
    //动画状态信息
    private AnimatorStateInfo mStateInfo;
    //定义状态常量值，前面不要带层名啊，否则无法判断动画状态
    private const string UnarmedIdleState = "Unarmed-Idle";
    private const string UnarmedAttack1State = "Unarmed-Attack01";
    private const string UnarmedAttack2State = "Unarmed-Attack02";
    private const string SwordIdleState = "Sword-Idle";
    private const string SwordAttack1State = "Sword-Attack01";
    private const string SwordAttack2State = "Sword-Attack02";
    private const string SickleIdleState = "Sickle-Idle";
    private const string SickleAttack1State = "Sickle-Attack01";
    private const string SickleAttack2State = "Sickle-Attack02";
    private Player1 player;
    //定义玩家连击次数
    private int mHitCount = 0;

    void Start()
    {
        //获取动画组件
        mAnimator = GetComponent<Animator>();
        player = GetComponent<Player1>();
    }

    void Update()
    {
       
        mStateInfo = mAnimator.GetCurrentAnimatorStateInfo(0);
        //如果玩家处于攻击状态，且攻击已经完成，则返回到Idle状态
        if (!mStateInfo.IsTag("Idle") && mStateInfo.normalizedTime > 1F)
        {
            mAnimator.SetInteger("Attack", 0);
            mHitCount = 0;
            player.isAttack = false; 
        }
        //如果按下鼠标左键，则开始攻击
        if (Input.GetMouseButtonUp(0))
        {
            //SwordAttack();
            //SickleAttack();
            if (player.Weapon == 0)
                UnarmedAttack();
            else if (player.Weapon == 1)
                SwordAttack();
            else if (player.Weapon == 2)
                SickleAttack();
        }
    }

    void UnarmedAttack()
    {
        player.isAttack = true;
        //如果玩家处于Idle状态且攻击次数为0，则玩家按照攻击招式1攻击，否则按照攻击招式2攻击，否则按照攻击招式3攻击
        if (mStateInfo.IsName(UnarmedIdleState) && mHitCount == 0 && mStateInfo.normalizedTime > 0)
        {
            mAnimator.SetInteger("Attack", 1);
            mHitCount = 1;
        }
        else if (mStateInfo.IsName(UnarmedAttack1State) && mHitCount == 1 && mStateInfo.normalizedTime > 0)
        {
            mAnimator.SetInteger("Attack", 2);
            mHitCount = 2;
        }
        else if (mStateInfo.IsName(UnarmedAttack2State) && mHitCount == 2 && mStateInfo.normalizedTime > 0)
        {
            mAnimator.SetInteger("Attack", 3);
            mHitCount = 3;
        }
    }
    void SwordAttack()
    {
        player.isAttack = true;
        //如果玩家处于Idle状态且攻击次数为0，则玩家按照攻击招式1攻击，否则按照攻击招式2攻击，否则按照攻击招式3攻击
        if (mStateInfo.IsName(SwordIdleState) && mHitCount == 0 && mStateInfo.normalizedTime > 0)
        {
            mAnimator.SetInteger("Attack", 1);
            mHitCount = 1;
        }
        else if (mStateInfo.IsName(SwordAttack1State) && mHitCount == 1 && mStateInfo.normalizedTime > 0)
        {
            mAnimator.SetInteger("Attack", 2);
            mHitCount = 2;
        }
        else if (mStateInfo.IsName(SwordAttack2State) && mHitCount == 2 && mStateInfo.normalizedTime > 0)
        {
            mAnimator.SetInteger("Attack", 3);
            mHitCount = 3;
        }
    }

    void SickleAttack()
    {
        player.isAttack = true;
        //如果玩家处于Idle状态且攻击次数为0，则玩家按照攻击招式1攻击，否则按照攻击招式2攻击，否则按照攻击招式3攻击
        if (mStateInfo.IsName(SickleIdleState) && mHitCount == 0 && mStateInfo.normalizedTime > 0)
        {
            mAnimator.SetInteger("Attack", 1);
            mHitCount = 1;
        }
        else if (mStateInfo.IsName(SickleAttack1State) && mHitCount == 1 && mStateInfo.normalizedTime > 0)
        {
            mAnimator.SetInteger("Attack", 2);
            mHitCount = 2;
        }
        else if (mStateInfo.IsName(SickleAttack2State) && mHitCount == 2 && mStateInfo.normalizedTime > 0)
        {
            mAnimator.SetInteger("Attack", 3);
            mHitCount = 3;
        }
    }

    void AttackIdle()
    {

        Debug.Log("攻击到目标");
    }

}

