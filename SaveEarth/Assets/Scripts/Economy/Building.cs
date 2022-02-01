using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base class for a structure/Building
/// </summary>
public class Building : MonoBehaviour
{
    public int id;
    public string didName;
    public string buildingName;
    public string pollution;
    public string cost;

    public ScriptableObject buildingData;

    public int level;
    public int pollutionOutput;
    public Dictionary<DataID, Dictionary<int, int>> costProg;
    public List<int> polProg = new List<int>();
    public DataID DID = new DataID();

    public Building()
    {
        level = 1;    
    }

    /// <summary>
    /// Whenever building is initiated, update the pollution
    /// </summary>
    public void UpdatePollution()
    {
        GameManager.instance.pollutionValue += pollutionOutput;
    }

    /// <summary>
    /// Level Up the Building if the required resources are met
    /// </summary>
    public virtual void LevelUp()
    {
        // required resources
        // 1. Stone 2. Wood 3. Metal 4. Currency(Food)
        // check if resources are enough to level up
        if (ResourceManager.instance.CheckRequirements(DID,level+1) && level < 4)
        {
            level++;
            pollutionOutput = polProg[level];
            UpdatePollution();
        }
    }

    /// <summary>
    /// Calculates the building's cost and pollution progs
    /// </summary>
    public virtual void CalculateProg()
    {
        
    }
}

[System.Serializable]
public class BuildingList
{
    public List<Building> buildingList;
}
