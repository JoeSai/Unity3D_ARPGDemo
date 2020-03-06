using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NPBehave;
using DG.Tweening;

public class EnemyDead_S_2 : Task
{
    private bool isDeath = false;
    private Transform roomEnemy;
    private Transform transform;

    private Animator anim;
    private ParticleSystem deathParticle;
    private List<Material> materials = new List<Material>();
    private bool isdeath = false;
    private RoomEnemy room;
    private System.Action<IEnumerator> StartCoroutine;

    public EnemyDead_S_2(SkeletonBehaviorTree skeletonBehaviorTree) : base("EnemyDead_S_2")
    {
        anim = skeletonBehaviorTree.Anim;
        transform = skeletonBehaviorTree.transform;
        deathParticle = this.transform.Find("Death_Particle").GetComponent<ParticleSystem>();
        room = roomEnemy.GetComponent<RoomEnemy>();
        StartCoroutine = skeletonBehaviorTree.StartCoroutine_BehaviorTree;

        foreach (Transform child in this.transform.GetComponentsInChildren<Transform>(true))
        {
            if (child.tag == "Dissolve")
            {
                materials.Add(child.GetComponent<Renderer>().material);

            }
        }
    }

    protected override void DoStart()
    {
        if (isDeath == true)
        {
            anim.SetTrigger("Die");
            Dissolve();
            deathParticle.Play();
            this.Stopped(true);
            return;
        }
        else
        {
            this.Stopped(false);
        }
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
        
    }


}
