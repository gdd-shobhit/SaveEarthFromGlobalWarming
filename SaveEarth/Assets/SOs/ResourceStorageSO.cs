using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Holds the data for resources for player
/// </summary>
[CreateAssetMenu(fileName = "ResourceStorage", menuName = "ScriptableObjects/ResourceStorage", order = 1)]
public class ResourceStorageSO : ScriptableObject
{
    // will be converted to
    // public Dictionary<DataID,int> currentResources;

    public int food;
    public int wood;
    public int metal;
    public int stone;
    public int gold;

    public int foodStorage;
    public int woodStorage;
    public int metalStorage;
    public int stoneStorage;
    public int goldStorage;

    // will introduce maximum storage allowed later
    // public Dictionary<DataID, int> currentMaxStorage;
}