using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;

public class SkeletonrInitial : MonoBehaviour
{
    public SkeletonPhys skeletonPhys;
    private RoomEnemy roomEnemy;
    [SerializeField]
    private BehaviorTree[] behaviors;
    private Transform[] skeleTrans;
    private Transform Player;

    void Start()
    {
        skeletonPhys.EnemyCount = skeleTrans.Length;
        roomEnemy = GetComponent<RoomEnemy>();      
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        upBehaviors();
        roomEnemy.upEnemyCount(upBehaviors);
    }
    private void Update()
    {
        if (roomEnemy.startBattle&&behaviors!=null)
        {
            skeletonPhys.RayStart = true;
            int i = 0;
            foreach (var item in behaviors)
            {
                if (skeletonPhys.enemyPos != null&& behaviors.Length != 1)
                {
                    item.SetVariableValue("target1", skeletonPhys.enemyPos[i]);
                }
                else if (behaviors.Length == 1)
                {
                      
                     Vector3 dir= ((item.transform.position - Player.position).normalized * 1.2f) + Player.position;
                    item.SetVariableValue("target1", dir);

                    Debug.DrawLine(Player.position, dir, Color.green);
                }
                else
                {
                    Vector3 dir = ((item.transform.position - Player.position).normalized * 1.2f) + Player.position;
                    item.SetVariableValue("target1", dir);
                }
                i++;
            }
        }

    }

    public BehaviorTree[] behaviorTrees { get { return behaviors; } set { behaviors = value ; } }

    public void getTransform(Transform[] enemy)
    {
        skeleTrans = new Transform[enemy.Length];
        enemy.CopyTo(skeleTrans, 0);
    }

    public void upBehaviors( )
    {
        if (roomEnemy.getThisTrans().Count == 0)
        {
            Destroy(skeletonPhys.gameObject);
            behaviors = null;
            skeletonPhys = null;
            return;
        }
        behaviors = new BehaviorTree[roomEnemy.getThisTrans().Count];
        roomEnemy.getThisTrans().CopyTo(behaviors, 0);
        skeletonPhys.EnemyCount =roomEnemy.getThisTrans().Count;
    }

}
