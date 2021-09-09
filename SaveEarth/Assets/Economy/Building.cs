using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public int level;
    public int pollutionOutput;
    public DataID dataID;
   
    public Building()
    {
        level = 1;
    }

    public void LevelUp()
    {
        level++;
    }
}
