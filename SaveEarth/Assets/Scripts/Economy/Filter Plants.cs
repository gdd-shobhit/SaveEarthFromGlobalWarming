using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FilterPlants : Building
{

    /// <summary>
    /// Filteration Plants gives negative PO(Pollution Output)
    /// Can only be built 
    /// </summary>
   public FilterPlants()
    {
        //this.DID = new DataID(3);
        this.pollutionOutput = -20;
    }
}
