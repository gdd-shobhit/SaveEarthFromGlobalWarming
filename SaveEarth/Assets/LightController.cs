using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;
using System;

public class LightController : MonoBehaviour
{
    public Volume volume;
    // Start is called before the first frame update
    public Bloom bloom;
    void Start()
    {
      volume = GetComponent<Volume>();
        if(volume.profile.TryGet<Bloom>(out bloom))
        {
            
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //bloom.intensity.value = Mathf.PingPong(Time.time * 2, 8);

        TimeOfTheDay();
    }

    private void TimeOfTheDay()
    {
        switch(GameManager.instance.currentTimeOfTheDay)
        {
            case GameManager.TimeOfTheDay.Morning: bloom.intensity.value = 1f;
                break;
            case GameManager.TimeOfTheDay.Afternoon: bloom.intensity.value = 0.4f;
                break;
            case GameManager.TimeOfTheDay.Evening: bloom.intensity.value = 1.0f;
                break;
            case GameManager.TimeOfTheDay.Night: bloom.intensity.value = 5.0f;
                break;
        }
    }
}
