using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class BossPhaseOneAttack : Action
{
    public SharedBool cAttcak = false;
    public SharedBool enemyIsHit = false;
    public SharedBool isPhaseTwo = false;
    public float phaseOneWaitTime;
    //private float startTime;
    private Animator anim;
    private bool Attack = false;
    private AnimatorStateInfo animatorInfo;
    private ParticleSystem SpecialAttack01;

    private int AttackCount = 0;

    public override void OnAwake()
    {

        anim = gameObject.GetComponent<Animator>();
        SpecialAttack01 = this.transform.Find("SpecialAttack01").GetChild(0).GetComponent<ParticleSystem>();
    }
   

    public override TaskStatus OnUpdate()
    {
        animatorInfo = anim.GetCurrentAnimatorStateInfo(0);

        if (animatorInfo.normalizedTime > 0.8f)
        {
            if (animatorInfo.IsTag("Attack"))
                anim.SetInteger("Attack", 0);
            if (animatorInfo.IsTag("SpecialAttack"))
                anim.SetInteger("SpecialAttack", 0);
        }

        if (cAttcak.Value == true && isPhaseTwo.Value == false)
        {
            anim.SetBool("Walk", false);
            if (enemyIsHit.Value == true)
            {
                anim.SetInteger("Attack", 0);
                anim.SetInteger("SpecialAttack", 0);
                StartCoroutine(isHit());
                return TaskStatus.Success;
            }

            if (!Attack && !animatorInfo.IsTag("Special")&& enemyIsHit.Value==false)
            {
                Attack = true;
                if(AttackCount<3)
                StartCoroutine(phaseOneAttack());
                else
                {
                    StartCoroutine(phaseOneSpecialAttack());
                }
                return TaskStatus.Success;
            }
        }

        return TaskStatus.Failure;
    }

    public IEnumerator phaseOneAttack()
    {
        yield return new WaitForSeconds(phaseOneWaitTime);
        if (isPhaseTwo.Value == false)
        {
            AttackCount++;
            int a = Random.Range(1, 6);
            anim.SetInteger("Attack", a);
        }
        Attack = false;
    }

    public IEnumerator phaseOneSpecialAttack()
    {
        yield return new WaitForSeconds(phaseOneWaitTime);
        int a = Random.Range(1, 3);
        AttackCount = 0;
        anim.SetInteger("SpecialAttack", a);
        Attack = false;
        if (a == 1)
        {
            yield return new WaitForSeconds(1f);
            SpecialAttack01.Play();
        }
    }


    public IEnumerator isHit()
    {
        yield return new WaitForSeconds(1f);
        enemyIsHit.Value = false;
    }


}
