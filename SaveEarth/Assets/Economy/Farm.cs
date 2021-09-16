using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farm : Building
{
    public Farm()
    {
        this.DID = new DataID(1, "Farm");
        this.pollutionOutput = -20;
    }
}
