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
        levelToPolutionOutput = new Dictionary<int, int>();
        levelToPolutionOutput.Add(1, 0);
        levelToPolutionOutput.Add(2, 0);
        levelToPolutionOutput.Add(3, 20);
        levelToPolutionOutput.Add(4, 20);

        dataIDToPollution.Add(new DataID(0, "Town Center"), levelToPolutionOutput);

        levelToPolutionOutput.Clear();
        levelToPolutionOutput.Add(1, -20);
        levelToPolutionOutput.Add(2, -25);
        levelToPolutionOutput.Add(3, -35);
        levelToPolutionOutput.Add(4, -50);

        dataIDToPollution.Add(new DataID(1, "Farm"), levelToPolutionOutput);

        levelToPolutionOutput.Clear();
        levelToPolutionOutput.Add(1, 60);
        levelToPolutionOutput.Add(2, 140);
        levelToPolutionOutput.Add(3, 300);
        levelToPolutionOutput.Add(4, 400);

        dataIDToPollution.Add(new DataID(2, "Factory"), levelToPolutionOutput);

        levelToPolutionOutput.Clear();
        levelToPolutionOutput.Add(1, -20);
        levelToPolutionOutput.Add(2, -50);
        levelToPolutionOutput.Add(3, -150);
        levelToPolutionOutput.Add(4, -300);

        dataIDToPollution.Add(new DataID(3, "Filteration Plant"), levelToPolutionOutput);

        levelToPolutionOutput.Clear();
        levelToPolutionOutput.Add(1, 20);
        levelToPolutionOutput.Add(2, 40);
        levelToPolutionOutput.Add(3, 60);
        levelToPolutionOutput.Add(4, 100);

        dataIDToPollution.Add(new DataID(4, "House"), levelToPolutionOutput);

    }

    DataID GetDID(string name)
    {

        return null;
    }

}
