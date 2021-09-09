using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : Building
{
    // Start is called before the first frame update

    public int population;

    private Dictionary<int, int> levelToPopulation = new Dictionary<int, int>();
    public House()
    {
        this.population = 20;
        this.dataID = new DataID(4, "House");
        this.pollutionOutput = 20;
    }
}
