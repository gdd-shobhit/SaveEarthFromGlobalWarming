using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farm : Building
{
    public int foodOutput;
    /// <summary>
    /// Produces food, also in-game Currency
    /// </summary>
    public Farm()
    {
        buildingData = GameManager.instance.buildingSOs[3];
        level = 1;
        DID = buildingData.dataId;
        pollutionOutput = buildingData.pollutionProg.levelProg[1];

        //ResourceManager.instance.foodOutput += foodOutput;
        UpdatePollution();
    }

}
