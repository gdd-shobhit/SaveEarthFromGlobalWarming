using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireFlyActivator : MonoBehaviour
{
    public GameObject firefly;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.instance.currentTimeOfTheDay == GameManager.TimeOfTheDay.Night)
        {
            if(!firefly.activeSelf)
            {
                firefly.SetActive(true);    
            }
        }

        else
        {
            if (firefly.activeSelf)
            {
                firefly.SetActive(false);
            }
        }
    }
}
