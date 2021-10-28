using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CostProgression
{
    public int id;
    public string DID;
    public int food_1;
    public int wood_1;
    public int stone_1;
    public int metal_1;
    public int food_2;
    public int wood_2;
    public int stone_2;
    public int metal_2;
    public int food_3;
    public int wood_3;
    public int stone_3;
    public int metal_3;

    public DataID actualDID = new DataID(); 
    private int maxLevel = 3;

    Dictionary<DataID, Dictionary<int, int>> dataIDToLevelProg = new Dictionary<DataID, Dictionary<int, int>>();
    // buildings dataID mapped to dictionary of Resource dataID mapped to level and value
    public Dictionary<DataID, Dictionary<DataID, Dictionary<int, int>>> progression = new Dictionary<DataID, Dictionary<DataID, Dictionary<int, int>>>();

    public void HandleProgression()
    {
        actualDID = CSVImportTool.dataIDs.FindDataID(DID.Split('_')[1]);
        List<int> foodValues = new List<int>();
        foodValues.Add(food_1);
        foodValues.Add(food_2);
        foodValues.Add(food_3);

        List<int> woodValues = new List<int>();
        woodValues.Add(wood_1);
        woodValues.Add(wood_2);
        woodValues.Add(wood_3);

        List<int> stoneValues = new List<int>();
        stoneValues.Add(stone_1);
        stoneValues.Add(stone_2);
        stoneValues.Add(stone_3);


        List<int> metalValues = new List<int>();
        metalValues.Add(metal_1);
        metalValues.Add(metal_2);
        metalValues.Add(metal_3);


        FillLevelProg(CSVImportTool.dataIDs.FindDataID("food"),foodValues);
        FillLevelProg(CSVImportTool.dataIDs.FindDataID("wood"), woodValues);
        FillLevelProg(CSVImportTool.dataIDs.FindDataID("stone"), stoneValues);
        FillLevelProg(CSVImportTool.dataIDs.FindDataID("metal"), metalValues);
        FillFullProgression();
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

    public void HandleProgression()
    {
        actualDID = CSVImportTool.dataIDs.FindDataID(DID.Split('_')[1]);
        List<int> levelProg = new List<int>();
        // adding 0 to get 0 index as 0 for convenience purposes
        levelProg.Add(0);
        levelProg.Add(PO_1);
        levelProg.Add(PO_2);
        levelProg.Add(PO_3);
        levelProg.Add(PO_4);
        progression.Add(actualDID, levelProg);
    }

}

[System.Serializable]
public class ProgressionsList
{
    public List<CostProgression> costProgs;
    public List<PollutionProgression> polProgs;
}
