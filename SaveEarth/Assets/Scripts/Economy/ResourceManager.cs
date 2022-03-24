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
    public Slider crystalSlider;
    public Slider metalSlider;
    public Slider goldSlider;

    public TextMeshProUGUI foodUI;
    public TextMeshProUGUI woodUI;
    public TextMeshProUGUI metalUI;
    public TextMeshProUGUI goldUI;
    public TextMeshProUGUI crystalUI;

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
            crystalSlider.maxValue = resourceStorageSO.stoneStorage;
            metalSlider.maxValue = resourceStorageSO.metalStorage;
            goldSlider.maxValue = resourceStorageSO.goldStorage;
            UpdateResources(resourceStorageSO.food, resourceStorageSO.wood, resourceStorageSO.metal, resourceStorageSO.gold, resourceStorageSO.stone,true);
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
    public bool CheckRequirements(BuildingSO BSO, int levelToBe)
    {
        if (foodSlider.value >= BSO.costProg.foodValues[levelToBe - 1] && woodSlider.value >= BSO.costProg.woodValues[levelToBe - 1]
            && metalSlider.value >= BSO.costProg.metalValues[levelToBe - 1] && goldSlider.value >= BSO.costProg.goldValues[levelToBe - 1] &&
            crystalSlider.value >= BSO.costProg.stoneValues[levelToBe - 1])
        {
            return true;
        }

        return false;

    }
    /// <summary>
    /// Takes in Food, Wood, Metal, Gold and Crystal
    /// </summary>
    public void UpdateResources(int food, int wood, int metal, int gold, int crystal,bool toAdd)
    {
        if(toAdd)
        {
            foodSlider.value += food;
            woodSlider.value += wood;
            crystalSlider.value += crystal;
            metalSlider.value += metal;
            goldSlider.value += gold;
        }
        else
        {
            foodSlider.value -= food;
            woodSlider.value -= wood;
            crystalSlider.value -= crystal;
            metalSlider.value -= metal;
            goldSlider.value -= gold;
        }
        
    }

    /// <summary>
    /// Handles the changes in the resources when days are passed
    /// </summary>
    public void HandleResourcesOutput()
    {
        resourceStorageSO.food += foodOutput + baseProductionRate;
        resourceStorageSO.gold += goldOutput;
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
