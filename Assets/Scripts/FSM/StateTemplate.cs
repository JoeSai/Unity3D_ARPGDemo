using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateTemplate<T> : StateBase {

    public T owner;

    public StateTemplate(int id,T o) : base(id)
    {
        owner = o;
    }
}
