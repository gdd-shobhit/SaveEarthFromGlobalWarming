using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;

    public HealthBar()
    {
        //slider = GameObject.Find("PollutionMeter").GetComponent<Slider>();
    }
    public void SetMaxPollution(int pollution)
    {
        slider.maxValue = pollution;
        slider.value = pollution;
    }

    public void SetHealth(float health)
    {
        if (health < 80)
        {
            GameManager.instance.healthLess80 = true;
        }
        else if(health <60)
        {
            GameManager.instance.healthLess60 = true;
        }

        if (GameManager.instance.healthLess60)
        {
       
            if (health <= 60)
            {
                slider.value = health;
            }
            else
            {
                slider.value = 60;
            }
        }
        else if (GameManager.instance.healthLess80)
        {
            if (health <= 80)
            {
                slider.value = health;
            }
            else
            {
                slider.value = 80;
            }
        }
        else
        {
            //if (health <= 100)
            //{
            //    slider.value = health;
            //}
            //else
            //{
            //    slider.value = 100;
            //}
        }
    }
}
