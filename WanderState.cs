using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

    public class WanderState : State
    {
        Enemy enemy;
        StateMachine m_stateMachine;
        bool changed = false;

        public WanderState(Enemy t_enemy, StateMachine t_stateMachine)
        {
            enemy = t_enemy;
            m_stateMachine = t_stateMachine;
            m_stateMachine.setTarget(changeWanderPoint());
        }
    
        public void Detected(GameObject t_target)
        {
            m_stateMachine.setTarget(t_target);
        }
 
       public void run()
       {
           if (!changed)
           {
               enemy.m_navAgent.SetDestination(enemy.m_stateMachine.getTarget().transform.position);
               changed = true;
           }
           
           if (enemy.m_stateMachine.getTargetType() == 1)
           {
               m_stateMachine.setTarget(GameObject.FindGameObjectWithTag("Player"));
               enemy.m_anim.SetBool("combat", true);
               changed = false;
               toCombatState();
           }

           if (m_stateMachine.getTargetType() == 0)
           {
               m_stateMachine.setTarget(changeWanderPoint());
               changed = false;
           }
       }

       void toCombatState()
       {
           m_stateMachine.setCurrentState(m_stateMachine.m_combatState);
       }

       public GameObject changeWanderPoint()
       {
           return enemy.m_wanderPoints[enemy.GetRand() % 10];
       }
        
    }

