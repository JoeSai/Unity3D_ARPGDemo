using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchWeaponsState : StateTemplate<PlayerCtrl>
{

    private GameObject sword;  //斧头
    private GameObject sickle; //大剑   
    private Transform weaponPositon;
    private bool inSwitchWeapon = false;
    private float TimeSwitch=1.0f;
    private float   time=0;

    public SwitchWeaponsState(int id, PlayerCtrl p) : base(id, p)
    {
        weaponPositon = GameObject.Find("StartLocation/Player1(Clone)/M_ROOT/M_CENTER/BODY1/BODY2/R_CBONE/R_ARM1/R_ARM2/R_Hand").transform;
        sword = GameObject.Instantiate(owner.Sword, weaponPositon);
        sword.gameObject.SetActive(false);
        sword.transform.parent = weaponPositon;

        sickle = GameObject.Instantiate(owner.Sickle, weaponPositon);
        sickle.gameObject.SetActive(false);
        sickle.transform.parent = weaponPositon;

        Debug.Log("装备武器，按q切换");
    }

    public override void OnEnter(params object[] args)
    {

        owner.switchingState = false;   
    }

    public override void OnStay(params object[] args)
    {
        base.OnStay(args);

        time += Time.deltaTime;
        if (inSwitchWeapon == false)
        {
            SwitchWeapon();
            inSwitchWeapon = true;
        }
        if (TimeSwitch < time)
        {
            inSwitchWeapon = false;
            owner.isSwichWeapon = false;
            time = 0;
            owner.playerState = PlayerState.Idle;
            owner.switchingState = true;
        }
    }

    private void SwitchWeapon()
    {
        switch (owner.Weapon)
        {
            case 0:
                owner.Weapon = 1;
                owner.AttackDecision.fieldOfAttackAngle = 120.0f;
                owner.AttackDecision.DetectionGameObject(false);
                owner.anim.SetInteger("SwitchWeapon", 1);
                sword.gameObject.SetActive(true);
                sickle.gameObject.SetActive(false);
                break;
            case 1:
                owner.Weapon = 2;
                owner.AttackDecision.fieldOfAttackAngle = 160.0f;
                owner.AttackDecision.DetectionGameObject(false);
                owner.anim.SetInteger("SwitchWeapon", 2);
                sword.gameObject.SetActive(false);
                sickle.gameObject.SetActive(true);
                break;
            case 2:
                owner.Weapon = 0;
                owner.AttackDecision.fieldOfAttackAngle = 90.0f;
                owner.AttackDecision.DetectionGameObject(true);
                owner.anim.SetInteger("SwitchWeapon", 0);
                sword.gameObject.SetActive(false);
                sickle.gameObject.SetActive(false);
                break;
            default:
                break;
        }
    }

}
