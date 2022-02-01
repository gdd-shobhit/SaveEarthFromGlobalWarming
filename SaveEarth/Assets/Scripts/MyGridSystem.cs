using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class MyGridSystem : MonoBehaviour
{
    public enum Resources
    {
        None,
        Wood,
        Stone,
        Metal
    }

    [SerializeField]
    private GameObject testBuilding;
    private Grid myGrid;
    public Vector3 cellSize;
    public GameObject gridLines;
    public List<GridObject> gridObjects;
    private Vector2 gridSize;
    private void Awake()
    {
        myGrid = GetComponent<Grid>();
        gridObjects = new List<GridObject>();
        cellSize = myGrid.cellSize;
        gridSize = new Vector2(10, 10);


        for (int i = 0; i < gridSize.x; i++)
        {
            for (int j = 0; j < gridSize.y; j++)
            {
                Vector3 worldLocation = myGrid.GetCellCenterWorld(new Vector3Int(i * (int)cellSize.x, j * (int)cellSize.y, 0));
                GridObject toAddInList = new GridObject(Instantiate(gridLines, worldLocation, Quaternion.identity), myGrid);
                gridObjects.Add(toAddInList);
            }
        }
    }

    /// <summary>
    /// An Object that will contain information like position, value, type etc
    /// </summary>
    public class GridObject
    {
        // just for visual reference, can be ommitted into transparent meshes for invisible grid of gridobjects
        GameObject visualObject;
        public GameObject buildingObject;
        public Vector3 worldLocation;
        public Resources TileType;
        // assuming the everything is empty
        public bool canBuild;
        Grid grid;
        public GridObject(GameObject gameObject, Grid grid)
        {
            this.grid = grid;
            visualObject = gameObject;
            worldLocation = visualObject.transform.position;
            canBuild = true;
        }

        private void OnDrawGizmos()
        {
            Handles.Label(worldLocation, "("+worldLocation.x+","+worldLocation.y+")", GUIStyle.none);
        }

    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            StopAllCoroutines();
            Vector3 positionToBePlaced = GetExactCenter(GetMouseWorldPosition());

            Debug.Log(positionToBePlaced);
            foreach (GridObject gridObject in gridObjects)
            {
                GameObject instantiatedBuilding;
                if (gridObject.worldLocation.x == positionToBePlaced.x && gridObject.worldLocation.z == positionToBePlaced.z)
                {
                    if(gridObject.canBuild)
                    {
                        instantiatedBuilding = Instantiate(testBuilding, positionToBePlaced, Quaternion.identity);
                        gridObject.canBuild = false;
                        gridObject.buildingObject = instantiatedBuilding;
                        // go into Coroutine to Show Selection Mode - there is a 'Building' there
                        return;
                    }
                    else
                    {
                        StartCoroutine(SelectionOfBuilding(gridObject));
                    }                 
                }
            }       
        }
    }

    IEnumerator SelectionOfBuilding(GridObject incomingGridObject)
    {
        Vector3 scale = incomingGridObject.buildingObject.transform.localScale;
        incomingGridObject.buildingObject.transform.localScale *= 1.25f;
        yield return new WaitForSeconds(0.2f);
        incomingGridObject.buildingObject.transform.localScale = Vector3.Lerp(scale, incomingGridObject.buildingObject.transform.localScale,0f);

    }


    /// <summary>
    /// Ray Casts in 3D to get mouse position with respect to World
    /// </summary>
    /// <returns></returns>
    private Vector3 GetMouseWorldPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit rayHit, 999f))
        {
            return rayHit.point;
        }
        else
        {
            return Vector3.zero;
        }
    }

    /// <summary>
    /// Snaps the building to the Center
    /// </summary>
    /// <param name="incomingVector"></param>
    /// <returns>Center for the mouse position selected as a Vector3</returns>
    private Vector3 GetExactCenter(Vector3 incomingVector)
    {
        Vector3 center = new Vector3((int)incomingVector.x, (int)incomingVector.y, (int)incomingVector.z);
        center.x += 0.5f;
        center.y += 0.5f;
        center.z += 0.5f;

        return center;

    }

    
}



