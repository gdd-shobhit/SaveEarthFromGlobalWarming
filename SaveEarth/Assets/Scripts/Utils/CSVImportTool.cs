using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using ExcelToCsv;

public class CSVImportTool : ToolEditor
{
    string myString = "Hello World";
    bool groupEnabled;
    bool costBool = true;
    bool pollutionBool = true;
    bool BuildingBool = true;

    

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
        myString = EditorGUILayout.TextField("Path(after Assets)", myString);

        groupEnabled = EditorGUILayout.BeginToggleGroup("Generate JSON", groupEnabled);
        costBool = EditorGUILayout.Toggle("Cost", costBool);
        pollutionBool = EditorGUILayout.Toggle("Pollution", pollutionBool);
        BuildingBool = EditorGUILayout.Toggle("Building", BuildingBool);
        EditorGUILayout.EndToggleGroup();

        if(GUILayout.Button("Import"))
        {
            ExcelToCsv.CSVParser.ConvertExcelToCSV();
            if(groupEnabled)
            {
                DataGenerationUtils.Generate(costBool, pollutionBool, BuildingBool);
            }
        }
    }
}
