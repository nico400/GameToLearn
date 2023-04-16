using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateAttackEnemy : StateBaseEnemy
{
    public float timeToState = 1.60f;
    public override void enterState(StateEnemyManeger enemy)
    {
        enemy.animEnemy.Play("attack1");
        
    }
    public override void updateState(StateEnemyManeger enemy)
    {
        timeToState -= Time.deltaTime;
        if (timeToState <= 0)
        {
            timeToState = 0;
            enemy.switchState(enemy.idleEnemy);
        }
    }
}
