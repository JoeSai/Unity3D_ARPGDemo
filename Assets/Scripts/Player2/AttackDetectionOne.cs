using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackDetectionOne : MonoBehaviour
{

    public bool startAttack = false;
    public List<Transform> OnTriggerEnemys = new List<Transform>();

    private AttackDetectionTwo AttackDetTwo;
    void Start()
    {
        AttackDetTwo = this.transform.root.GetChild(0).GetComponent<AttackDetectionTwo>();
        AttackDetTwo.detectionOnes.Add(this);
    }

    // Update is called once per frame
   
   

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Bear"|| other.tag == "Skeleton_Normal" || other.tag == "Boss")
        {
            if (OnTriggerEnemys.Contains(other.transform) == false)
            {              
                OnTriggerEnemys.Add(other.transform);              
            }
                
        }
    }
   

}
