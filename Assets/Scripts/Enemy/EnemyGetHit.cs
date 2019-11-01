using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class EnemyGetHit : Action
{

    public SharedInt damageValue;

    public SharedBool isWake;

    public SharedInt EnemyHp;
    public SharedBool enemyDeath = false;
    public SharedBool enemyHit=false;
    private Animator  anim;
    private Image BloodBar;
    private bool trigger = false;
    private ParticleSystem BloodParticle;

    private float Hp;
    public override void OnAwake(){
        
        anim=gameObject.GetComponent<Animator>();
        BloodBar = this.transform.Find("Canvas").GetChild(0).GetComponent<Image>();
        BloodBar.gameObject.SetActive(true);
        BloodParticle = this.transform.Find("BloodParticle").GetComponent<ParticleSystem>();
        Hp = EnemyHp.Value;
    }
   public override TaskStatus OnUpdate(){

      if(enemyHit.Value==true){
         anim.SetTrigger("GetHit");
           ReduceHPValue();
           BloodParticle.Play();
           enemyHit.Value=false;
        }

        return TaskStatus.Failure;
    
   }
    private void ReduceHPValue()
    {
        EnemyHp.Value -= damageValue.Value;
        BloodBar.fillAmount = EnemyHp.Value / Hp;

        if (EnemyHp.Value <= 0)
        {
            enemyDeath.Value = true;
        }
    }

}
