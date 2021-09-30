using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.IO;

public class DataGenerationUtils : MonoBehaviour
{
    internal static void Generate(DataIDList sample,bool costBool, bool pollutionBool, bool buildingBool)
    {
        string path = Directory.GetCurrentDirectory() + "\\Assets\\StaticData\\JSON\\DataIDs.json";

        // create and open if doesn't exist
        FileStream fs = new FileStream(path, FileMode.OpenOrCreate);

        fs.SetLength(0);
        StreamWriter writer = new StreamWriter(fs);
        writer.WriteLine(JsonUtility.ToJson(sample).ToString());
        // get the string using JsonUtility and write it to the file
        writer.Close();
        // close the file after writing
    }
}
