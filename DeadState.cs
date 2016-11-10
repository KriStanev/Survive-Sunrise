using UnityEngine;
using System.Collections;

public class DeadState : State 
{
    private Enemy enemy;
    float timeToDisappear = 0;

    public void run()
    {
        timeToDisappear += enemy.getDeltaTime();

        if (timeToDisappear > 10.0f)
        {
            enemy.gameObject.SetActive(false);
        }

    }

    public DeadState(Enemy t_enemy)
    {
        enemy = t_enemy;
    }

}
