using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farm : Building
{
    /// <summary>
    /// Produces food, also in-game Currency
    /// </summary>
    public Farm()
    {
        DID = GameManager.instance.dataIDList.FindDataID("farm");
        this.pollutionOutput = -20;
    }
}
