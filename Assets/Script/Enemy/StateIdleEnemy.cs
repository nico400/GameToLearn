using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateIdleEnemy : StateBaseEnemy
{
    public override void enterState(StateEnemyManeger enemy)
    {
        enemy.animEnemy.SetBool("ViewToPlayer", false);
    }
    public override void updateState(StateEnemyManeger enemy)
    {
        //trocar de estado
        //State.switchState(State.followPlayer);
        float distance = Vector3.Distance(enemy.transform.position, enemy.player.transform.position);
        if (distance <= 50)
        {
            enemy.switchState(enemy.followPlayer);
        }
    }
}