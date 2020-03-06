using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NPBehave;

public class Skeleton_Start_2 : Task
{
    private float startTime;
    private float waitTime;
    private float waitTime_2 = 2.0f;
    private Animator anim;
    private bool isWake = false;
    public Skeleton_Start_2(SkeletonBehaviorTree skeletonBehaviorTree) : base("Skeleton_Start_2")
    {
        anim = skeletonBehaviorTree.Anim;
        startTime = Time.time;

    }

    protected override void DoStart()
    {

        if (isWake == true)
        {
            this.Stopped(isWake);
            return;
        }
        if (waitTime + startTime + waitTime_2 < Time.time)
        {
            isWake = true;
            this.Stopped(isWake);
            return;
        }
        else if (waitTime + startTime < Time.time)
        {
            anim.SetBool("IsWakeUp", true);
        }
        this.Stopped(isWake);
    }
}
