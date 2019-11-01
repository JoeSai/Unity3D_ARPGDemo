using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class SkeletonRun : Action
{
    //public SharedBool isRunning = false;
   
    //public bool Cattack;
    public SharedVector3 target1;
    public SharedBool Attack = false;
    private Transform target;
    private NavMeshAgent agent;
    private Animator anim;
    private AnimatorStateInfo mStateInfo;


    public override void OnAwake()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        anim = gameObject.GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        agent.stoppingDistance = 0.8f;
    }

    public override TaskStatus OnUpdate()
    {

        mStateInfo = anim.GetCurrentAnimatorStateInfo(0);

        if (target1.Value == Vector3.zero||target1==null) return TaskStatus.Failure;

        if (mStateInfo.IsTag("Attack") == false&&Attack.Value==false && mStateInfo.IsTag("GetHit") == false)
        {
            agent.isStopped = false;
            agent.SetDestination(target1.Value);
            anim.SetBool("Move", true);
        }
        else
        {
            agent.isStopped=true;
            anim.SetBool("Move", false);
        }
     
        //if ((target.position - transform.position).magnitude <= 2.0f)
       if(!agent.pathPending && agent.remainingDistance < agent.stoppingDistance)
        {
            Quaternion look = Quaternion.LookRotation(target.position-transform.position);
            Quaternion lookLerp = Quaternion.Slerp(transform.rotation, look, Time.deltaTime *10f);
            transform.rotation = lookLerp;

            anim.SetBool("Move", false);
            if (Attack.Value == false)
                agent.stoppingDistance=0.5f;
            else
                agent.stoppingDistance = 0.8f;
            return TaskStatus.Success;
        }

        return TaskStatus.Running;
    }
    

}
