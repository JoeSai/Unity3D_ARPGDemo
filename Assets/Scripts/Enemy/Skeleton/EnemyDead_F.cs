using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class EnemyDead_F : Conditional
{
    public SharedBool isDeath=false;

    private Animator anim;
    private ParticleSystem deathParticle;
    private bool isdeath = false;

    public override void OnAwake()
    {
        anim = gameObject.GetComponent<Animator>();
        deathParticle = this.transform.Find("DeathParticle").GetChild(0).GetComponent<ParticleSystem>();
        //room = roomEnemy.Value.GetComponent<RoomEnemy>();



        //foreach (Transform child in this.transform.GetComponentsInChildren<Transform>(true))
        //{
        //    if (child.tag == "Dissolve")
        //    {
        //        materials.Add(child.GetComponent<Renderer>().material);

        //    }
        //}
    }

    public override TaskStatus OnUpdate()
    {
        if(isDeath.Value==true){
            anim.SetTrigger("Die");
               deathParticle.Play();
            return TaskStatus.Success;
        }else
        return TaskStatus.Failure;
    }
}
