using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farm : Building
{
    public int foodOutput;
    /// <summary>
    /// Produces food, also in-game Currency
    /// </summary>
    private void Start()
    {

        buildingData = GameManager.instance.buildingSOs[3];
        level = 1;
        DID = buildingData.dataId;
        GameManager.instance.pollutionValue += buildingData.pollutionProg.levelProg[1];
        foodOutput = 5;
        StartCoroutine(IncreaseFood());
    }


    IEnumerator IncreaseFood()
    {
        while(foodOutput > 0)
        {
            yield return new WaitForSeconds(4);
            ResourceManager.instance.foodSlider.value += foodOutput;
            if (foodOutput <= 0)
                break;
        }
        

    }

}
