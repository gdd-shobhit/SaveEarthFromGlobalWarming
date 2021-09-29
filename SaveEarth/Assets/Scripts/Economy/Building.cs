using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public int level;
    public int pollutionOutput;
    public DataID DID;
    public Dictionary<int, int> prog; 
   
    public Building()
    {
        level = 1;
    }

    /// <summary>
    /// Level Up the Building if the required resources are met
    /// </summary>
    public virtual void LevelUp()
    {
        // required resources
        // 1. Stone 2. Wood 3. Metal 4. Currency(Food)
        // check if resources are enough to level up
        if (ResourceManager.CheckRequirements(DID, level+1))
        {
            level++;
        }
    }
}
