using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine  {

    public Dictionary<int, StateBase> m_StateCache;
    public StateBase m_prviousState;
    public StateBase m_currentState;

    public StateMachine(StateBase beginState)
    {
        m_prviousState = null;
        m_currentState = beginState;
        m_StateCache = new Dictionary<int, StateBase>();
        AddState(beginState);
        m_currentState.OnEnter();
    }

    public void AddState(StateBase state)
    {
        if (!m_StateCache.ContainsKey(state.ID))
        {
            m_StateCache.Add(state.ID, state);
            state.machine = this;
        }
    }

    //切换状态
    public void TranslateState(int id)
    {
        if (!m_StateCache.ContainsKey(id))
        {
            return;
        }

        m_prviousState = m_currentState;
        m_currentState = m_StateCache[id];
        m_currentState.OnEnter();
    }
    //保持状态
    public void Update()
    {
        if (m_currentState != null)
        {        
            m_currentState.OnStay();
        }
    }
}
