using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using DG.Tweening;
using System;

public class RoomEnemy : MonoBehaviour
{
    public bool startBattle = false;
    public bool enterRoom = false;
    public AttackDetectionTwo PlayerAttackDec;
    [SerializeField]
    private List<BehaviorTree> behaviorsTree=new List<BehaviorTree>();
    private GamePanel gamePanel;

    private Action actions;


    private void Start()
    {
        PlayerAttackDec = GameObject.FindGameObjectWithTag("Player").GetComponent<AttackDetectionTwo>();
        gamePanel= GameObject.Find("Canvas").transform.GetChild(0).GetComponent<GamePanel>();
    }

    private void Update()
    {
        if (enterRoom&&behaviorsTree!=null)
        {
            startBattle = true;
            foreach (var item in behaviorsTree)
            { 
                if(item.gameObject.tag== "Skeleton_Normal")
                {
                    item.SetVariableValue("target1",Vector3.zero);
                }
                item.EnableBehavior();
            }

            PlayerAttackDec.Enemys = getBehaviorTranform();
            enterRoom = false;
        }
        
      
    }

    public void getBehaviorTree(Transform[] enemy)
    {       
        for (int i = 0; i < enemy.Length; i++)
        {
            behaviorsTree.Add(enemy[i].GetComponent<BehaviorTree>());

        }
        foreach (var item in behaviorsTree)
        {
            item.SetVariableValue("RoomEnemy", this.transform);
        }
    }
    
    public  List<BehaviorTree> getThisTrans()
    {
        return this.behaviorsTree;
    }
    private List<Transform> getBehaviorTranform()
    {
        List<Transform> transforms = new List<Transform>();
        foreach (var item in behaviorsTree)
        {
            transforms.Add(item.transform);
        }
        return transforms;
    }
    
    public void EnemyDeath(BehaviorTree deathItem)
    {
        if (behaviorsTree.Count > 1)
        {
            behaviorsTree.Remove(deathItem);
            Destroy(deathItem.gameObject);
            if(deathItem.gameObject.tag== "Skeleton_Normal")
            actions();          
        }
       else if (behaviorsTree.Count == 1){
            behaviorsTree.Remove(deathItem);
            Destroy(deathItem.gameObject);
            if (deathItem.gameObject.tag == "Skeleton_Normal")
              actions();
            
            GameFacade.Instance.RoomCount --;
            gamePanel.UpdateRoomCount(GameFacade.Instance.RoomCount);
        }
    }

    public void upEnemyCount(Action runner)
    {
        actions += runner;
    }

}
