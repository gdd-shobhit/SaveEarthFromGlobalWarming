using UnityEngine;

[CreateAssetMenu(fileName = "DataID", menuName = "ScriptableObjects/BuildingObject", order = 1)]
public class BuildingSO : ScriptableObject
{
    public DataID dataId;
    public int size;
    public CostProgression costProg;
    public PollutionProgression pollutionProg;

    //public ScriptableObject Requirements;

    // in seconds
    public int timeToBuild = 30;

    public int maxLevel = 10;
}