using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownCenter : Building
{
    
    
    public TownCenter()
    {
        // sets the DID when its instantiated - Shobhit
        DID = GameManager.instance.dataIDList.FindDataID("towncenter");
        polProg = GameManager.instance.polProg[0].progression[DID];
        pollutionOutput = polProg[1];
        UpdatePollution();

    }

    /// <summary>
    /// Level Ups the the town center by 1
    /// </summary>
    public override void LevelUp(int incomingLevel)
    {
        // if requirements are met
        // Requirements - Food, Wood, Metal, Stone, Population, Factories number,
        // Farm Number, Filteration Plant Numbers and their levels
        ResourceManager.instance.baseProductionRate += 10;
        base.LevelUp(incomingLevel);
    }


}
