using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

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
    public GameObject infoPanelPollution;
    public Slider foodSlider;
    public Slider woodSlider;
    public Slider stoneSlider;
    public Slider metalSlider;
    public Slider goldSlider;
    public ResourceStorageSO resourceStorageSO;

    public BuildingSO selectedBuildingReq;

    [SerializeField] private int amountOfClicks = 0;
    [SerializeField] private int currentGold = 500;
    public int foodOutput = 0;
    public int goldOutput = 0;

    // production per min
    public int baseProductionRate;

    public static ResourceManager instance;

    void Start()
    {

        if (instance == null)
        {
            instance = this;
            baseProductionRate = 0;
            foodSlider.maxValue = resourceStorageSO.foodStorage;
            woodSlider.maxValue = resourceStorageSO.woodStorage;
            stoneSlider.maxValue = resourceStorageSO.stoneStorage;
            metalSlider.maxValue = resourceStorageSO.metalStorage;
            goldSlider.maxValue = resourceStorageSO.goldStorage;
            UpdateResources();
        }
        else
        {
            Destroy(gameObject);
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



                if (resourceStorageSO.food >= foodLevelProg[levelToBe] && resourceStorageSO.wood >= woodLevelProg[levelToBe]
                    && resourceStorageSO.stone >= stoneLevelProg[levelToBe] && resourceStorageSO.metal >= metalLevelProg[levelToBe] && resourceStorageSO.gold >= goldLevelProg[levelToBe])
                {

                    resourceStorageSO.food -= foodLevelProg[levelToBe];
                    resourceStorageSO.wood -= woodLevelProg[levelToBe];
                    resourceStorageSO.stone -= stoneLevelProg[levelToBe];
                    resourceStorageSO.metal -= metalLevelProg[levelToBe];
                    resourceStorageSO.gold -= goldLevelProg[levelToBe];
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
        foodSlider.value = resourceStorageSO.food;
        woodSlider.value = resourceStorageSO.wood;
        stoneSlider.value = resourceStorageSO.stone;
        metalSlider.value = resourceStorageSO.metal;
        goldSlider.value = resourceStorageSO.gold;
    }

    /// <summary>
    /// Handles the changes in the resources when days are passed
    /// </summary>
    public void HandleResourcesOutput()
    {
        resourceStorageSO.food += (foodOutput + baseProductionRate);
        //instance.currentWood += baseProductionRate/2;
        //instance.currentStone += baseProductionRate/3;
        //instance.currentMetal += baseProductionRate/4;
        resourceStorageSO.gold += goldOutput;
        UpdateResources();
    }

    /// <summary>
    /// WORKS!
    /// </summary>
    public void IncreaseFoodTest()
    {

        foodSlider.value = 5000;
        foodSlider.GetComponentInChildren<TextMeshProUGUI>().text = foodSlider.value.ToString();
        infoPanel.SetActive(true);
        infoPanelLabel.GetComponent<TextMeshProUGUI>().text = selectedBuildingReq.dataId.name;
        infoPanelFood.GetComponent<TextMeshProUGUI>().text = selectedBuildingReq.costProg.food_1.ToString();
        infoPanelGold.GetComponent<TextMeshProUGUI>().text = selectedBuildingReq.costProg.gold_1.ToString();
        infoPanelWood.GetComponent<TextMeshProUGUI>().text = selectedBuildingReq.costProg.wood_1.ToString();
        infoPanelMetal.GetComponent<TextMeshProUGUI>().text = selectedBuildingReq.costProg.metal_1.ToString();
        infoPanelStone.GetComponent<TextMeshProUGUI>().text = selectedBuildingReq.costProg.stone_1.ToString();

        infoPanelPollution.GetComponent<TextMeshProUGUI>().text = selectedBuildingReq.pollutionProg.levelProg[1].ToString();

        //while(foodSlider.value < foodSlider.value + 5000)
        //{
        //    foodSlider.value++;
        //    foodSlider.GetComponentInChildren<TextMeshProUGUI>().text = foodSlider.ToString();
        //}
    }

    /// <summary>
    /// Displays requirements of the building
    /// </summary>
    public void DisplayInfo()
    {
 
        //DataID did = GameManager.instance.dataIDList.FindDataID(buildingName.ToLower());
        //foreach (CostProgression costProgression in GameManager.instance.costProg)
        //{
        //    if (costProgression.actualDID == did )
        //    {
        //        Dictionary<int, int> foodLevelProg = new Dictionary<int, int>();
        //        Dictionary<int, int> woodLevelProg = new Dictionary<int, int>();
        //        Dictionary<int, int> stoneLevelProg = new Dictionary<int, int>();
        //        Dictionary<int, int> metalLevelProg = new Dictionary<int, int>();
        //        Dictionary<int, int> goldLevelProg = new Dictionary<int, int>();

        //        foreach (KeyValuePair<DataID, Dictionary<int, int>> something in costProgression.progression[did])
        //        {
        //            if (something.Key.name.Equals("food"))
        //            {
        //                foodLevelProg = costProgression.progression[did][something.Key];
        //            }
        //            else if (something.Key.name.Equals("wood"))
        //            {
        //                woodLevelProg = costProgression.progression[did][something.Key];
        //            }
        //            else if (something.Key.name.Equals("stone"))
        //            {
        //                stoneLevelProg = costProgression.progression[did][something.Key];
        //            }
        //            else if (something.Key.name.Equals("metal"))
        //            {
        //                metalLevelProg = costProgression.progression[did][something.Key];
        //            }
        //            else if(something.Key.name.Equals("gold"))
        //            {
        //                goldLevelProg = costProgression.progression[did][something.Key];
        //            }
        //        }


        //        //infoPanelFood.GetComponent<TextMeshProUGUI>().text = foodLevelProg[levelToBe].ToString();
        //        //infoPanelGold.GetComponent<TextMeshProUGUI>().text = goldLevelProg[levelToBe].ToString();
        //        //infoPanelWood.GetComponent<TextMeshProUGUI>().text = woodLevelProg[levelToBe].ToString();
        //        //infoPanelMetal.GetComponent<TextMeshProUGUI>().text = metalLevelProg[levelToBe].ToString();
        //        //infoPanelStone.GetComponent<TextMeshProUGUI>().text = stoneLevelProg[levelToBe].ToString();
        //    }

        //}

     

    }

}
