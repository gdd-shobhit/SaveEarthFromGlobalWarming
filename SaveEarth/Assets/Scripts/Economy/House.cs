using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Increases population, increases city value in general
/// </summary>
public class House : Building
{
    // Start is called before the first frame update

    public int population;

    //private Dictionary<int, int> levelToPopulation = new Dictionary<int, int>();
    public House()
    {
        this.population = 20;
        DID = GameManager.instance.dataIDList.FindDataID("house");
        polProg = GameManager.instance.polProg[4].progression[DID];
        pollutionOutput = polProg[1];
        UpdatePollution();
    }

    public void IncreasePopulations()
    {
        float decayConstant = 1 / 0.85f;
        population = (int)(population * decayConstant);
    }

    public override void LevelUp(int incomingLevel)
    {
        base.LevelUp(incomingLevel);
        IncreasePopulations();
    }
}
