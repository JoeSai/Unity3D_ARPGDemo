using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YM.Dungen;

public class EnemyIns 
{

    public GameObject Bear;
    public GameObject FrieDemon;
    public GameObject Skeleton_N;
    public Transform BossPos;
    private List<Transform[]> insPos1 = new List<Transform[]>();
    private List<Transform[]> insPos2 = new List<Transform[]>();
    private List<RoomEnemy> roomEne = new List<RoomEnemy>();
    private List<SkeletonrInitial> skeletonrIn = new List<SkeletonrInitial>();
    int roomEneCount = 0;

    private List<Transform> TestTrans_S = new List<Transform>();
    private List<Transform> TestTrans_B = new List<Transform>();

    

    public int EnemyGenerate(List<Room> rooms)
    {
        int count = 0;
        foreach (var item in rooms)
        {
            if (item.gameObject.tag == "BattleRoom_S")
            {
                TestTrans_S.Add(item.transform);
                count++;
            }
            if(item.gameObject.tag == "BattleRoom_B")
            {
                TestTrans_B.Add(item.transform);
                count++;
            }
        }
        generatingLocation1(TestTrans_S);
        generatingLocation2(TestTrans_B);
        return count;
    }

    /// <summary>
    /// 生成敌人类型1
    /// </summary>
    /// <param name="Rooms"></param>
    public void generatingLocation1 (List<Transform> Rooms)
    {
        foreach (var item in Rooms)
        {
            RoomEnemy Re = item.gameObject.AddComponent(typeof(RoomEnemy)) as RoomEnemy;
            roomEne.Add(Re);

            GetCollisionDetection(item.transform.Find("CollisionDetection").GetComponentsInChildren<EnterRoom>(), Re);

            SkeletonrInitial SI = item.gameObject.AddComponent(typeof(SkeletonrInitial)) as SkeletonrInitial;
            skeletonrIn.Add(SI);
            Transform EnemyPos = item.gameObject.transform.Find("EnemyPos");
            SkeletonPhys Sp = EnemyPos.gameObject.AddComponent(typeof(SkeletonPhys)) as SkeletonPhys;

            SI.skeletonPhys = Sp;

            insPos1.Add(EnemyPos.GetComponentsInChildren<Transform>());
        }
        foreach (var item in insPos1)
        {

            int a = Random.Range(1, 4);
            Transform[] enemys = new Transform[a];
            int[] k = randomPos(a, item.Length-1);
            for (int i = 0; i < a; i++)
            {
                enemys[i] = InsEnemy1(item[k[i]]);
            }
            roomEne[roomEneCount].getBehaviorTree(enemys);
            skeletonrIn[roomEneCount].getTransform(enemys);
            roomEneCount++;
        }
    }
    /// <summary>
    /// 生成敌人2
    /// </summary>
    /// <param name="Rooms"></param>
    public void generatingLocation2(List<Transform> Rooms)
    {
        foreach (var item in Rooms)
        {
            RoomEnemy Re = item.gameObject.AddComponent(typeof(RoomEnemy)) as RoomEnemy;
            roomEne.Add(Re);
            GetCollisionDetection(item.transform.Find("CollisionDetection").GetComponentsInChildren<EnterRoom>(), Re);

            Transform EnemyPos = item.gameObject.transform.Find("EnemyPos");

            insPos2.Add(EnemyPos.GetComponentsInChildren<Transform>());
        }
        //foreach (var item in insPos2)
        //{
        //    int a = 2;
        //    Transform[] enemys = new Transform[a];
        //    int[] k = randomPos(a);
        //    for (int i = 0; i < a; i++)
        //    {
        //        enemys[i] = InsEnemy2(item[k[i]]);
        //    }
        //    roomEne[l++].getBehaviorTree(enemys);

        //}

        for (int i = 0; i < insPos2.Count; i++)
        {
            int a = 2;
            Transform[] enemys = new Transform[a];
            int[] k = randomPos(a,4);
            for (int j = 0; j < a; j++)
            {
                enemys[j] = InsEnemy2(insPos2[i][k[j]]);
                //BearInitial BI = enemys[j].gameObject.AddComponent(typeof(BearInitial)) as BearInitial;
            }
         
            roomEne[roomEneCount + i].getBehaviorTree(enemys);
        }
    }


    public GameObject generatingBoss()
    {
        GameObject Boss = GameObject.Instantiate(FrieDemon, BossPos.position, Quaternion.Euler(0f, 180f, 0f));
        Boss.transform.parent = BossPos;
        Boss.SetActive(false);
        return Boss;
    }


    Transform InsEnemy1(Transform transform)
    {
        GameObject enemy = GameObject.Instantiate(Skeleton_N, transform.position, Quaternion.identity);
        enemy.transform.parent = transform;
        return enemy.transform;
    }
    Transform InsEnemy2(Transform transform)
    {
        GameObject enemy = GameObject.Instantiate(Bear, transform.position, transform.rotation);
        enemy.transform.parent = transform;
        return enemy.transform;
    }


     void InsEnemy3(Transform transform)
    {
        GameObject enemy = GameObject.Instantiate(FrieDemon, transform.position ,Quaternion.identity);
        enemy.transform.parent = transform;
    }

    private void GetCollisionDetection(EnterRoom[] enterRooms,RoomEnemy roomEnemy)
    {
        foreach (var item in enterRooms)
        {
            item.roomEnemy = roomEnemy;
        }
    }
    int[] randomPos(int randomCount,int randomNum)
    {
        int size = randomNum;
        int[] index = new int[size];
        for (int i = 0; i < size; i++)
        {
            index[i] = i + 1;
        }
        int[] array = new int[randomCount];
        int idx;
        for (int j = 0; j < randomCount; j++)
        {
            idx = Random.Range(0, size);
            array[j] = index[idx];
            index[idx] = index[size - 1];
            size--;
        }
        return array;




        //int[] m = new int[a];
        //for (int i = 0; i < a; i++)
        //{
        //    int k = Random.Range(1, 10);
        //    for (int j = 0; j < i; j++)
        //    {
        //        if (k == m[j])
        //        {
        //            i--;
        //            break;
        //        }
        //        m[i] = k;
        //    }
        //}
        //return m;
    }

   


}
