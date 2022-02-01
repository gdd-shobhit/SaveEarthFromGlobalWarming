using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Resource", menuName = "ScriptableObjects/ResourceObjects", order = 1)]
public class ResourceSO : ScriptableObject
{
    public DataID dataId;

    public GameObject resourcePrefab;

    // Will act as resource amount
    public int hitPoints;

    // Worker Attack - resistance for actual mining
    public int resistance;

    public List<Vector2> positionOnMap;
}
