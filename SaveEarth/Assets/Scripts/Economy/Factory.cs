using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Produces waste but increases City's value
/// </summary>
public class Factory : Building
{
    public Factory()
    {
        DID = GameManager.instance.dataIDList.FindDataID("factory");
        polProg = GameManager.instance.polProg[2].progression[DID];
        pollutionOutput = polProg[1];
        UpdatePollution();
    }
}
