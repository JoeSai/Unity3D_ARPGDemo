using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class BossPhaseTwoAttack : Action
{
    public SharedBool cAttcak = false;
    public float phaseTowWaitTime;
    //private float startTime;
    private Animator anim;
    private bool Attack = false;
    private AnimatorStateInfo animatorInfo;

    public override void OnAwake()
    {

        anim = gameObject.GetComponent<Animator>();
    }
    public override void OnStart()
    {

    }

    public override TaskStatus OnUpdate()
    {
        animatorInfo = anim.GetCurrentAnimatorStateInfo(0);

        if (animatorInfo.normalizedTime > 0.8f)
        {
            if (animatorInfo.IsTag("ComboAttackOne"))
                anim.SetInteger("Combo", 0);
        }

        if (cAttcak.Value == true)
        {
            anim.SetBool("Walk", false);
            if (Attack == false && animatorInfo.IsTag("Special") == false)
            {
                Attack = true;
                StartCoroutine(phaseTwoAttack());
            }

            return TaskStatus.Success;
        }
        return TaskStatus.Failure;
    }

    public IEnumerator phaseTwoAttack()
    {
        yield return new WaitForSeconds(phaseTowWaitTime);
        if (cAttcak.Value == true)
        {
            int a = Random.Range(1, 4);
            anim.SetInteger("Combo", a);
        }

        Attack = false;
    }

    public IEnumerator phaseOneSpecialAttack()
    {
        yield return new WaitForSeconds(phaseTowWaitTime);
        int a = Random.Range(1, 3);
        anim.SetInteger("SpecialAttack", a);
        Attack = false;
    }




}
