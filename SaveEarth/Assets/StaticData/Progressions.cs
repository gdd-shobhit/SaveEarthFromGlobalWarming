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

    private int maxLevel = 3;

    Dictionary<DataID, Dictionary<int, int>> dataIDToLevelProg;
    // buildings dataID mapped to dictionary of Resource dataID mapped to level and value
    public Dictionary<DataID, Dictionary<DataID, Dictionary<int, int>>> progression;

    public CostProgression()
    {
        progression = new Dictionary<DataID, Dictionary<DataID, Dictionary<int, int>>>();
    
        dataIDToLevelProg = new Dictionary<DataID, Dictionary<int, int>>();
        
        HandleProgression();       
    }
    void HandleProgression()
    {
        Debug.Log(food_1);
        List<int> foodValues = new List<int>();
        int[] foodArray = { food_1, food_2, food_3 };
        foodValues.AddRange(foodArray);

        List<int> woodValues = new List<int>();
        int[] woodArray = { wood_1, wood_2, wood_3 };
        woodValues.AddRange(woodArray);

        List<int> stoneValues = new List<int>();
        int[] stoneArray = { stone_1, stone_2, stone_3 };
        stoneValues.AddRange(stoneArray);

        List<int> metalValues = new List<int>();
        int[] metalArray = { metal_1, metal_2, metal_3 };
        metalValues.AddRange(metalArray);

        FillLevelProg(CSVImportTool.dataIDs.FindDataID("resource_food"),foodValues);
        FillLevelProg(CSVImportTool.dataIDs.FindDataID("resource_wood"), foodValues);
        FillLevelProg(CSVImportTool.dataIDs.FindDataID("resource_stone"), foodValues);
        FillLevelProg(CSVImportTool.dataIDs.FindDataID("resource_metal"), foodValues);
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
        levelProg.Clear();
    }

    void FillFullProgression()
    {
        Debug.Log(dataIDToLevelProg);
        progression.Add(CSVImportTool.dataIDs.FindDataID(DID), dataIDToLevelProg);
    }
}

[System.Serializable]
public class PollutionProgression
{

}

[System.Serializable]
public class ProgressionsList
{
    public List<CostProgression> costProgs;
    public List<PollutionProgression> polProgs;
}
