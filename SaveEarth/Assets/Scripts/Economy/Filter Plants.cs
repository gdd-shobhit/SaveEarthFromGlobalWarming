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
        buildingData = GameManager.instance.buildingSOs[2];
        level = 1;
        DID = buildingData.dataId;
        pollutionOutput = buildingData.pollutionProg.levelProg[1];
        UpdatePollution();
    }
}
