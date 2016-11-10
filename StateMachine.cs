using UnityEngine;
using System.Collections;

public class StateMachine 
{
    GameObject m_target;
    int m_targetType = 0;
    State m_currentState;
    public WanderState m_wanderState;
    public CombatState m_combatState;
    public DeadState m_deadState;
    
    public StateMachine(Enemy t_enemy)
    {
        m_wanderState = new WanderState(t_enemy, this);
        m_combatState = new CombatState(t_enemy, this);
        m_deadState = new DeadState(t_enemy);
        m_currentState = m_wanderState;
    }

    public void setTarget(GameObject t_target)
    {
        m_target = t_target;
    }

    public GameObject getTarget()
    {
        return m_target;
    }

    //0 - generate new wanderPoint
    //1 - make the player the target
    //2 for retaining current target- no change of target
    public void setTargetType(int t_type)
    {
        m_targetType = t_type;
    }

    public int getTargetType()
    {
        return m_targetType;
    }

    public void setCurrentState(State t_state)
    {
        m_currentState = t_state;
    }

    public State getCurrentState()
    {
        return m_currentState;
    }

    public void runStateMachine()
    {
        m_currentState.run();
    }

}
