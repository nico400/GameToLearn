using System;
using UnityEngine;

public abstract class StateBaseEnemy
{
    public abstract void enterState(StateEnemyManeger enemy);
    public abstract void updateState(StateEnemyManeger enemy);
}
