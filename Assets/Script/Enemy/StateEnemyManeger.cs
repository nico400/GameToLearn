using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateEnemyManeger : MonoBehaviour
{
    StateBaseEnemy currentState;
    public StateFollowPlayer followPlayer = new StateFollowPlayer();
    public StateIdleEnemy idleEnemy = new StateIdleEnemy();
    public StateAttackEnemy attackEnemy = new StateAttackEnemy();

    public GameObject player;
    public Animator animEnemy;
    public Rigidbody rg;
    public float Life = 500;

    void Start()
    {
        ArrowScript.takedamegeinEnemy += whatMyLife;

        currentState = idleEnemy;

        currentState.enterState(this);
    }

    // Update is called once per frame
    void Update()
    {
        currentState.updateState(this);
    }
    public void switchState(StateBaseEnemy state)
    {
        currentState = state;
        state.enterState(this);
    }
    float whatMyLife(float dmg)
    {
        return Life -= dmg;
    }
}
