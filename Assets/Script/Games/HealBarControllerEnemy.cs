using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealBarControllerEnemy : MonoBehaviour
{
    public Slider BarValue;
    public StateEnemyManeger lifeEnemy;
    void Update()
    {
        BarValue.maxValue = 500;
        BarValue.value = lifeEnemy.Life;
    }
}
