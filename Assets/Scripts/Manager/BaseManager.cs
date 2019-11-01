using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseManager
{
    public static Func<List<YM.Dungen.Room>,int> GetEnemyRoom;
    protected GameFacade facade;
    public BaseManager(GameFacade facade)
    {
        this.facade = facade;
    }

    public virtual void OnInit() { }
    public virtual void Update() { }
    public virtual void OnDestroy() { }
}
