using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomEnemy_2 : MonoBehaviour
{
    public bool startBattle = false;
    public bool enterRoom = false;
    public AttackDetectionTwo PlayerAttackDec;
    [SerializeField]
    private List<SkeletonBehaviorTree> SkebehaviorsTree = new List<SkeletonBehaviorTree>();
    private GamePanel gamePanel;

    private Action actions;


    private void Start()
    {
        PlayerAttackDec = GameObject.FindGameObjectWithTag("Player").GetComponent<AttackDetectionTwo>();
        gamePanel = GameObject.Find("Canvas").transform.GetChild(0).GetComponent<GamePanel>();
    }

    private void Update()
    {
        if (enterRoom && SkebehaviorsTree != null)
        {
            startBattle = true;
            foreach (var item in SkebehaviorsTree)
            {
                if (item.gameObject.tag == "Skeleton_Normal")
                {
                    item.Blackboard["Target"] = Vector3.zero;
                }
                item.StartBehaviorTree();
            }

            PlayerAttackDec.Enemys = getBehaviorTranform();
            enterRoom = false;
        }


    }

    public void getBehaviorTree(Transform[] enemy)
    {
        for (int i = 0; i < enemy.Length; i++)
        {
            SkebehaviorsTree.Add(enemy[i].GetComponent<SkeletonBehaviorTree>());

        }
        foreach (var item in SkebehaviorsTree)
        {
            //item.SetVariableValue("RoomEnemy", this.transform);
            item.RoomEnemy_2 = this;
        }
    }

    public List<SkeletonBehaviorTree> getThisTrans()
    {
        return this.SkebehaviorsTree;
    }
    private List<Transform> getBehaviorTranform()
    {
        List<Transform> transforms = new List<Transform>();
        foreach (var item in SkebehaviorsTree)
        {
            transforms.Add(item.transform);
        }
        return transforms;
    }

    public void EnemyDeath(SkeletonBehaviorTree deathItem)
    {
        if (SkebehaviorsTree.Count > 1)
        {
            SkebehaviorsTree.Remove(deathItem);
            Destroy(deathItem.gameObject);
            if (deathItem.gameObject.tag == "Skeleton_Normal")
                actions();
        }
        else if (SkebehaviorsTree.Count == 1)
        {
            SkebehaviorsTree.Remove(deathItem);
            Destroy(deathItem.gameObject);
            if (deathItem.gameObject.tag == "Skeleton_Normal")
                actions();

            GameFacade.Instance.RoomCount--;
            gamePanel.UpdateRoomCount(GameFacade.Instance.RoomCount);
        }
    }

    public void upEnemyCount(Action runner)
    {
        actions += runner;
    }
}
