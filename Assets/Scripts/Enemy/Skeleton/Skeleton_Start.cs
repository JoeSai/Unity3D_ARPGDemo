using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class Skeleton_Start : Action
{
    public float waitTime;
    private float WaitTime2 = 1.5f;
    private float startTime;
    public SharedBool  IsWake=false;
    private Animator anim;
       public override void OnAwake(){

       anim=gameObject.GetComponent<Animator>();
        startTime = Time.time;
        
        }


        public override TaskStatus OnUpdate()
        {
           if(IsWake.Value==true)
           return TaskStatus.Success;

        if(waitTime + startTime+ WaitTime2 < Time.time)
        {
            IsWake.Value = true;           
            return TaskStatus.Success;
        }
        else if (waitTime + startTime < Time.time)
        {
            anim.SetBool("IsWakeUp", true);
            return TaskStatus.Running;
         }
        else
            return TaskStatus.Running;
        }

}
