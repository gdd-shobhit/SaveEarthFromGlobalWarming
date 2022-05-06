using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Produces waste but increases City's value
/// </summary>
public class Factory : Building
{
    public int goldCoinsOutput=0;

    private void Start()
    {
        buildingData = GameManager.instance.buildingSOs[1];
        level = 1;
        DID = buildingData.dataId;
        GameManager.instance.pollutionValue += buildingData.pollutionProg.levelProg[1];
        goldCoinsOutput = 4;

        //ResourceManager.instance.goldOutput = goldCoinsOutput;
        StartCoroutine(GoldUpdate());
    }

    IEnumerator GoldUpdate()
    {
        while(goldCoinsOutput > 0)
        {
            yield return new WaitForSeconds(5);
            ResourceManager.instance.goldSlider.value += goldCoinsOutput;
            if (goldCoinsOutput <= 0)
                break;
        }

        yield return null;
    }
}
