using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    private int _pollutionLevel = 0; // Pollution level of the tile. (Create a property for this)
    private int _terrain = 0; // Is this a water tile, plains tile, mountains, etc.
    // private int[,] position; Keeps track of this tiles position on the map. Can be set by the grid.
    // private int buildingType = 000; This keeps track of the building(s) if any on this tile.

    /// <summary>
    /// Plains = 0, Hill = 1, Mountain = 2, Water = 3
    /// </summary>
    public int Terrain { get => _terrain; set => _terrain = value; }
    public int PollutionLevel { get => _pollutionLevel; }
    
    /// <summary>
    /// If a building is added that creates pollution on this tile add pollution to tile at end of day cycle.
    /// </summary>
    /// <param name="pollution"></param>
    /// <returns></returns>
    public int ChangePollution(int pollution)
    {
        return _pollutionLevel += pollution;
    }


}
