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
    public int currentFood = 500;
    public int currentWood = 200;
    public int currentStone = 150;
    public int currentMetal = 100;
    public int currentGold = 0;
    

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

    private void FixedUpdate()
    {
       
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
                foreach (KeyValuePair<DataID,Dictionary<int,int>> something in costProgression.progression[did])
                {                
                   
                    if(something.Key == GameManager.instance.dataIDList.FindDataID("food"))
                    {
                        foodLevelProg = costProgression.progression[did][something.Key];
                    }                   
                }         

                //foodLevelProg = costProgression.progression[did][GameManager.instance.dataIDList.FindDataID("food")];
                if(instance.currentFood >= foodLevelProg[levelToBe])
                {
                    instance.currentFood -= foodLevelProg[levelToBe];
                    UpdateResources();
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        return false;
    }

    private void UpdateResources()
    {
        foodTM.GetComponent<TextMeshProUGUI>().text = "";
        foodTM.GetComponent<TextMeshProUGUI>().text = instance.currentFood.ToString();
        woodTM.GetComponent<TextMeshProUGUI>().text = instance.currentWood.ToString();
        stoneTM.GetComponent<TextMeshProUGUI>().text = instance.currentStone.ToString();
        metalTM.GetComponent<TextMeshProUGUI>().text = instance.currentMetal.ToString();
        goldTM.GetComponent<TextMeshProUGUI>().text = instance.currentGold.ToString();
    }
}
