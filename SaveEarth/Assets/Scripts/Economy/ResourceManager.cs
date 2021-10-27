using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor.UI;
using TMPro;

/// <summary>
/// Will keep track of resources
/// </summary>
public class ResourceManager : MonoBehaviour
{
    public GameObject resourcesReqPanel;
    [SerializeField] private int amountOfClicks = 0;
    [SerializeField] private int currentFood = 1000;
    [SerializeField] private int currentWood = 1000;
    [SerializeField] private int currentStone = 1000;
    [SerializeField] private int currentMetal = 1000;
    [SerializeField] private int currentGold = 1000;
    
    

    public GameObject foodTM;
    public GameObject woodTM;
    public GameObject stoneTM;
    public GameObject metalTM;
    public GameObject goldTM;


    // production per min
    public int baseProductionRate = 100;

    public static ResourceManager instance;

    void Start()
    {
    
        if (instance == null)
        {
            instance = this;
            foodTM.GetComponent<TextMeshProUGUI>().text = currentFood.ToString();
            woodTM.GetComponent<TextMeshProUGUI>().text = currentWood.ToString();
            stoneTM.GetComponent<TextMeshProUGUI>().text = currentStone.ToString();
            metalTM.GetComponent<TextMeshProUGUI>().text = currentMetal.ToString();
            goldTM.GetComponent<TextMeshProUGUI>().text = currentGold.ToString();
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
                }         

                if(instance.currentFood >= foodLevelProg[levelToBe] && instance.currentWood >= woodLevelProg[levelToBe]
                    && instance.currentStone >= stoneLevelProg[levelToBe] && instance.currentMetal >= metalLevelProg[levelToBe])
                {
                    
                    instance.currentFood -= foodLevelProg[levelToBe];
                    Debug.Log("" + instance.currentFood + " food level prog: " + foodLevelProg[levelToBe]);
                    instance.currentWood -= woodLevelProg[levelToBe];
                    instance.currentStone -= stoneLevelProg[levelToBe];
                    instance.currentMetal -= metalLevelProg[levelToBe];
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

    private void UpdateResources()
    {
        //foodTM.GetComponent<TextMeshProUGUI>().text = "";
        //woodTM.GetComponent<TextMeshProUGUI>().text = "";
        //stoneTM.GetComponent<TextMeshProUGUI>().text = "";
        //metalTM.GetComponent<TextMeshProUGUI>().text = "";
        //goldTM.GetComponent<TextMeshProUGUI>().text = "";

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

}
