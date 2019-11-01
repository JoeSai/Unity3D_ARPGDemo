using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum PlayerState
{
    None,
    Idle,
    Runing,
    Attack,
    Roll,
    SpecialAttack,
    GetHit,
    Force,
    SwitchWeapons    
}


public class PlayerCtrl : MonoBehaviour
{

    public Animator anim;
    public CharacterController cc;

    public float Rollpow = 600;
    public int reduceHpValue = 10;
    public Vector3 forceVec;
    public PlayerState playerState = PlayerState.Idle;

    public bool switchingState = false;
    public bool isRoll = false;
    public bool isAttack = false;
    public bool isSwichWeapon=false;
    public bool isGetHit = false;
    public bool isDeath = false;
    public bool isForce = false;

    public StateMachine machine;

    public GameObject Sword;  //斧头
    public GameObject Sickle; //大剑   
    public int Weapon = 0;
    public int mHitCount = 0;  //定义角色连击次数
    public AttackDetectionTwo AttackDecision;
    public GamePanel gamePanel;
    private AnimatorStateInfo animatorInfo;
   
    

    private void Start()
    {
        anim = GetComponent<Animator>();
        cc = GetComponent<CharacterController>();
        AttackDecision = GetComponent<AttackDetectionTwo>();
        gamePanel = GameObject.Find("Canvas").transform.GetChild(0).GetComponent<GamePanel>();

        IdleState idle = new IdleState(1, this);
        RunState Runing = new RunState(2, this);
        RollState Rolling = new RollState(3, this);
        AttackState attack = new AttackState(4, this);
        SwitchWeaponsState switchWeapons = new SwitchWeaponsState(5, this);
        GetHitAndDeath getHitAndDeath = new GetHitAndDeath(6, this);
        ForceState force = new ForceState(7, this);

        machine = new StateMachine(idle);
        machine.AddState(Runing);
        machine.AddState(Rolling);
        machine.AddState(attack);
        machine.AddState(switchWeapons);
        machine.AddState(getHitAndDeath);
        machine.AddState(force);
    }
    private void Update()
    {
        animatorInfo = anim.GetCurrentAnimatorStateInfo(0);

        if (isDeath == true)  return;
           

        //点击UI退出
        if (EventSystem.current.IsPointerOverGameObject() ||
        EventSystem.current.currentSelectedGameObject != null)
        {
            return;
        }

       

        //受击
        GetHit();
        //跑动
        Run();
        //翻滚
        Roll();
        //攻击
        Attack();
        //切换武器
        SwichWeapon();
        // 击退
        force();

        if(switchingState==true)
        UpdateAnimation();
    }
   
    private void UpdateAnimation()
    {
        switch (playerState)
        {
            case PlayerState.Idle:
                machine.TranslateState(1);
                break;
            case PlayerState.Runing:
                machine.TranslateState(2);
                break;
            case PlayerState.Roll:
                machine.TranslateState(3);
                break;
            case PlayerState.Attack:
                machine.TranslateState(4);
                break;
            case PlayerState.SwitchWeapons:
                machine.TranslateState(5);
                break;
            case PlayerState.GetHit:
                machine.TranslateState(6);
                break;
            case PlayerState.Force:
                machine.TranslateState(7);
                break;
            default:
                break;
        }
    }

    private void LateUpdate()
    {
        machine.Update();
    }
 
    private void Roll()
    {
        if (gamePanel.Fp < 60)
        {
            gamePanel.Fp += (10 * Time.deltaTime);
        }
        else
        {
            gamePanel.Fp = 60;
        }

        
        if (isAttack == false && Input.GetKeyDown(KeyCode.Space)&&isSwichWeapon==false
            && gamePanel.Fp >= 20 && animatorInfo.IsTag("GetHit") == false)
        {
            isRoll = true;
            gamePanel.Fp -= 20;
            switchingState = true;
            playerState = PlayerState.Roll;
        }
    }

    private void Attack()
    {
        if (playerState != PlayerState.Runing && Input.GetMouseButtonUp(0)
            && isRoll == false && isSwichWeapon == false && animatorInfo.IsTag("GetHit") == false)
        {
            switchingState = true;
            playerState = PlayerState.Attack;
        }
    }
    private void SwichWeapon()
    {
        if (playerState == PlayerState.Idle && Input.GetKeyDown(KeyCode.Q))
        {
            switchingState = true;
            isSwichWeapon = true;
            playerState = PlayerState.SwitchWeapons;
        }
    }
    private void Run()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        if (animatorInfo.IsTag("GetHit") == false && isSwichWeapon == false && isAttack == false && isRoll == false
        && (Mathf.Abs(h) > 0.1f || Mathf.Abs(v) > 0.1))
        {
            switchingState = true;
            playerState = PlayerState.Runing;
        }
       
    }
    
    private void GetHit()
    {
        if (isGetHit && animatorInfo.IsTag("Roll") == false && isSwichWeapon==false)
        {
            switchingState = true;
            playerState = PlayerState.GetHit;
        }
    }


    private void force()
    {
        if (isForce&& forceVec!=null)
        {
            switchingState = true;
            playerState = PlayerState.Force;
        }
    }
}




