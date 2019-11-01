using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterRoom : MonoBehaviour
{
    private bool enterRoom = false;
    public RoomEnemy roomEnemy;

    private Transform Door;
    void Start()
    {
        Door = this.transform.parent.GetChild(0);
    }

    // Update is called once per frame
    
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if (other.tag == "Player"&&roomEnemy!=null&& enterRoom==false)
        {
            roomEnemy.enterRoom = true;          
            enterRoom = true;
        }
    }
}
