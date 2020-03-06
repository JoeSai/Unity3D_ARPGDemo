using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NPBehave;

public class EnemyGetHit_2 : Task
{
    private int damageValue;
    private bool isWake;
    private int enemyHP=150;
    private bool enemyDeath = false;
    private Animator anim;
    private Image BloodBar;
    private bool trigger = false;
    private ParticleSystem BloodParticle;
    private Transform transform;
    private float HP;
    private Blackboard ownBlackboard;

    public EnemyGetHit_2(SkeletonBehaviorTree skeletonBehaviorTree) : base("EnemyGetHit_2")
    {
        anim = skeletonBehaviorTree.Anim;
        ownBlackboard = skeletonBehaviorTree.Blackboard;
        transform = skeletonBehaviorTree.transform;
        BloodBar= this.transform.Find("Canvas").GetChild(0).GetComponent<Image>();
        BloodBar.gameObject.SetActive(true);
        BloodParticle = this.transform.Find("BloodParticle").GetComponent<ParticleSystem>();
        HP = enemyHP;
        ownBlackboard["enemyHit"] = false;
    }
    protected override void DoStart()
    {
        if (ownBlackboard.Get<bool>("enemyHit"))
        {
            anim.SetTrigger("GetHit");
            ReduceHPValue();
            BloodParticle.Play();
            ownBlackboard["enemyHit"] = false;
        }

        this.Stopped(false);
    }

    private void ReduceHPValue()
    {
        enemyHP -= damageValue;
        BloodBar.fillAmount = enemyHP / HP;

        if (enemyHP <= 0)
        {
            enemyDeath = true;
        }
    }
}
