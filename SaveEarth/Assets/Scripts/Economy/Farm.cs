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
        //this.DID = new DataID(1);
        this.pollutionOutput = -20;
    }
}
