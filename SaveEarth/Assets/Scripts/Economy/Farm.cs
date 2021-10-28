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
        DID = GameManager.instance.dataIDList.FindDataID("farm");
        polProg = GameManager.instance.polProg[1].progression[DID];
        pollutionOutput = polProg[1];
        foodOutput = 25;
        ResourceManager.instance.foodOutput += foodOutput;
        UpdatePollution();
    }

}
