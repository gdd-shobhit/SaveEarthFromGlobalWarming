using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Will keep track of resources
/// </summary>
public class ResourceManager : MonoBehaviour
{
    public GameObject resourcesReqPanel;
    public GameObject infoPanel;
    public GameObject infoPanelLabel;
    public GameObject infoPanelFood;
    public GameObject infoPanelWood;
    public GameObject infoPanelStone;
    public GameObject infoPanelMetal;
    public GameObject infoPanelGold;

    [SerializeField] private int amountOfClicks = 0;
    [SerializeField] private int currentFood = 800;
    public int currentWood = 700;
    public int currentStone = 500;
    public int currentMetal = 400;
    [SerializeField] private int currentGold = 500;
    public int foodOutput = 0;
    public int goldOutput = 0;


    public GameObject foodTM;
    public GameObject woodTM;
    public GameObject stoneTM;
    public GameObject metalTM;
    public GameObject goldTM;


    // production per min
    public int baseProductionRate;

    public static ResourceManager instance;

    void Start()
    {

        if (instance == null)
        {
            instance = this;
            baseProductionRate = 0;
            UpdateResources();
        }
        else
        {
            Destroy(gameObject);
        }

    }


    /// <summary>
    /// Displays requirements of the building
    /// </summary>
    public void DisplayInfo(string buildingName, int levelToBe)
    {
        infoPanel.SetActive(true);
        infoPanelLabel.GetComponent<TextMeshProUGUI>().text = buildingName;
        DataID did = GameManager.instance.dataIDList.FindDataID(buildingName.ToLower());
        foreach (CostProgression costProgression in GameManager.instance.costProg)
        {
            if (costProgression.actualDID == did )
            {
                Dictionary<int, int> foodLevelProg = new Dictionary<int, int>();
                Dictionary<int, int> woodLevelProg = new Dictionary<int, int>();
                Dictionary<int, int> stoneLevelProg = new Dictionary<int, int>();
                Dictionary<int, int> metalLevelProg = new Dictionary<int, int>();
                Dictionary<int, int> goldLevelProg = new Dictionary<int, int>();

                foreach (KeyValuePair<DataID, Dictionary<int, int>> something in costProgression.progression[did])
                {
                    if (something.Key.name.Equals("food"))
                    {
                        foodLevelProg = costProgression.progression[did][something.Key];
                    }
                    else if (something.Key.name.Equals("wood"))
                    {
                        woodLevelProg = costProgression.progression[did][something.Key];
                    }
                    else if (something.Key.name.Equals("stone"))
                    {
                        stoneLevelProg = costProgression.progression[did][something.Key];
                    }
                    else if (something.Key.name.Equals("metal"))
                    {
                        metalLevelProg = costProgression.progression[did][something.Key];
                    }
                    else if(something.Key.name.Equals("gold"))
                    {
                        goldLevelProg = costProgression.progression[did][something.Key];
                    }
                }
                infoPanelFood.GetComponent<TextMeshProUGUI>().text = foodLevelProg[levelToBe].ToString();
                infoPanelGold.GetComponent<TextMeshProUGUI>().text = goldLevelProg[levelToBe].ToString();
                infoPanelWood.GetComponent<TextMeshProUGUI>().text = woodLevelProg[levelToBe].ToString();
                infoPanelMetal.GetComponent<TextMeshProUGUI>().text = metalLevelProg[levelToBe].ToString();
                infoPanelStone.GetComponent<TextMeshProUGUI>().text = stoneLevelProg[levelToBe].ToString();


            }
        }
    }

    /// <summary>
    /// Takes in DID and find its requirements and tells if it meets the levelupRequirements 
    /// </summary>
    /// <param name="did"></param>
    public bool CheckRequirements(DataID did, int levelToBe)
    {
        foreach(CostProgression costProgression in GameManager.instance.costProg )
        {
            if(costProgression.actualDID == did)
            {
                Dictionary<int, int> foodLevelProg = new Dictionary<int, int>();
                Dictionary<int, int> woodLevelProg = new Dictionary<int, int>();
                Dictionary<int, int> stoneLevelProg = new Dictionary<int, int>();
                Dictionary<int, int> metalLevelProg = new Dictionary<int, int>();
                Dictionary<int, int> goldLevelProg = new Dictionary<int, int>();

                foreach (KeyValuePair<DataID,Dictionary<int,int>> something in costProgression.progression[did])
                {                                
                    if(something.Key.name.Equals("food"))
                    {
                        foodLevelProg = costProgression.progression[did][something.Key];
                    }
                    else if (something.Key.name.Equals("wood"))
                    {
                        woodLevelProg = costProgression.progression[did][something.Key];
                    }
                    else if (something.Key.name.Equals("stone"))
                    {
                        stoneLevelProg = costProgression.progression[did][something.Key];
                    }
                    else if (something.Key.name.Equals("metal"))
                    {
                        metalLevelProg = costProgression.progression[did][something.Key];
                    }
                    else if(something.Key.name.Equals("gold"))
                    {
                        goldLevelProg = costProgression.progression[did][something.Key];
                    }
                }



                if (instance.currentFood >= foodLevelProg[levelToBe] && instance.currentWood >= woodLevelProg[levelToBe]
                    && instance.currentStone >= stoneLevelProg[levelToBe] && instance.currentMetal >= metalLevelProg[levelToBe] && instance.currentGold >= goldLevelProg[levelToBe])
                {
                    
                    instance.currentFood -= foodLevelProg[levelToBe];
                    instance.currentWood -= woodLevelProg[levelToBe];
                    instance.currentStone -= stoneLevelProg[levelToBe];
                    instance.currentMetal -= metalLevelProg[levelToBe];
                    instance.currentGold -= goldLevelProg[levelToBe];
                    UpdateResources();
                    return true;
                }
                
                else
                {
                    instance.resourcesReqPanel.SetActive(true);
                    return false;
                }
            }
        }

        return false;
    }

    public void UpdateResources()
    {
        foodTM.GetComponent<TextMeshProUGUI>().text = instance.currentFood.ToString();
        woodTM.GetComponent<TextMeshProUGUI>().text = instance.currentWood.ToString();
        stoneTM.GetComponent<TextMeshProUGUI>().text = instance.currentStone.ToString();
        metalTM.GetComponent<TextMeshProUGUI>().text = instance.currentMetal.ToString();
        goldTM.GetComponent<TextMeshProUGUI>().text = instance.currentGold.ToString();
    }

    public void WoodClicker()
    {
        amountOfClicks++;
        instance.currentWood += 5;
        woodTM.GetComponent<TextMeshProUGUI>().text = instance.currentWood.ToString();
    }

    public void StoneClicker()
    {
        amountOfClicks++;
        instance.currentStone += 4;
        stoneTM.GetComponent<TextMeshProUGUI>().text = instance.currentStone.ToString();
    }

    public void MetalClicker()
    {
        amountOfClicks++;
        instance.currentMetal += 3;
        metalTM.GetComponent<TextMeshProUGUI>().text = instance.currentMetal.ToString();
    }

    /// <summary>
    /// Handles the changes in the resources when days are passed
    /// </summary>
    public void HandleResourcesOutput()
    {
        instance.currentFood += (foodOutput + baseProductionRate);
        instance.currentWood += baseProductionRate/2;
        instance.currentStone += baseProductionRate/3;
        instance.currentMetal += baseProductionRate/4;
        instance.currentGold += goldOutput;
        UpdateResources();
    }

   

}
