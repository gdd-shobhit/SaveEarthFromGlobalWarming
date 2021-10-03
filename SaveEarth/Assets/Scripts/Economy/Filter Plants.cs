using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Filteration Plants gives negative PO(Pollution Output)
/// Can only be built a few times
/// </summary>
public class FilterPlants : Building
{

   
   public FilterPlants()
    {
        //this.DID = new DataID(3);
        this.pollutionOutput = -20;
    }
}
