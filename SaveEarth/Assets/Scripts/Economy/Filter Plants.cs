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
        DID = GameManager.instance.dataIDList.FindDataID("filterationplant");
        polProg = GameManager.instance.polProg[3].progression[DID];
        pollutionOutput = polProg[1];
        UpdatePollution();
    }
}
