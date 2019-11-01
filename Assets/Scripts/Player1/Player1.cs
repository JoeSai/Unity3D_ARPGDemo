using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1 : MonoBehaviour {


    private Animator anim;
    private Rigidbody rig;
    public GameObject Sword;  //斧头
    public GameObject Sickle; //大剑
    public float velocity = 4; //速度
    public float force = 5;
    private bool isIdle = false;
    public bool isRunning = false;
    public bool isRolling = false;
    public bool isAttack = false;
   // private bool isSpecialAttack = false;
    public bool isSwitching = false;
    public int Weapon = 1;
    private AnimatorStateInfo mStateInfo;
    private Transform weaponPositon;
    private bool H_Sword=false;
    private bool H_Sickle=false;

    private int HP;



    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        rig = this.GetComponent<Rigidbody>();
    
        weaponPositon = GameObject.Find("Player/M_ROOT/M_CENTER/BODY1/BODY2/R_CBONE/R_ARM1/R_ARM2/R_Hand").transform; 

        //Debug.Log(weaponPositon.name);
        //Sword = GameObject.Find("Player/M_ROOT/M_CENTER/BODY1/BODY2/R_CBONE/R_ARM1/R_ARM2/R_Hand/TH_Axe08");
        //Sickle= GameObject.Find("Player/M_ROOT/M_CENTER/BODY1/BODY2/R_CBONE/R_ARM1/R_ARM2/R_Hand/Heavy_Full_Metal_Sword 1");
    }

    // Update is called once per frame
    void Update()
    {
        mStateInfo = anim.GetCurrentAnimatorStateInfo(0);
     
        if (mStateInfo.IsTag("Idle")) 
        {
           // rig.isKinematic = true;
            isIdle = true;
        }
        else
        {
            isIdle = false;
        }
        if (isAttack == true||isSwitching==true)
        {
            return;
        }
   
        Move();

        if (Input.GetKeyDown("space") && isAttack == false)
        {
            rig.isKinematic = false;
            isRolling = true;            
            anim.SetTrigger("RollTrigger");
        }


        if (isRolling == true)
        {
            Roll();
            StartCoroutine(DelayFuc(() => isRolling = false , 0.23f));
        }
       

        if (Input.GetKeyDown(KeyCode.R)&&H_Sickle==false)
        {
            if (H_Sword == true && Weapon == 1)
            {
                Sword.gameObject.SetActive(false);
            }
            H_Sickle = true;
            Sickle = Instantiate(Sickle, weaponPositon);
            Sickle.gameObject.SetActive(true);
            Sickle.transform.parent = weaponPositon;
            Weapon=2;
            isSwitching = true;
            anim.SetInteger("SwitchWeapon", 2);
        }
        if (Input.GetKeyDown(KeyCode.E) &&H_Sword==false)
        {
            if (H_Sickle == true && Weapon == 2)
            {
                Sickle.gameObject.SetActive(false);
            }
            H_Sword = true;
            Sword = Instantiate(Sword, weaponPositon);
            Sword.gameObject.SetActive(true);
            Sword.transform.parent = weaponPositon;
            Weapon = 1;
            isSwitching = true;
            anim.SetInteger("SwitchWeapon", 1);
        }

        if (mStateInfo.IsTag("Idle"))
        {           

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                if (H_Sickle == false && H_Sword == false)
                    return;
                Weapon = 2;
                isSwitching = true;
                SwitchWeapon();
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                if (H_Sword == false)
                    return;
                Weapon = 0;
                isSwitching = true;
                SwitchWeapon();
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                if (H_Sickle == false)
                    return;
                Weapon = 1;
                isSwitching = true;
                SwitchWeapon();
            }            
        }
        if (isSwitching == true)
        {
            StartCoroutine(DelayFuc(() => isSwitching = false, 0.9f));
        }

    }
    void Move()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 nowVel = rig.velocity;
        if (Mathf.Abs(h) > 0.5f || Mathf.Abs(v) > 0.5f)
        {
            isRunning = true;
            Vector3 dir = new Vector3(h, nowVel.y, v);
            rig.isKinematic = false;
            anim.SetBool("IsRunning", true);
            
            rig.velocity = dir * velocity;
            //transform.Translate(new Vector3(h, 0, v) * velocity * Time.deltaTime, Space.World);
            Quaternion look = Quaternion.LookRotation(dir);
            Quaternion lookLerp = Quaternion.Slerp(transform.rotation, look, Time.deltaTime * 8f);
            transform.rotation = lookLerp;
        }
        else
        {            
            anim.SetBool("IsRunning", false);
            rig.velocity = new Vector3(0, nowVel.y, 0);
            isRunning = false;
        }

    }
    void Roll()
    {      
        //transform.Translate(transform.forward * force * Time.deltaTime, Space.World);
        rig.AddForce(transform.forward * force * Time.deltaTime, ForceMode.VelocityChange);

    }
    void SwitchWeapon()
    {

        switch (Weapon)
        {
            case 0:
                Sickle.gameObject.SetActive(false);
                Sword.gameObject.SetActive(true);
                Weapon = 1;
                anim.SetInteger("SwitchWeapon", 1);
                break;
            case 1:
                Sword.gameObject.SetActive(false);
                Sickle.gameObject.SetActive(true);
                Weapon = 2;
                anim.SetInteger("SwitchWeapon", 2);
                break;
            case 2:
                Sword.gameObject.SetActive(false);
                Sickle.gameObject.SetActive(false);
                Weapon = 0;
                anim.SetInteger("SwitchWeapon", 0);
                break;
            default:
                break;
                
        }
    }


    public  IEnumerator DelayFuc(Action action, float delaySeconds)
    {
        yield return new WaitForSeconds(delaySeconds);
        action();
    }

}
