using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Town Center for the city - Singular and already build in to start off the game.
/// </summary>
public class TownCenter : Building
{
    public TownCenter()
    {
        // sets the DID when its instantiated - Shobhit
        buildingData = GameManager.instance.buildingSOs[0];
        level = 1;
        DID = buildingData.dataId;
        pollutionOutput = buildingData.pollutionProg.levelProg[1];
        UpdatePollution();

    }

    /// <summary>
    /// Level Ups the the town center by 1
    /// </summary>
    public override void LevelUp()
    {
        // if requirements are met
        // Requirements - Food, Wood, Metal, Stone, Population, Factories number,
        // Farm Number, Filteration Plant Numbers and their levels
        ResourceManager.instance.baseProductionRate += 10;
        
        base.LevelUp();
    }


}
