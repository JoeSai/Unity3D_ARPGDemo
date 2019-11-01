using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class CanAttackBoss : Conditional
{
    public Transform target;
    public float fieldOfAttackAngle = 70;
    public float attackDistrance =3f;
    private float viewAngleStep = 30;
    public SharedBool canAttack = false;

    private Animator anim;

    public override void OnAwake()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        anim = gameObject.GetComponent<Animator>();
        
    }

    public override TaskStatus OnUpdate()
    {
       
        if (target == null) return TaskStatus.Failure;

        float distance = (target.position - transform.position).magnitude;
        float angle = Vector3.Angle(transform.forward, target.position - transform.position);

        DrawFieldOfView();

        if (distance < attackDistrance && angle < fieldOfAttackAngle * 0.5)
        {
            //sharedTarget.Value = target;
            canAttack.Value = true;
            return TaskStatus.Success;
        }
        else
        {
            canAttack.Value = false;
            anim.SetInteger("Attack", 0);
            anim.SetInteger("SpecialAttack", 0);
            anim.SetInteger("Combo", 0);
            return TaskStatus.Failure;
        }
        
    }

    void DrawFieldOfView()

    {
        // 获得最左边那条射线的向量，相对正前方
        Vector3 forward_left = Quaternion.Euler(0, -fieldOfAttackAngle * 0.5f, 0) * transform.forward * attackDistrance;
        // 依次处理每一条射线
        for (int i = 0; i <= viewAngleStep; i++)
        {
            // 每条射线都在forward_left的基础上偏转一点，最后一个正好偏转90度到视线最右侧
            Vector3 v = Quaternion.Euler(0, (fieldOfAttackAngle / viewAngleStep) * i, 0) * forward_left;
            // Player位置加v，就是射线终点pos
            Vector3 pos = transform.position + v;
            Debug.DrawLine(transform.position, pos, Color.red);

        }
    }

}
