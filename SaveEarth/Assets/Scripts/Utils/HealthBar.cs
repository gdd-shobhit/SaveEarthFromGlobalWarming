using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;

    public HealthBar()
    {
        slider = GameObject.Find("PollutionMeter").GetComponent<Slider>();
    }
    public void SetMaxPollution(int pollution)
    {
        slider.maxValue = pollution;
        slider.value = pollution;
    }

    public void SetHealth(int health)
    {
        slider.value = health;
    }
}
