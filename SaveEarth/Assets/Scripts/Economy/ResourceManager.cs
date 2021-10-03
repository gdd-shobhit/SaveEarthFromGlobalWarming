using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Will keep track of resources
/// </summary>
public class ResourceManager : MonoBehaviour
{
    int currentFood = 400;
    int currentWood = 200;
    int currentStone = 150;
    int currentMetal = 100;

    // production per min
    public int baseProductionRate = 100;

    public static ResourceManager instance;

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
