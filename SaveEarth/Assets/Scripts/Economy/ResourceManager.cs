using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    int food = 400;
    int wood = 200;
    int stone = 150;
    int metal = 100;

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
