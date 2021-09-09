using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    // private TileManager grid; Create maybe a 32x32 grid for now? This has data of each tile/node and their positions.
    // 
    //
    // 


    // Create properties to view terrain types, pollution levels, etc.


    /// <summary>
    /// This function allows for the tiles to change beautifully when you f*ck up and pollute your world too much :)
    /// Uses Dijsktra's Algorithm to traverse through all the tiles. Doesn't necessarily finds a shortest path BUT we think 
    /// it looks pretty cool in the way that it searches each node in a visual aspect.
    /// Doesn't return anything since we aren't searching for a shortest path. It will just change the way each tile looks.
    /// </summary>
    /// <param name="graph">The total graph/grid of the tiles</param>
    /// <param name="source">The starting/source node of the search</param>
    public void ChangeTerrain(int[,] graph, int source)
    {
        // Dijsktra's Algorithm here to beautifully change tiles when your pollution gets to the next threshold
        // Here's a reminder of how to do that algorithm: https://www.geeksforgeeks.org/csharp-program-for-dijkstras-shortest-path-algorithm-greedy-algo-7/


    }

    /// <summary>
    /// We can randomly generate the map each game
    /// </summary>
    private void GenerateMap()
    {
        // Terrain types are from 0 - 3. We want at least one 0 for the town center.
        // 0 = Plains, 1 = Hill, 2 = Mountains, 3 = Water
        // Add Tiles to grid and create their positions in their scripts.
    }


}
