using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Dictionary<DataID, Dictionary<int,int>> dataIDToPollution = new Dictionary<DataID, Dictionary<int, int>>();

    /// <summary>
    /// Time passed since the level started
    /// </summary>
    public float time = 0;

    /// <summary>
    /// To make the time go faster so that the player doesnt have to wait.
    /// </summary>
    public float timeMultiplier = 1;

    /// <summary>
    /// Days passed since the game began. 2 min is 1 day (1 min is day and night)
    /// </summary>
    private int daysPassed=0;
    public int totalDaysPassed = 0;

    /// <summary>
    /// 10 days passed would give you 1 skill point which you can put in the research tree
    /// </summary>
    public int skillPoints = 0;

    private Dictionary<int, int> levelToPolutionOutput;
    public Dictionary<DataID, int> currentResources = new Dictionary<DataID, int>();

    public GameManager()
    {
        PopulatePollutionEconomy();       
    }
    
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

    }

    private void FixedUpdate()
    {

        // Deals with Time
        UpdateTime();
        
    }

    /// <summary>
    /// Updates the Time with framerate
    /// </summary>
    private void UpdateTime()
    {
        time += Time.deltaTime * timeMultiplier;

        if (time > 120.0f)
        {
            daysPassed++;
            totalDaysPassed = daysPassed;
            time = 0;
        }

        if(daysPassed>=10)
        {
            skillPoints++;
            totalDaysPassed = daysPassed;
            daysPassed = 0;
        }
            

    }

    void PopulatePollutionEconomy()
    {
        // Json file will be inputed

    }

    DataID GetDID(GameObject gameObject)
    {
        return gameObject.GetComponent<DataID>();
    }

}
