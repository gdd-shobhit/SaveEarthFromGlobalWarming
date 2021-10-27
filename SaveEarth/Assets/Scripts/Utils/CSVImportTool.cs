using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class CSVImportTool : ToolEditor
{
    string myString = "Hello World";
    bool groupEnabled;
    bool dataIdData = true;
    bool buildingData = true;
    bool costData = true;
    bool pollutionData = true;
    bool costBool = true;
    bool pollutionBool = true;
    bool BuildingBool = true;
    static public DataIDList dataIDs = new DataIDList();
    static public BuildingList buildingList = new BuildingList();
    static public ProgressionsList progressionList = new ProgressionsList();

    [MenuItem("Tools/CSV Importer")]
    static void Init()
    {
        // get window if it exists or make a new one
        CSVImportTool window = (CSVImportTool)GetWindow(typeof(CSVImportTool));
        window.Show();
    }

    private void OnGUI()
    {
        GUILayout.Label("Base Settings", EditorStyles.boldLabel);
        dataIdData = EditorGUILayout.Toggle("DataIDs", dataIdData);
        buildingData = EditorGUILayout.Toggle("Building Data", buildingData);
        costData = EditorGUILayout.Toggle("Cost Data", costData);
        pollutionData = EditorGUILayout.Toggle("Pollution Data", pollutionData);

        // DataGeneration JSON
        groupEnabled = EditorGUILayout.BeginToggleGroup("Generate JSON", groupEnabled);
        costBool = EditorGUILayout.Toggle("Cost", costBool);
        pollutionBool = EditorGUILayout.Toggle("Pollution", pollutionBool);
        BuildingBool = EditorGUILayout.Toggle("Building", BuildingBool);
        EditorGUILayout.EndToggleGroup();

        if (GUILayout.Button("Import"))
        {
            List<string> dataPaths = new List<string>();
            string textData;
            if (dataIdData)
            {
                textData = File.ReadAllText(Directory.GetCurrentDirectory() + "\\Assets\\StaticData\\CSV\\DataId.csv");
                dataIDs.dataList = CSVParser.Deserialize<DataID>(textData).ToList();
                foreach (DataID did in dataIDs.dataList)
                {
                    did.SetName();
                }
            }

            if (buildingData)
            {
                textData = File.ReadAllText(Directory.GetCurrentDirectory() + "\\Assets\\StaticData\\CSV\\BuildingEconomy.csv");
                buildingList.buildingList = CSVParser.Deserialize<Building>(textData).ToList();
            }

            if (costData)
            {
                textData = File.ReadAllText(Directory.GetCurrentDirectory() + "\\Assets\\StaticData\\CSV\\CostProgression.csv");
                progressionList.costProgs = CSVParser.Deserialize<CostProgression>(textData).ToList();
                foreach (CostProgression cprog in progressionList.costProgs)
                {
                    cprog.HandleProgression();
                    Debug.Log(cprog.DID);
                }
            }

            if (pollutionData)
            {
                textData = File.ReadAllText(Directory.GetCurrentDirectory() + "\\Assets\\StaticData\\CSV\\PollutionProgression.csv");
                progressionList.polProgs = CSVParser.Deserialize<PollutionProgression>(textData).ToList();
            }


            if (groupEnabled)
            {
                DataGenerationUtils.Generate(dataIDs, costBool, pollutionBool, BuildingBool);
            }
        }
    }

    static public void TestMethod()
    {
        List<string> dataPaths = new List<string>();
        string textData;

        textData = File.ReadAllText(Directory.GetCurrentDirectory() + "\\Assets\\StaticData\\CSV\\DataId.csv");
        dataIDs.dataList = CSVParser.Deserialize<DataID>(textData).ToList();
        foreach (DataID did in dataIDs.dataList)
        {
            did.SetName();
        }


        //if (buildingData)
        //{
        //    textData = File.ReadAllText(Directory.GetCurrentDirectory() + "\\Assets\\StaticData\\CSV\\BuildingEconomy.csv");
        //    buildingList.buildingList = CSVParser.Deserialize<Building>(textData).ToList();
        //}

        textData = File.ReadAllText(Directory.GetCurrentDirectory() + "\\Assets\\StaticData\\CSV\\CostProgression.csv");
        progressionList.costProgs = CSVParser.Deserialize<CostProgression>(textData).ToList();
        foreach (CostProgression cprog in progressionList.costProgs)
        {
            cprog.HandleProgression();
            Debug.Log(cprog.DID);
        }


        //if (pollutionData)
        //{
        //    textData = File.ReadAllText(Directory.GetCurrentDirectory() + "\\Assets\\StaticData\\CSV\\PollutionProgression.csv");
        //    progressionList.polProgs = CSVParser.Deserialize<PollutionProgression>(textData).ToList();
        //}


        //if (groupEnabled)
        //{
        //    DataGenerationUtils.Generate(dataIDs, costBool, pollutionBool, BuildingBool);
        //}
    }
}
