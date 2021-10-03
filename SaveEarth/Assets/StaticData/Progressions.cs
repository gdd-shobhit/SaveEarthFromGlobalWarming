using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Progressions
{
    public Dictionary<int, int> progression;
}

[System.Serializable]
public class ProgressionsList
{
    public Dictionary<DataID, Dictionary<int,int>> progs;
}
