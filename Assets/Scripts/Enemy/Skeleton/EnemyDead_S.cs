using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class EnemyDead_S : Conditional
{
    public SharedBool isDeath=false;
    public SharedTransform roomEnemy;

    private Animator anim;
    private ParticleSystem deathParticle;
    private List<Material> materials = new List<Material>();
    private bool isdeath = false;
    private RoomEnemy room;

    public override void OnAwake()
    {
        anim = gameObject.GetComponent<Animator>();
        deathParticle = this.transform.Find("Death_Particle").GetComponent<ParticleSystem>();
        room = roomEnemy.Value.GetComponent<RoomEnemy>();



        foreach (Transform child in this.transform.GetComponentsInChildren<Transform>(true))
        {
            if (child.tag == "Dissolve")
            {
                materials.Add(child.GetComponent<Renderer>().material);

            }
        }
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
        foreach (var item in materials)
        {
            item.DOFloat(1, "_dissolveValue", 3.5f);          
        }
        StartCoroutine(DeleteEnemy());
    }
    private IEnumerator DeleteEnemy()
    {
        yield return new WaitForSeconds(3.5f);
        room.EnemyDeath(this.GetComponent<BehaviorTree>());
    }
    




}
