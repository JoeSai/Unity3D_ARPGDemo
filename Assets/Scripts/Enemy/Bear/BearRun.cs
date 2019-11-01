using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class BearRun : Action
{
    public Transform target;
    public SharedBool Attack = false;
    private NavMeshAgent agent;
    private Animator anim;
    private AnimatorStateInfo mStateInfo;


    public override void OnAwake()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        anim = gameObject.GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        agent.stoppingDistance = 2f;
    }

    public override TaskStatus OnUpdate()
    {

        mStateInfo = anim.GetCurrentAnimatorStateInfo(0);

        if (target == null)
        {
            return TaskStatus.Failure;
        }

        if (!mStateInfo.IsTag("Attack")&& Attack.Value==false)   
        {
            agent.isStopped = false;
            agent.SetDestination(target.position);
            anim.SetBool("Move", true);
        }
        else
        {
            agent.isStopped = true;
            anim.SetBool("Move", false);
        }

       if(!agent.pathPending && agent.remainingDistance < agent.stoppingDistance)
        {
            Quaternion look = Quaternion.LookRotation(target.position - transform.position);
            Quaternion lookLerp = Quaternion.Slerp(transform.rotation, look, Time.deltaTime * 10f);
            transform.rotation = lookLerp;

            anim.SetBool("Move", false);
            return TaskStatus.Success;
        }
        return TaskStatus.Running;
    }
}
