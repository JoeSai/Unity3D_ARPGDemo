using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class EnemyDead_B : Conditional
{
    public SharedBool isDeath=false;
    public SharedTransform roomEnemy;
    public  SharedMaterial material;
    private Animator anim;
    private ParticleSystem deathParticle;
   
    private bool isdeath = false;
    private RoomEnemy room;

    public override void OnAwake()
    {
        anim = gameObject.GetComponent<Animator>();
        deathParticle = this.transform.Find("Death_Particle").GetComponent<ParticleSystem>();
        room = roomEnemy.Value.GetComponent<RoomEnemy>();
 
    }

    public override TaskStatus OnUpdate()
    {
        if(isDeath.Value==true){
            anim.SetTrigger("Die");
            Dissolve();
               deathParticle.Play();
            return TaskStatus.Success;
        }else
        return TaskStatus.Failure;
    }


    private void Dissolve()
    {
        material.Value.DOFloat(1, "_dissolveValue", 3.5f).OnComplete(() => { room.EnemyDeath(this.GetComponent<BehaviorTree>()); });
                 
    }




}
