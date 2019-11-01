using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackDetection : MonoBehaviour
{

    private Transform Target;
    public float SkillDistance = 2;//扇形距离
    public float SkillJiaodu = 60;//扇形的角度
    public bool isDamage = false;
    private PlayerCtrl player;
    void Start()
    {
        Target = GameObject.FindGameObjectWithTag("Player").transform;
        player = Target.GetComponent<PlayerCtrl>();
    }

    
    void Update()
    {
        //Detection();
    }

    void Detection()
    {
        float distance = Vector3.Distance(transform.position, Target.position);//距离
        Vector3 norVec = transform.rotation * Vector3.forward;
        Vector3 temVec = Target.position - transform.position;
        float DeviationAngle = Mathf.Acos(Vector3.Dot(norVec.normalized, temVec.normalized)) * Mathf.Rad2Deg;//计算两个向量间的夹角
        //Debug.Log(DeviationAngle);
        if (distance < SkillDistance)
        {
            if (DeviationAngle <= SkillJiaodu * 0.5f)
            {
                isDamage = true;
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
}
