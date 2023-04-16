using UnityEngine;

public class StateFollowPlayer : StateBaseEnemy
{
    public override void enterState(StateEnemyManeger enemy)
    {
        enemy.animEnemy.SetBool("ViewToPlayer", true);
    }
    public override void updateState(StateEnemyManeger enemy)
    {
        //trocar de estado
        //State.switchState(State.followPlayer);
        Vector3 dirToPlayer = (enemy.transform.position - enemy.player.transform.position).normalized;
        dirToPlayer.y = 0;

        Quaternion lookToPlayer = Quaternion.LookRotation(dirToPlayer, Vector3.up);
        enemy.transform.rotation = Quaternion.Lerp(enemy.transform.rotation, lookToPlayer, 2 * Time.deltaTime);

        enemy.rg.velocity = -dirToPlayer * 6;

        float distance = Vector3.Distance(enemy.transform.position, enemy.player.transform.position);
        if (distance >= 50)
        {
            enemy.switchState(enemy.idleEnemy);
        }
        if(distance <= 16)
        {
            enemy.switchState(enemy.attackEnemy);
        }
    }
}
