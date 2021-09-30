using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Dictionary<DataID, Dictionary<int,int>> dataIDToPollution = new Dictionary<DataID, Dictionary<int, int>>();
    public float time = 0;
    private Dictionary<int, int> levelToPolutionOutput;

    public GameManager()
    {
        PopulatePollutionEconomy();       
    }
    
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

    void PopulatePollutionEconomy()
    {
        // Json file will be inputed

    }

    DataID GetDID(GameObject gameObject)
    {
        return gameObject.GetComponent<DataID>();
    }

}
