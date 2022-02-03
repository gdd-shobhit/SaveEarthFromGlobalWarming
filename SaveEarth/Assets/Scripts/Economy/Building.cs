using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base class for a structure/Building
/// </summary>
public class Building : MonoBehaviour
{

    // needs to be deprecated - It will only hold its SO in ht end
    public BuildingSO buildingData;

    public int level;
    public int pollutionOutput;
    public DataID DID = new DataID();

    public Building()
    {
        level = 1;
        pollutionOutput = 0;
    }

    /// <summary>
    /// Whenever building is initiated, update the pollution
    /// </summary>
    public void UpdatePollution()
    {
        GameManager.instance.pollutionValue = pollutionOutput;
    }

    /// <summary>
    /// Level Up the Building if the required resources are met
    /// </summary>
    public virtual void LevelUp()
    {
        level++;
        pollutionOutput = buildingData.pollutionProg.levelProg[level];
        UpdatePollution();
        // required resources
        // 1. Stone 2. Wood 3. Metal 4. Currency(Food)
        // check if resources are enough to level up
        //if (ResourceManager.instance.CheckRequirements(DID,level+1) && level < 4)
        //{
        //    level++;
        //    pollutionOutput = buildingData.pollutionProg.progressionByLevel[level];
        //    UpdatePollution();
        //}
    }

 

   
}