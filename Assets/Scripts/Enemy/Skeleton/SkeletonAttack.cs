using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
public class SkeletonAttack : Action
{

    private Animator anim;
    public SharedBool C_Attcak =false;
    private bool Attack = false;
    private AnimatorStateInfo animatorInfo;


    public override void OnAwake()
    {
        anim = gameObject.GetComponent<Animator>();
    }
    public override TaskStatus OnUpdate()
    {
        animatorInfo = anim.GetCurrentAnimatorStateInfo(0);
        if (C_Attcak.Value == false)
        {
            return TaskStatus.Failure;
        }
        if (Attack==false&&C_Attcak.Value==true&& animatorInfo.IsTag("GetHit")==false)
        {
            Attack = true;
            StartCoroutine(DelayFuc());
            return TaskStatus.Success;
        }
        else
        {
            if(animatorInfo.normalizedTime > 0.8f)
             anim.SetInteger("Attack", 0);
            return TaskStatus.Running;
        }
       

       
    }

    private IEnumerator DelayFuc()
    {    
        yield return new WaitForSeconds(2.5f);
        if (C_Attcak.Value == false && animatorInfo.IsTag("GetHit") == false)
            yield return 0;
        int a = Random.Range(1, 4);
        anim.SetInteger("Attack", a);
        Attack = false;

    }

}
