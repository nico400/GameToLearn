using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealBarController : MonoBehaviour
{
    public Slider BarValue;
    public PlayerController lifePlayer;
    void Update()
    {
        BarValue.maxValue = 100;
        BarValue.value = lifePlayer.LifePlayer;
    }
}
