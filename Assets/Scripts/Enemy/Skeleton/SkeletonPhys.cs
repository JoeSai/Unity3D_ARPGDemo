using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonPhys : MonoBehaviour
{


    public bool RayStart = false;
    public int EnemyCount;
    private Transform player;
    private Vector3 MoveDir = Vector3.zero;
    private List<Vector3> redPos = new List<Vector3>();
    private List<int> redNumber = new List<int>();
    private List<Vector3> AddPos = new List<Vector3>();
    public int[] sepNum=new int[2];
    [SerializeField]
    private bool forward = false;
    [SerializeField]
    private bool isOneGroup = false;
   
    public Vector3[] enemyPos;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
       
    }

    // Update is called once per frame
    void Update()
    {
        if (RayStart)
        {
            RayTest();
        }
      
    }

 
    
    private void GetEnemyPos(List<Vector3> Pos)
    {
        if (EnemyCount == 1)
        {
            enemyPos = new Vector3[1];
            enemyPos[0] = player.position;
            return;
        }
        if (Pos.Count < 4)
        {
            enemyPos = null;
            return;
        }
        else
        {
            enemyPos = new Vector3[EnemyCount];

            int a = Pos.Count / EnemyCount;

            try
            {
                enemyPos[0] = Pos[3];
                for (int i = 1; i < EnemyCount; i++)
                {
                    enemyPos[i] = Pos[a * i + 3]-new Vector3(0,0.2f,0);
                }
            }catch(ArgumentOutOfRangeException e)
            {
                enemyPos = null;
                Debug.Log(e); 
            }
           
        }
    }

    void RayTest()
    {

        for (int i = 0; i <= 72; i++)
        {
            RaycastHit hit;

            Vector3 v = Quaternion.Euler(0, 5 * i, 0) * player.forward * 1.5f;
            Vector3 v2 = player.position + new Vector3(0, 0.2f, 0);
            Vector3 pos = v2 + v;
            //Debug.DrawLine(v2, pos1, Color.yellow);

            if (Physics.Raycast(v2, v, out hit, 1.5f, LayerMask.GetMask(("Ray"))))
            {
                //Debug.Log(hit.collider.name);
                if (i == 0)
                {
                    forward = true;
                }
                Debug.DrawLine(v2, pos, Color.yellow);
            }
            else
            {
                if (i == 0)
                {
                    forward = false;
                }
                Debug.DrawLine(v2, pos, Color.red);
                redPos.Add(pos);
                redNumber.Add(i);
                
            }
        }
        if(isThreeGroup())
        {
            int a= ThreeGroup();

            for (int i = 0; i < sepNum[0]; i++)
            {
                try
                {
                    AddPos.Add(redPos[i]);
                }
                catch (ArgumentOutOfRangeException e)
                {
                    Debug.Log(e);
                    AddPos.Clear();
                }
            }
            for (int i = sepNum[0]+a; i < redNumber.Count; i++)
            {
                AddPos.Add(redPos[i]);
            }
        }
        else
        {
            for (int i = 0; i < redNumber.Count - 1; i++)
            {
                if ((redNumber[i] + 1) != redNumber[i + 1])
                {
                    isOneGroup = false;
                    break;
                }
                else
                {
                    isOneGroup = true;
                }
            }
            if (isOneGroup && forward)
            {
                OneGroup();

                for (int i = 0; i < redPos.Count; i++)
                {
                    AddPos.Add(redPos[i]);
                }

            }
            else if(!isOneGroup&&!forward)
            {
                TwoGroup_1();
                for (int i = 0; i < redPos.Count; i++)
                {
                    AddPos.Add(redPos[i]);
                }

            }
            else if (forward && !isOneGroup)
            {
                Vector3 v2 = player.position + new Vector3(0, 0.2f, 0);
               // Debug.Log(SignedAngleBetween(FirstLine()));
               // Debug.DrawLine(v2, FirstLine(), Color.blue);
                Debug.DrawLine(v2, v2 + player.forward, Color.blue);

                if (SignedAngleBetween(FirstLine()) < 90)
                {
                    int a= TwoGroup_3();
                    
                    for (int i = sepNum[0]-a; i < redPos.Count; i++)
                    {
                        AddPos.Add(redPos[i]);
                    }
                }
                else
                {
                    TwoGroup_2();
                    for (int i = 0; i < sepNum[1]-sepNum[0]; i++)
                    {
                        AddPos.Add(redPos[i]);
                    }
                }
            }
            else
            {
                Array.Clear(sepNum, 0, 2);

                foreach (var item in redPos)
                {
                    AddPos.Add(item);
                }
            }
        }


        GetEnemyPos(AddPos);

       // getPos();

        redNumber.Clear();
        redPos.Clear();
        AddPos.Clear();

    }
    int ThreeGroup()
    {
        int a = 0;
        int b = 0;
        Array.Clear(sepNum, 0, 2);
        sepNum[0] = -1;
        for (int i = 0; i < redNumber.Count - 1; i++)
        {
            if ((redNumber[i] + 1) != redNumber[i + 1])
            {

                if (sepNum[0] == -1)
                {
                    sepNum[0] = redNumber[i];
                    a = i;
                }
                else
                {
                    sepNum[1] = redNumber[i + 1];
                    b = i + 1;
                }
                    
            }

        }
        return b - a;
    }

    void OneGroup()
    {
        Array.Clear(sepNum, 0, 2);
        sepNum[0] = redNumber[0];
        sepNum[1] = redNumber[redNumber.Count-1];
    }

    Vector3 FirstLine()
    {    
         for (int i = 0; i < redNumber.Count - 1; i++)
         {
             if ((redNumber[i] + 1) != redNumber[i + 1])
            {
                return redPos[i];              
            }
 
         }
        return Vector3.zero;
    }

    float SignedAngleBetween(Vector3 a)
    {
        Vector3 d1 = (player.position+ player.forward+new Vector3(0,0.2f,0)).normalized;
        Vector3 n = (player.position + player.right + new Vector3(0, 0.2f, 0)).normalized;
        Vector3 d2 = a.normalized;
        float angle = Vector3.Angle(d1, d2);
        float sign = Mathf.Sign(Vector3.Dot(n, Vector3.Cross(d1, d2)));
        float signed_angle = angle * sign;
        return (signed_angle <= 0) ? 360 + signed_angle : signed_angle; ;
    }


    void TwoGroup_1()
    {
        Array.Clear(sepNum, 0, 2);
        for (int i = 0; i < redNumber.Count - 1; i++)
        {
            if ((redNumber[i] + 1) != redNumber[i + 1])
            {
                sepNum[0] = redNumber[i];
                sepNum[1] = redNumber[i + 1];
            }
        } 
    }
    void TwoGroup_2()
    {
        Array.Clear(sepNum, 0, 2);
        sepNum[0] = redNumber[0];
        for (int i = 0; i < redNumber.Count - 1; i++)
        {
            if ((redNumber[i] + 1) != redNumber[i + 1])
            {
                    sepNum[1] = redNumber[i];
            }
        }
    }
    private bool isThreeGroup()
    {
        int a = 0;
        for (int i = 0; i < redNumber.Count - 1; i++)
        {
            if ((redNumber[i] + 1) != redNumber[i + 1])
            {
                a++;
            }
        }
        if (a > 1)
            return true;
        else
            return false;
    }

    private int TwoGroup_3()
    {

        int a = 0;
        Array.Clear(sepNum, 0, 2);
        for (int i = 0; i < redNumber.Count - 1; i++)
        {
            if ((redNumber[i] + 1) != redNumber[i + 1])
            {
                sepNum[0] = redNumber[i+1];
                a = redNumber[i + 1]- redNumber[i];
            }
        }
        sepNum[1]= redNumber[redNumber.Count - 1];


        return  a+redNumber[0];
    }



   

}
