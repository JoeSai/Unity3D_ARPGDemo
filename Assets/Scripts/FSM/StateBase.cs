using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateBase  {

    public int ID { get; set; }

    public StateMachine machine;
    public StateBase (int id)
    {
        this.ID = id;
    }
    public virtual void OnEnter(params object[] args) { }
    public virtual void OnStay(params object[] args) { }
    public virtual void OnExit(params object[] args) { }
}
