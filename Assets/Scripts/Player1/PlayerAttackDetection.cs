using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackDetection : MonoBehaviour {

    public Transform Target;
    public float SkillDistance = 3;//扇形距离
    public float SkillJiaodu =120;//扇形的角度

    private PlayerAttack playerAttack;
    void Start () {
        playerAttack = GetComponent<PlayerAttack>();

    }
	
	// Update is called once per frame
	void Update () {


        //Detection();
    }

    void Detection()
    {
        float distance = Vector3.Distance(transform.position, Target.position);//距离
        Vector3 norVec = transform.rotation * Vector3.forward;
        //Vector3 norVecDraw = transform.position + (transform.rotation * Vector3.forward) * 5;//此处*5只是为了画线更清楚,可以不要
        Vector3 temVec = Target.position - transform.position;
        //Debug.DrawLine(transform.position, norVecDraw, Color.red);//画出技能释放者面对的方向向量
        //Debug.DrawLine(transform.position, Target.position, Color.green);//画出技能释放者与目标点的连线
        //Debug.DrawLine(norVec.normalized, temVec.normalized, Color.blue);
        float DeviationAngle = Mathf.Acos(Vector3.Dot(norVec.normalized, temVec.normalized)) * Mathf.Rad2Deg;//计算两个向量间的夹角
        if (distance < SkillDistance)
        {
            if (DeviationAngle <= SkillJiaodu * 0.5f)
            {
                
            }
        }
    }
}
