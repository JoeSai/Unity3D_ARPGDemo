using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {

    private EnemyAttackDetection enemyAtDt;

	// Use this for initialization
	void Start () {
        enemyAtDt = GetComponent<EnemyAttackDetection>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    void PhysicalDamage()
    {
        if (enemyAtDt.isDamage == true)
        {
            Debug.Log("造成了伤害");
        }
    }
}
