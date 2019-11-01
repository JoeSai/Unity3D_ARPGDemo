using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class BossRun : Action
{
    public SharedBool phaseTwo;

    public Transform target;
    public float speed;

    private NavMeshAgent agent;
    private Animator anim;
    private AnimatorStateInfo mStateInfo;


    public override void OnAwake()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        anim = gameObject.GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        agent.stoppingDistance = 2.8f;
    }

    public override TaskStatus OnUpdate()
    {

        mStateInfo = anim.GetCurrentAnimatorStateInfo(0);


        if (target == null) return TaskStatus.Failure;

        if (!phaseTwo.Value)
        {
            if (!mStateInfo.IsTag("Attack") && !mStateInfo.IsTag("SpecialAttack")
            && !mStateInfo.IsTag("Special"))
            {
                agent.isStopped = false;
                agent.SetDestination(target.position);
                anim.SetBool("Walk", true);
            }
        }
        if (phaseTwo.Value)
        {
            if (!mStateInfo.IsTag("ComboAttackOne") && !mStateInfo.IsTag("ComboAttackTwo") && !mStateInfo.IsTag("Special"))
            {
                agent.isStopped = false;
                agent.SetDestination(target.position);
                anim.SetBool("Walk", true);
            }
            else
            {
                anim.SetBool("Walk", false);
                agent.isStopped = true;
            }
        }

        if (!agent.pathPending && agent.remainingDistance < agent.stoppingDistance)
        {
            agent.isStopped = true;
            Quaternion look = Quaternion.LookRotation(target.position - transform.position);
            Quaternion lookLerp = Quaternion.Slerp(transform.rotation, look, Time.deltaTime * 5f);
            transform.rotation = lookLerp;
            anim.SetBool("Walk", false);
            return TaskStatus.Success;
        }
        return TaskStatus.Running;


    }
}