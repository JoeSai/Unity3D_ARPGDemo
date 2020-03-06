using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using NPBehave;

public class SkeletonRun_2 : Task
{
    private Animator anim;
    private NavMeshAgent meshAgent;
    private Blackboard ownBlackboard;
    private AnimatorStateInfo stateInfo;
    private Transform target;
    private Transform transform;
    private bool Attack = false;
    private Vector3 enemyTarget = Vector3.zero;
    public SkeletonRun_2(SkeletonBehaviorTree skeletonBehaviorTree) : base("SkeletonRun_2")
    {
        anim = skeletonBehaviorTree.Anim;
        meshAgent = skeletonBehaviorTree.MeshAgent;
        ownBlackboard = skeletonBehaviorTree.Blackboard;
        transform = skeletonBehaviorTree.transform;
        target = skeletonBehaviorTree.Target;
        meshAgent.stoppingDistance = 0.8f;
    }

    protected override void DoStart()
    {
        stateInfo = anim.GetCurrentAnimatorStateInfo(0);
        enemyTarget = ownBlackboard.Get<Vector3>("enemyTarget");
        Attack = ownBlackboard.Get<bool>("Attack");
        if (enemyTarget==Vector3.zero||enemyTarget!=null)
        {
            this.Stopped(false);
            return;
        }
        if (stateInfo.IsTag("Attack") == false && Attack == false && stateInfo.IsTag("GetHit") == false){
            meshAgent.isStopped = false;
            meshAgent.SetDestination(enemyTarget);
            anim.SetBool("Move", true);
        }
        else
        {
            meshAgent.isStopped = true;
            anim.SetBool("Move", false);
        }

        if (!meshAgent.pathPending && meshAgent.remainingDistance < meshAgent.stoppingDistance)
        {
            Quaternion look = Quaternion.LookRotation(target.position - transform.position);
            Quaternion lookLerp = Quaternion.Slerp(transform.rotation, look, Time.deltaTime * 10f);
            transform.rotation = lookLerp;

            anim.SetBool("Move", false);

            if (Attack == false)
                meshAgent.stoppingDistance = 0.5f;
            else
                meshAgent.stoppingDistance = 0.8f;

            this.Stopped(true);
            return;
        }
        this.Stopped(false);
    }
}
