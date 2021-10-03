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
        //this.DID = new DataID(4);
        this.pollutionOutput = 20;
    }

    public void IncreasePopulations()
    {
        float decayConstant = 1 / 0.85f;
        population = (int)(population * decayConstant);
    }
}
