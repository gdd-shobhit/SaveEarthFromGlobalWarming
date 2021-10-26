using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileManager : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap;
    GridLayout gridLayout;

    private int _pollutionLevel = 0; // Pollution level of the map. (Create a property for this)
    public int PollutionLevel { get => _pollutionLevel; }

    private void Awake()
    {
        gridLayout = tilemap.layoutGrid;
    }

    

    /// <summary>
    /// If pollution raises too high, change every tile on the map algorithmically.
    /// </summary>
    /// <returns> 0 if successful. 1 if unsuccessful</returns>
    private int ChangeAlgo()
    {
        for (int i = 0; i < 100; i++)
        {
            // Use tilemap.layoutgrid to find the grid. Can use that to find local coordinates of cells to alter them.
            print(gridLayout.cellLayout);
        }
        return 1;
    }

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
