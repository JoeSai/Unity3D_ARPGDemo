using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;

public class EnemyManage : BaseManager
{
    private GameObject Map;
    private GameObject boss;
    private EnemyIns enemyIns = new EnemyIns();
    private bool BossOpen = false;

    public EnemyManage(GameFacade facade) : base(facade)
    {
        Map = facade.Map;
    }
    public override void OnInit()
    {
        base.OnInit();
        enemyIns.Skeleton_N= Resources.Load("Prefabs/Enemy/Skeleton_Normal") as GameObject;
        enemyIns.Bear = Resources.Load("Prefabs/Enemy/Bear") as GameObject;
        enemyIns.FrieDemon = Resources.Load("Prefabs/Enemy/FireDemon") as GameObject;
        enemyIns.BossPos = GameObject.Find("BossPos").transform;
        GetEnemyRoom = enemyIns.EnemyGenerate;
        boss=enemyIns.generatingBoss();

    }

    public override void Update()
    {
        base.Update();


        if (facade.RoomCount==0&& BossOpen==false)
        {
            BossOpen = true;
            boss.SetActive(true);
            facade.Coroutines(Test());
            Transform player = GameObject.FindGameObjectWithTag("Player").transform;
            Transform endPos = GameObject.Find("EndLocation").transform;
            player.transform.parent = endPos;
            player.transform.localPosition = Vector3.zero;
        }
    }

    private IEnumerator Test()
    {
        BehaviorTree behaviorTree = boss.GetComponent<BehaviorTree>();
        yield return new WaitForSeconds(2.5f);
        behaviorTree.EnableBehavior();
    }

}
