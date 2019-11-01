using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;

public class AttackDetectionTwo : MonoBehaviour
{

    public float fieldOfAttackAngle = 90.0f;
    private float attackDistrance = 2.0f;
    private float viewAngleStep;

    public int WeaponDetec = 0;
    public List<AttackDetectionOne> detectionOnes = new List<AttackDetectionOne>();
    [SerializeField]
    private List<Transform> enemys = new List<Transform>();

    public List<Transform> Enemys { set { enemys = value; } }


   

    // Update is called once per frame
    void Update()
    {
        DrawFieldOfView();
    
    }


    private bool DecisionEnemy(Transform enemy)
    {
        if (enemy == null) return false;
        float distance = (enemy.position - transform.position).magnitude;
        float angle = Vector3.Angle(transform.forward, enemy.position - transform.position);
        if (distance < attackDistrance && angle < fieldOfAttackAngle * 0.5f)
        {
            return true;
        }

        return false;
    }



    private void CleanList()
    {
        foreach (var item in detectionOnes)
        {
            item.OnTriggerEnemys.Clear();
        }
    }
    private void Detection()
    {
        foreach (var item in detectionOnes)
        {
            //if(DecisionEnemy(item))
            //Debug.Log(item.name);
            foreach (var item2 in item.OnTriggerEnemys)
            {
                if (DecisionEnemy(item2))
                {
                    Debug.Log(item2.name);
                    item2.GetComponent<BehaviorTree>().SetVariableValue("EnGetHit", true);
                }
                    
            }
          
        }
    }



    public void DetectionGameObject(bool isfist)
    {

       foreach (var item in detectionOnes)
       {
           if(item.gameObject.tag == "FistDetection")
           {
                if(isfist==false)
              item.gameObject.SetActive(false);
                else
               item.gameObject.SetActive(true);
            }
       }
        
    }


    void DrawFieldOfView()
    {
        viewAngleStep = fieldOfAttackAngle / 2;
        
        Vector3 forward_left = Quaternion.Euler(0, -fieldOfAttackAngle * 0.5f, 0) * transform.forward * attackDistrance;
     
        for (int i = 0; i <= viewAngleStep; i++)
        {          
            Vector3 v = Quaternion.Euler(0, (fieldOfAttackAngle / viewAngleStep) * i, 0) * forward_left;           
            Vector3 pos = transform.position + v;
            Debug.DrawLine(transform.position, pos, Color.blue);

        }
    }


    //public List<Transform> DecisionEnemys()
    //{
    //    if (enemys.Count==0||enemys==null)
    //    {
    //        canAttackEnemy.Clear();
    //        return null;
    //    }


    //    foreach (var item in enemys)
    //    {
    //        float distance = (item.position - transform.position).magnitude;
    //        float angle = Vector3.Angle(transform.forward, item.position - transform.position);
    //        if (distance < attackDistrance && angle < fieldOfAttackAngle * 0.5f)
    //        {
    //            if (canAttackEnemy.Contains(item) == false)
    //            {      
    //               canAttackEnemy.Add(item); 
    //            }

    //        }
    //        else
    //        {
    //            canAttackEnemy.Remove(item);
    //        }
    //    }

    //    return canAttackEnemy;
    //}

}
