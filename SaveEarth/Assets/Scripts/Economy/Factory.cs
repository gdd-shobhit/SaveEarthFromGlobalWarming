using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Produces waste but increases City's value
/// </summary>
public class Factory : Building
{
    public int goldCoinsOutput=0;
    public Factory()
    {
        buildingData = GameManager.instance.buildingSOs[1];
        level = 1;
        DID = buildingData.dataId;
        pollutionOutput = buildingData.pollutionProg.levelProg[1];
        goldCoinsOutput = 20;
        
        //ResourceManager.instance.goldOutput = goldCoinsOutput;
        UpdatePollution();
    }
}
