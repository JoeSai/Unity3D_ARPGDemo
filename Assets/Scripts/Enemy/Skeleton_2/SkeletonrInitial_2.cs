using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonrInitial_2 : MonoBehaviour
{
    public SkeletonPhys skeletonPhys;
    private RoomEnemy_2 roomEnemy;
    [SerializeField]
    private SkeletonBehaviorTree[] behaviors;
    private Transform[] skeleTrans;
    private Transform Player;

    void Start()
    {
        skeletonPhys.EnemyCount = skeleTrans.Length;
        roomEnemy = GetComponent<RoomEnemy_2>();
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        upBehaviors();
        roomEnemy.upEnemyCount(upBehaviors);
    }
    private void Update()
    {
        if (roomEnemy.startBattle && behaviors != null)
        {
            skeletonPhys.RayStart = true;
            int i = 0;
            foreach (var item in behaviors)
            {
                if (skeletonPhys.enemyPos != null && behaviors.Length != 1)
                {                  
                    item.Blackboard["Target"] = skeletonPhys.enemyPos[i];
                }
                else if (behaviors.Length == 1)
                {

                    Vector3 dir = ((item.transform.position - Player.position).normalized * 1.2f) + Player.position;                  
                    item.Blackboard["Target"] = dir;
                    Debug.DrawLine(Player.position, dir, Color.green);
                }
                else
                {
                    Vector3 dir = ((item.transform.position - Player.position).normalized * 1.2f) + Player.position;                    
                    item.Blackboard["Target"] = dir;
                }
                i++;
            }
        }

    }

    public SkeletonBehaviorTree[] behaviorTrees { get { return behaviors; } set { behaviors = value; } }

    public void getTransform(Transform[] enemy)
    {
        skeleTrans = new Transform[enemy.Length];
        enemy.CopyTo(skeleTrans, 0);
    }

    public void upBehaviors()
    {
        if (roomEnemy.getThisTrans().Count == 0)
        {
            Destroy(skeletonPhys.gameObject);
            behaviors = null;
            skeletonPhys = null;
            return;
        }
        behaviors = new SkeletonBehaviorTree[roomEnemy.getThisTrans().Count];
        roomEnemy.getThisTrans().CopyTo(behaviors, 0);
        skeletonPhys.EnemyCount = roomEnemy.getThisTrans().Count;
    }

}
