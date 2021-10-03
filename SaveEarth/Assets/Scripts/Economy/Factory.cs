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
        //this.DID = new DataID(2);
        this.pollutionOutput = 60;
    }
}
