using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : Building
{
    public Factory()
    {
        this.DID = new DataID(2, "Factory");
        this.pollutionOutput = 60;
    }
}
