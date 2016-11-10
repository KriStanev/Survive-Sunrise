using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


    public class CombatState : State
    {
        private Enemy enemy;
        private StateMachine m_stateMachine;
        int nextPunch, count = 0;
        public int distance;

        public CombatState(Enemy t_enemy, StateMachine t_stateMachine)
        {
            enemy = t_enemy;
            m_stateMachine = t_stateMachine;
            nextPunch = 30 + enemy.GetRand() % 60;
        }
 
        public void run()
        {
            enemy.m_navAgent.SetDestination(m_stateMachine.getTarget().transform.position);

            distance = (int)(enemy.getDistance(enemy.transform.position, m_stateMachine.getTarget().transform.position) * 10);

            if (enemy.m_hitPoints < 1)
            {
                enemy.m_anim.Play("Die");
                m_stateMachine.setCurrentState(m_stateMachine.m_deadState);
            }

            if (m_stateMachine.getTargetType() == 0)
            {
                enemy.m_anim.SetBool("combat", false);
                m_stateMachine.setTarget(m_stateMachine.m_wanderState.changeWanderPoint());
                toWanderState();
            }
               
            if (count == nextPunch && distance < 3)
            {
                punch();
                count = 0;
                nextPunch = 30 + enemy.GetRand() % 60;
            }
            
            count++;

        }

        void toWanderState()
        {
            m_stateMachine.setCurrentState(m_stateMachine.m_wanderState);
        }

        void punch()
        {
            switch(enemy.GetRand() % 2)
            {
                case 0:
                    enemy.m_anim.Play("leftPunch");
                    m_stateMachine.getTarget().GetComponent<PlayerController>().takeDamage(14);
                    break;

                case 1:
                    enemy.m_anim.Play("rightPunch");
                    m_stateMachine.getTarget().GetComponent<PlayerController>().takeDamage(12);
                    break;
            }
        }

        public void takeDamage(int t_dmg)
        {
            if (enemy.GetRand() % 4 == 0)
            {
                enemy.m_anim.Play("Dodge");
            }
            else
            {
                enemy.m_hitPoints -= t_dmg;
            }
        }           
    }

