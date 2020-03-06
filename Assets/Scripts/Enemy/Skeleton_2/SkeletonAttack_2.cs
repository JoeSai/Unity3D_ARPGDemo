using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NPBehave;

public class SkeletonAttack_2 : Task
{
    private Animator anim;
    private Blackboard ownBlackboard;
    private bool Attack = false;
    private AnimatorStateInfo stateInfo;
    private bool canAttack = false;
    private System.Action<IEnumerator> StartCoroutine;

    public SkeletonAttack_2(SkeletonBehaviorTree skeletonBehaviorTree) : base("SkeletonAttack_2")
    {
        anim = skeletonBehaviorTree.Anim;
        ownBlackboard = skeletonBehaviorTree.Blackboard;
        StartCoroutine = skeletonBehaviorTree.StartCoroutine_BehaviorTree;
    }

    protected override void DoStart()
    {
        stateInfo = anim.GetCurrentAnimatorStateInfo(0);
        canAttack = ownBlackboard.Get<bool>("canAttack");
        if (canAttack == false)
        {
            this.Stopped(false);
            return;
        }
        if (Attack == false && canAttack == true && stateInfo.IsTag("GetHit") == false)
        {
            Attack = true;
            StartCoroutine(DelayFun());
            this.Stopped(true);
        }
        else
        {
            if (stateInfo.normalizedTime > 0.8f)
            anim.SetInteger("Attack", 0);
            this.Stopped(false);
        }
    }

    private IEnumerator DelayFun()
    {
        yield return new WaitForSeconds(2.5f);
        if (canAttack == false && stateInfo.IsTag("GetHit") == false)
            yield return 0;
        int a = UnityEngine.Random.Range(1, 4);
        anim.SetInteger("Attack", a);
        Attack = false;
    }

}
