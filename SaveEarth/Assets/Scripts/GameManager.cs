using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public DataIDList dataIDList;
    public List<CostProgression> costProg;
    public List<PollutionProgression> polProg;
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

    /// <summary>
    /// Total Pollution Value
    /// </summary>
    public int pollutionValue = 0;

    public float health = 100;
    public Text pollutionOutputText;
    public Text daysPassedText;
    public Text skillPointsText;


    public HealthBar healthbar;
    private Dictionary<int, int> levelToPolutionOutput;
    public Dictionary<DataID, int> currentResources = new Dictionary<DataID, int>();

    void Start()
    {
        if (instance == null)
        {
            instance = this;
            dataIDList = new DataIDList();
            dataIDList = CSVImportTool.dataIDs;
            costProg = CSVImportTool.progressionList.costProgs;
            polProg = CSVImportTool.progressionList.polProgs;
            healthbar = new HealthBar();
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
        pollutionOutputText.text = "Current Pollution Output: "+pollutionValue+"/day";
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
            totalDaysPassed++;
            daysPassedText.text = "Days Survived: " + totalDaysPassed;
            health -= ((float)pollutionValue / 20);
            healthbar.SetHealth(health);
            ResourceManager.instance.baseProductionRate = 0;
            ResourceManager.instance.HandleResourcesOutput();
            time = 0;
        }

        if(daysPassed>=30)
        {
            skillPoints++;
            skillPointsText.text = "Skill Points: " + skillPoints;
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

    public void IncreaseTimeBy(int multiplier)
    {
        timeMultiplier *= multiplier;
    }

    public void SetTimeToDefault()
    {
        timeMultiplier = 1;
    }

    public void SkillToBaseProduction()
    {
        if(skillPoints>0)
        {
            ResourceManager.instance.baseProductionRate += 3;
            skillPoints--;
        }
    }
}
