using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownCenter : Building
{
    public TownCenter()
    {
        this.DID = new DataID(0, "Town Center");
        this.pollutionOutput = -20;
    }

    /// <summary>
    /// Level Ups the the town center by 1
    /// </summary>
    public override void LevelUp()
    {
        // if requirements are met
        // Requirements - Food, Wood, Metal, Stone, Population, Factories number,
        // Farm Number, Filteration Plant Numbers and their levels
        base.LevelUp();

    }
}
