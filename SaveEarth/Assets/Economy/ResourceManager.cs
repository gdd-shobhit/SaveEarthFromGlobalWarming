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
    

}
