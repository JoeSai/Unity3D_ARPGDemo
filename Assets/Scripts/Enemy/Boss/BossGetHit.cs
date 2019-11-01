using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class BossGetHit : Conditional
{
   public SharedBool GetHit = false;
    public SharedBool isHit = false;
    public SharedBool isPhaseTwo=false;
    public SharedBool Death = false;
    public SharedInt hitCount = 0;
   private Animator  anim;
    private BossHp bossHp;
    private AnimatorStateInfo animatorInfo;
    private ParticleSystem bloodParticle;
    private ParticleSystem special;

    public override void OnAwake(){        
        anim=gameObject.GetComponent<Animator>();
        bloodParticle = transform.Find("BloodParticle").GetComponent<ParticleSystem>();
        special = transform.Find("Special").GetChild(0).GetComponent<ParticleSystem>();
        bossHp = transform.Find("CanvasBoss").GetChild(0).GetComponent<BossHp>();     
  }
   public override TaskStatus OnUpdate(){

        animatorInfo = anim.GetCurrentAnimatorStateInfo(0);

        if (isPhaseTwo.Value == true&& GetHit.Value == true && !animatorInfo.IsTag("Special")) {

            bossHp.ChangeHP(20);
            bloodParticle.Play();
            if(bossHp.CurrentHP <= 0)
            {
                Death.Value = true;
            }
            GetHit.Value = false;
            return TaskStatus.Success;
        }
     
      else if(GetHit.Value==true&&hitCount.Value<3&& !animatorInfo.IsTag("Special"))
        {
            anim.SetTrigger("GetHit");
            bloodParticle.Play();
            hitCount.Value++;
            bossHp.ChangeHP(30);
            if (bossHp.CurrentHP < 160)
            {
                isPhaseTwo.Value = true;
                StartCoroutine(SpecialParticlePlay());
            }
            isHit.Value = true;
            GetHit.Value=false;      
         return TaskStatus.Success;
      }
        else     
       return TaskStatus.Failure;
    
    
   }


    private IEnumerator SpecialParticlePlay()
    {
        yield return new WaitForSeconds(4.5f);
        special.Play();
    }


}
