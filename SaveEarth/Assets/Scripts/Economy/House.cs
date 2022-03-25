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

    private void Start()
    {
        this.population = 20;
        buildingData = GameManager.instance.buildingSOs[4];
        level = 1;
        DID = buildingData.dataId;
        pollutionOutput = buildingData.pollutionProg.levelProg[1];
        GameManager.instance.pollutionValue += pollutionOutput;
    }

    public void IncreasePopulations()
    {
        float decayConstant = 1 / 0.85f;
        population = (int)(population * decayConstant);
    }

    public override void LevelUp()
    {
        base.LevelUp();
        IncreasePopulations();
    }
}
