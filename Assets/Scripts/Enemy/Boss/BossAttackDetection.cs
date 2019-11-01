using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;

public class BossAttackDetection : MonoBehaviour
{

    private Transform Target;
    private BehaviorTree behaviorTree;
    public float SkillDistance = 2;//扇形距离
    public float SkillJiaodu = 60;//扇形的角度
    public bool isDamage = false;
    private PlayerCtrl player;
    void Start()
    {
        Target = GameObject.FindGameObjectWithTag("Player").transform;
        player = Target.GetComponent<PlayerCtrl>();
        behaviorTree = GetComponent<BehaviorTree>();
    }




    void Detection()
    {
        behaviorTree.SetVariableValue("HitCount", 0);
        float distance = Vector3.Distance(transform.position, Target.position);//距离
        Vector3 norVec = transform.rotation * Vector3.forward;
        Vector3 temVec = Target.position - transform.position;
        float DeviationAngle = Mathf.Acos(Vector3.Dot(norVec.normalized, temVec.normalized)) * Mathf.Rad2Deg;//计算两个向量间的夹角
        if (distance < SkillDistance)
        {
            if (DeviationAngle <= SkillJiaodu * 0.5f)
            {
                isDamage = true;
                player.reduceHpValue = 15;
                player.isGetHit = true;
            }
            else
            {
                isDamage = false;
            }
        }
        else
        {
            isDamage = false;
        }
    }
    //范围
    void SpeciaAttack_1()
    {
        float distance = Vector3.Distance(transform.position, Target.position);
        if (distance < 4)
        {
            player.forceVec = transform.position;
            player.reduceHpValue = 20;
            player.isForce = true;         
        }
    }

    //二阶段
    void Anger()
    {
        float distance = Vector3.Distance(transform.position, Target.position);
        if (distance < 5)
        {
            player.forceVec = transform.position;
            player.reduceHpValue = 15;
            player.isForce = true;
        }
    }

    
}
