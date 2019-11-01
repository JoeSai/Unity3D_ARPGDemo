using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class IntComparison : Conditional
{

    public SharedBool inPhaseTwo;
    private Animator anim;
    private bool Stunned = false;
    private float Stunnedtime = 5.0f;

    public override void OnAwake()
    {

        anim = gameObject.GetComponent<Animator>();

    }


    public override TaskStatus OnUpdate()
    {

        if (inPhaseTwo.Value == true)
        {

            if (Stunned == false)
            {
                Stunned = true;
                Stunnedtime += Time.time;
                anim.SetTrigger("GetHitAndStunned");
                anim.SetInteger("SpecialAttack", 0);
                anim.SetInteger("Attack", 0);
            }
            if (Stunned == true && Stunnedtime > Time.time)
                return TaskStatus.Running;
            else
                return TaskStatus.Success;
        }
        else
        return TaskStatus.Failure;
    }
}
