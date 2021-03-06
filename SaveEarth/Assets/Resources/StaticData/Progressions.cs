using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CostProgression
{
    public int id;
    public string DID;
    int food_1;
    int wood_1;
    int stone_1;
    int metal_1;
    int gold_1;
    int food_2;
    int wood_2;
    int stone_2;
    int metal_2;
    int gold_2;
    int food_3;
    int wood_3;
    int stone_3;
    int metal_3;
    int gold_3;

    public DataID actualDID = new DataID(); 
    private int maxLevel = 3;

    Dictionary<DataID, Dictionary<int, int>> dataIDToLevelProg = new Dictionary<DataID, Dictionary<int, int>>();
    // buildings dataID mapped to dictionary of Resource dataID mapped to level and value
    public Dictionary<DataID, Dictionary<DataID, Dictionary<int, int>>> progression = new Dictionary<DataID, Dictionary<DataID, Dictionary<int, int>>>();
    public List<int> foodValues = new List<int>();
    public List<int> woodValues = new List<int>();
    public List<int> stoneValues = new List<int>();
    public List<int> metalValues = new List<int>();
    public List<int> goldValues = new List<int>();
    public void HandleProgression()
    {
        actualDID = CSVImportTool.dataIDs.FindDataID(DID.Split('_')[1]);
        
        foodValues.Add(food_1);
        foodValues.Add(food_2);
        foodValues.Add(food_3);
        
        woodValues.Add(wood_1);
        woodValues.Add(wood_2);
        woodValues.Add(wood_3);

        stoneValues.Add(stone_1);
        stoneValues.Add(stone_2);
        stoneValues.Add(stone_3);

        metalValues.Add(metal_1);
        metalValues.Add(metal_2);
        metalValues.Add(metal_3);

        goldValues.Add(gold_1);
        goldValues.Add(gold_2);
        goldValues.Add(gold_3); 
    }

    void FillLevelProg(DataID did, List<int> levelToValue)
    {
        Dictionary<int,int> levelProg = new Dictionary<int, int>();
        for(int i = 1; i <= maxLevel; i++)
        {
            levelProg.Add(i, levelToValue[i-1]);
        }

        dataIDToLevelProg.Add(did, levelProg);
    }

    void FillFullProgression()
    {
        progression.Add(CSVImportTool.dataIDs.FindDataID(DID.Split('_')[1]), dataIDToLevelProg);
    }
}

[System.Serializable]
public class PollutionProgression
{
    public int id;
    public string DID;
    public int PO_1;
    public int PO_2;
    public int PO_3;
    public int PO_4;

    public DataID actualDID = new DataID();
    public Dictionary<DataID, List<int>> progression = new Dictionary<DataID, List<int>>();
    public Dictionary<int,int> progressionByLevel = new Dictionary<int,int>();
    public List<int> levelProg = new List<int>();
    public void HandleProgression()
    {
        actualDID = CSVImportTool.dataIDs.FindDataID(DID.Split('_')[1]);
        
        // adding 0 to get 0 index as 0 for convenience purposes
        levelProg.Add(0);
        levelProg.Add(PO_1);
        levelProg.Add(PO_2);
        levelProg.Add(PO_3);
        levelProg.Add(PO_4);

        //progressionByLevel.Add(0, 0);
        //progressionByLevel.Add(1, PO_1);
        //progressionByLevel.Add(2, PO_2);
        //progressionByLevel.Add(3, PO_3);
        //progressionByLevel.Add(4, PO_4);

        //Debug.Log(progressionByLevel[1]);
        //progression.Add(actualDID, levelProg);
    }

}

[System.Serializable]
public class ProgressionsList
{
    public List<CostProgression> costProgs;
    public List<PollutionProgression> polProgs;
}
