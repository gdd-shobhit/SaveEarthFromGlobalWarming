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
    protected int currentFood = 400;
    protected int currentWood = 200;
    protected int currentStone = 150;
    protected int currentMetal = 100;
    protected int currentGold = 0;
    

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


    void DataIDToResources()
    {

    }

    /// <summary>
    /// Takes in DID and find its requirements and tells if it meets the levelupRequirements 
    /// </summary>
    /// <param name="did"></param>
    internal static bool CheckRequirements(DataID did,int levelReq)
    {
        return false;
    }
}
