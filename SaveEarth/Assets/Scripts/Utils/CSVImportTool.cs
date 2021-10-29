using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class CSVImportTool : MonoBehaviour
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

    //[MenuItem("Tools/CSV Importer")]
    //static void Init()
    //{
    //     get window if it exists or make a new one
    //    CSVImportTool window = (CSVImportTool)GetWindow(typeof(CSVImportTool));
    //    window.Show();
    //}

    //private void OnGUI()
    //{
    //    GUILayout.Label("Base Settings", EditorStyles.boldLabel);
    //    dataIdData = EditorGUILayout.Toggle("DataIDs", dataIdData);
    //    buildingData = EditorGUILayout.Toggle("Building Data", buildingData);
    //    costData = EditorGUILayout.Toggle("Cost Data", costData);
    //    pollutionData = EditorGUILayout.Toggle("Pollution Data", pollutionData);

    //    // DataGeneration JSON
    //    groupEnabled = EditorGUILayout.BeginToggleGroup("Generate JSON", groupEnabled);
    //    costBool = EditorGUILayout.Toggle("Cost", costBool);
    //    pollutionBool = EditorGUILayout.Toggle("Pollution", pollutionBool);
    //    BuildingBool = EditorGUILayout.Toggle("Building", BuildingBool);
    //    EditorGUILayout.EndToggleGroup();

    //    if (GUILayout.Button("Import"))
    //    {
    //        List<string> dataPaths = new List<string>();
    //        string textData;
    //        if (dataIdData)
    //        {
    //            textData = File.ReadAllText(Directory.GetCurrentDirectory() + "\\Assets\\StaticData\\CSV\\DataId.csv");
    //            dataIDs.dataList = CSVParser.Deserialize<DataID>(textData).ToList();
    //            foreach (DataID did in dataIDs.dataList)
    //            {
    //                did.SetName();
    //            }
    //        }

    //        if (buildingData)
    //        {
    //            textData = File.ReadAllText(Directory.GetCurrentDirectory() + "\\Assets\\StaticData\\CSV\\BuildingEconomy.csv");
    //            buildingList.buildingList = CSVParser.Deserialize<Building>(textData).ToList();
    //        }

    //        if (costData)
    //        {
    //            textData = File.ReadAllText(Directory.GetCurrentDirectory() + "\\Assets\\StaticData\\CSV\\CostProgression.csv");
    //            progressionList.costProgs = CSVParser.Deserialize<CostProgression>(textData).ToList();
    //            foreach (CostProgression cprog in progressionList.costProgs)
    //            {
    //                cprog.HandleProgression();

    //            }
    //        }

    //        if (pollutionData)
    //        {
    //            textData = File.ReadAllText(Directory.GetCurrentDirectory() + "\\Assets\\StaticData\\CSV\\PollutionProgression.csv");
    //            progressionList.polProgs = CSVParser.Deserialize<PollutionProgression>(textData).ToList();
    //        }


    //        if (groupEnabled)
    //        {
    //            DataGenerationUtils.Generate(dataIDs, costBool, pollutionBool, BuildingBool);
    //        }
    //    }
    //}

    private string dataID_path = "";
    private void Start()
    {
       
    }


    static public void TestMethod()
    {
        List<string> dataPaths = new List<string>();
        string textData;

        //textData = File.ReadAllText(Directory.GetCurrentDirectory() + "\\Assets\\Resources\\StaticData\\CSV\\DataId.csv");
        textData = "ID,DID\n0,building_towncenter\n1,building_farm\n2,building_factory\n3,building_filterationplant\n4,building_house\n5,TBD\n6,TBD\n7,TBD\n8,TBD\n9,TBD\n10,currency_gold\n11,resource_food\n12,resource_wood\n13,resource_stone\n14,resource_metal";
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

        //textData = File.ReadAllText(Directory.GetCurrentDirectory() + "\\Assets\\Resources\\StaticData\\CSV\\CostProgression.csv");
        textData = "ID,DID,food_1,wood_1,stone_1,metal_1,food_2,wood_2,stone_2,metal_2,food_3,wood_3,stone_3,metal_3\n0,building_towncenter,500,200,200,300,1500,1800,800,700,3500,3800,2800,1800\n1,building_farm,200,100,50,20,500,250,100,75,1000,500,200,100\n2,building_factory,300,200,100,150,600,700,500,400,1500,1200,1000,800\n3,building_filterationplant,400,300,200,100,1000,800,700,350,2500,1600,1200,750\n4,building_house,150,100,50,50,250,250,100,100,400,400,300,250";
        progressionList.costProgs = CSVParser.Deserialize<CostProgression>(textData).ToList();

        foreach (CostProgression cprog in progressionList.costProgs)
        {
            cprog.HandleProgression();
        }

        //textData = File.ReadAllText(Directory.GetCurrentDirectory() + "\\Assets\\Resources\\StaticData\\CSV\\PollutionProgression.csv");
        textData = "ID,DID,PO_1,PO_2,PO_3,PO_4\n0,building_towncenter,0,0,20,40\n1,building_farm,5,10,20,40\n2,building_factory,60,140,300,400\n3,building_filterationplant,-20,-50,-150,-300\n4,building_house,20,40,60,100";
        progressionList.polProgs = CSVParser.Deserialize<PollutionProgression>(textData).ToList();

        foreach (PollutionProgression pprog in progressionList.polProgs)
        {
            pprog.HandleProgression();
        }

        //if (groupEnabled)
        //{
        //    DataGenerationUtils.Generate(dataIDs, costBool, pollutionBool, BuildingBool);
        //}
    }
}

