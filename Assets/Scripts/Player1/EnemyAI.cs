using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EnemyState
{
    Sleeping,
    Idle,
    Move,
    Attack,
    GetHit,
    Die
}
public class EnemyAI : MonoBehaviour {

    private Transform Player;
    private Animator anim;
    private bool GetHit = false;
    private AnimatorStateInfo mStateInfo;
    private float distance;
    public EnemyState enemyState = EnemyState.Sleeping;
    private Rigidbody rig;
    public bool waitAttack = false;
    //private EnemyAttackDetection enemyAtDt;
    public static IEnumerator DelayFuc(Action action, float delaySeconds)
    {
        yield return new WaitForSeconds(delaySeconds);
        action();
    }
    void Start () {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        //enemyAtDt = GetComponent<EnemyAttackDetection>();
        anim = GetComponent<Animator>();
        rig = GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update () {

        mStateInfo = anim.GetCurrentAnimatorStateInfo(0);
        distance = Vector3.Distance(transform.position, Player.position);//距离
        if (distance <= 3&& enemyState == EnemyState.Sleeping)
        {            
            anim.SetTrigger("WakeUp");
            Orientation();
            //if (mStateInfo.normalizedTime >= 1f)
            //{
            //    enemyState = EnemyState.Idle;
            //}
                StartCoroutine(DelayFuc(() => enemyState = EnemyState.Idle, 1.2f));
        }

        if (enemyState == EnemyState.Idle|| enemyState == EnemyState.Attack && distance>=1.5f)
        {
            if(enemyState == EnemyState.Attack)
            {
                if(mStateInfo.normalizedTime >=0.8f)
                {
                    enemyState = EnemyState.Move;
                }
                //StartCoroutine(DelayFuc(() => enemyState = EnemyState.Move, 0.55f));
            }
            else
            {
                enemyState = EnemyState.Move;
            }
        }

        if(enemyState==EnemyState.Idle|| enemyState == EnemyState.Move&& distance <= 1.5f)
        {
            enemyState = EnemyState.Idle;
            StartCoroutine(DelayFuc(() => enemyState = EnemyState.Attack, 0.3f));            
        }
        if (Input.GetMouseButtonDown(0))
        {
            //enemyState = EnemyState.GetHit;
            //if (mStateInfo.normalizedTime >= 0.8f)
            //{
            //    enemyState = EnemyState.Idle;
            //}
           
        }
        switch (enemyState)
        {
            case EnemyState.Idle:
                anim.SetBool("Move", false);
                anim.SetInteger("Attack", 0);
                break;
            case EnemyState.Move:
                anim.SetInteger("Attack", 0);
                Move();
                break;
            case EnemyState.Attack:
                if (waitAttack == false)
                {
                    anim.SetBool("Move", false);
                    int i = UnityEngine.Random.Range(1, 4);
                    anim.SetInteger("Attack", i);
                }

                break;
            case EnemyState.GetHit:
                if (GetHit == false)
                {
                    EnemyGetHit();
                }
                if (mStateInfo.normalizedTime >= 0.8f)
                {
                    GetHit = false;
                    enemyState = EnemyState.Idle;
                }
                    break;
            case EnemyState.Die:
                break;
        }

        
    }
    void Move()
    {   
            anim.SetBool("Move", true);
            Orientation();
            rig.MovePosition(transform.position + transform.forward * 4 * Time.deltaTime);
    }
    void Orientation()
    {
        Vector3 dir = Player.position - transform.position;
        Quaternion look = Quaternion.LookRotation(dir);
        Quaternion lookLerp = Quaternion.Slerp(transform.rotation, look, Time.deltaTime * 8f);
        transform.rotation = lookLerp;
    }
    void EnemyGetHit()
    {
        GetHit = true;
        anim.SetInteger("Attack", 0);
        anim.SetBool("Move", false);
        anim.SetTrigger("GetHit");
    }
}
