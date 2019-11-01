using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class BearAttack : Action
{
    private Animator anim;
    public SharedBool C_Attack =false;

    public override void OnAwake()
    {
        anim = gameObject.GetComponent<Animator>();
    }
    public override TaskStatus OnUpdate()
    {

        if (C_Attack.Value==true)
        {  
            StartCoroutine(DelayFuc());

            return TaskStatus.Success;
        }
     else
       return TaskStatus.Failure;
       
    }

    public IEnumerator DelayFuc()
    {
        yield return new WaitForSeconds(2.3f);
        int a = Random.Range(1, 4);
        anim.SetInteger("Attack", a);      
    }
}
