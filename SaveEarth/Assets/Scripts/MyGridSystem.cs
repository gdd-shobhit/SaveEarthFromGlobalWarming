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

    public enum Mode
    {
        Brush,
        Selection
    }

    public enum BuildingBrush
    {
        Towncenter,
        House
    }

    [SerializeField]
    private GameObject testBuilding;
    public List<GameObject> testBuildingsPrefabs;
    public GameObject ghost;
    private Grid myGrid;
    public Vector3 cellSize;
    public GameObject gridLines;
    public List<GridObject> gridObjects;
    private Vector2 gridSize;
    public static MyGridSystem instance;
    public Mode currentMode;
    public GameObject buildingPS;
    public BuildingBrush currentBuildingBrush;
    
    public List<Material> buildMaterials;


    private void Awake()
    {
        currentMode = Mode.Selection;
        myGrid = GetComponent<Grid>();
        gridObjects = new List<GridObject>();
        cellSize = myGrid.cellSize;
        gridSize = new Vector2(30, 30);
        ghost = Instantiate(testBuilding);
        ghost.SetActive(false);
        currentBuildingBrush = BuildingBrush.House;
        //ghost.AddComponent<GhostBuilding>();

        for (int i = 0; i < gridSize.x; i++)
        {
            for (int j = 0; j < gridSize.y; j++)
            {
                Vector3 worldLocation = myGrid.GetCellCenterWorld(new Vector3Int(i * (int)cellSize.x, j * (int)cellSize.y, 0));
                GridObject toAddInList = new GridObject(Instantiate(gridLines, worldLocation, Quaternion.identity), myGrid);
                gridObjects.Add(toAddInList);
            }
        }
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }
    }



    public GridObject GetGridObject(float x, float z)
    {
        foreach (GridObject gridObject in gridObjects)
        {
            if (gridObject.worldLocation.x == x && gridObject.worldLocation.z == z)
            {
                return gridObject;
            }
        }

        return null;
    }

    /// <summary>
    /// An Object that will contain information like position, value, type etc
    /// </summary>
    public class GridObject
    {
        // just for visual reference, can be ommitted into transparent meshes for invisible grid of gridobjects
        public GameObject visualObject;
        public GameObject buildingObject;
        public BuildingSO buildingSO;
        public Vector3 worldLocation;
        // for bigger buildings
        public List<Vector3> allWorldLocation;
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
    }

    private void Update()
    {
        //ChangeMode();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (currentMode == Mode.Brush)
            {
                currentMode = Mode.Selection;
                ghost.SetActive(false);
            }
            else
            {
                currentMode = Mode.Brush;
                ghost.SetActive(true);
            }
        }

        if (currentMode == Mode.Brush)
        {
            bool canBuild = false;
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
               
                if (currentBuildingBrush == BuildingBrush.Towncenter)
                {
                    currentBuildingBrush = BuildingBrush.House;
                    ghost.GetComponent<GhostBuilding>().currentType = GhostBuilding.BuildingType.House;
                }
                else
                {
                    currentBuildingBrush= BuildingBrush.Towncenter;
                    ghost.GetComponent<GhostBuilding>().currentType = GhostBuilding.BuildingType.Towncenter;
                }

                CheckBuildingBrush();
            }
            // adds a temporary building to see where to place it
            if (ghost.GetComponent<GhostBuilding>() == null)
                ghost.AddComponent<GhostBuilding>();

            if (Input.GetKeyDown(KeyCode.Tab))
                ChangeRotation(ghost.GetComponent<GhostBuilding>());

            Vector3 positionToPlaced = GetExactCenter(GetMouseWorldPosition());

            GridObject gridObject1 = GetGridObject(positionToPlaced.x, positionToPlaced.z);

            if(gridObject1 != null && gridObject1.canBuild)
            {
                if (ghost.GetComponent<GhostBuilding>().buildingData != null && ghost.GetComponent<GhostBuilding>().buildingData.size == 2)
                {
                    Vector3 something = gridObject1.visualObject.transform.position;

                    if(!GetGridObject(something.x + 1f, something.z).canBuild || !GetGridObject(something.x + 1f, something.z + 1).canBuild || !GetGridObject(something.x, something.z + 1f).canBuild)
                    {
                        // red
                        ghost.GetComponent<GhostBuilding>().rendererForBuilding.GetComponent<Renderer>().material = buildMaterials[1];
                        canBuild = false;
                    }
                    else
                    {
                        //Color.green;
                        ghost.GetComponent<GhostBuilding>().rendererForBuilding.GetComponent<Renderer>().material = buildMaterials[0];
                        canBuild = true;
                    }                
                }
                else
                {
                    //Color.green;
                    ghost.GetComponent<GhostBuilding>().rendererForBuilding.GetComponent<Renderer>().material = buildMaterials[0];
                    canBuild = true;
                }
            }
            else
            {
                //Color.red;
                ghost.GetComponent<GhostBuilding>().rendererForBuilding.GetComponent<Renderer>().material = buildMaterials[1];
                canBuild = false;
            }

            if (Input.GetMouseButtonDown(0) && canBuild)
            {
                
                if(currentBuildingBrush == BuildingBrush.House)
                    ghost.GetComponent<GhostBuilding>().rendererForBuilding.GetComponent<Renderer>().material = buildMaterials[2];
                else if (currentBuildingBrush == BuildingBrush.Towncenter)
                    ghost.GetComponent<GhostBuilding>().rendererForBuilding.GetComponent<Renderer>().material = buildMaterials[3];
                Destroy(ghost.GetComponent<GhostBuilding>());
                StopAllCoroutines();
                Vector3 positionToBePlaced = GetExactCenter(GetMouseWorldPosition());

                GridObject gridObject = GetGridObject(positionToBePlaced.x, positionToBePlaced.z);
                if (gridObject != null && gridObject.canBuild)
                {
                    
                    gridObject.buildingObject = Instantiate(ghost);
                    GameObject ps = Instantiate(buildingPS, positionToBePlaced, Quaternion.identity);
                    Vector3 actualPosition = ghost.transform.position;
                    actualPosition.y = -0.25f;
                    if (ghost.GetComponent<GhostBuilding>().buildingData != null && ghost.GetComponent<GhostBuilding>().buildingData.size == 2)
                    {
                        OccupyMoreSpace(gridObject);
                        actualPosition.y = -1.25f;
                    }
                    
                    gridObject.buildingObject.transform.position = Vector3.Lerp(gridObject.buildingObject.transform.position, actualPosition, Time.deltaTime * 20f);
                    gridObject.canBuild = false;
                  
                    CheckBuildingBrush();
                }
                else
                {
                    StartCoroutine(SelectionOfBuilding(gridObject));
                    // for testing purposes
                    gridObject.buildingObject.GetComponent<Building>().LevelUp();
                }
            }
        }
    }

    private void OccupyMoreSpace(GridObject go)
    {
        Vector3 something = go.visualObject.transform.position;
        GetGridObject(something.x + 1f, something.z).canBuild = false;

        GetGridObject(something.x + 1f, something.z+1).canBuild = false;


        GetGridObject(something.x, something.z +1f).canBuild = false;
    }

    private void CheckBuildingBrush()
    {
        switch (currentBuildingBrush)
        {
            case BuildingBrush.Towncenter:
                Destroy(ghost);
                ghost = Instantiate(testBuildingsPrefabs[1]);
                
                
                break;
            case BuildingBrush.House:
                Destroy(ghost);
                ghost = Instantiate(testBuildingsPrefabs[0]);
                break;
        }
    }

    public void ChangeMode()
    {
        currentMode = Mode.Brush;
        ghost.SetActive(true);
    }

    private void ChangeRotation(GhostBuilding ghost)
    {
        switch(ghost.currentDir)
        {
            case GhostBuilding.Dir.Up: ghost.currentDir = GhostBuilding.Dir.Right; break;
            case GhostBuilding.Dir.Right: ghost.currentDir = GhostBuilding.Dir.Down; break;
            case GhostBuilding.Dir.Down: ghost.currentDir = GhostBuilding.Dir.Left; break;
            case GhostBuilding.Dir.Left: ghost.currentDir = GhostBuilding.Dir.Up; break;
        }
    }

    IEnumerator SelectionOfBuilding(GridObject incomingGridObject)
    {
        Vector3 scale = incomingGridObject.buildingObject.transform.localScale;
        incomingGridObject.buildingObject.transform.localScale *= 1.25f;
        yield return new WaitForSeconds(0.2f);
        incomingGridObject.buildingObject.transform.localScale = Vector3.Lerp(scale, incomingGridObject.buildingObject.transform.localScale, 0f);

    }


    /// <summary>
    /// Ray Casts in 3D to get mouse position with respect to World
    /// </summary>
    /// <returns></returns>
    public Vector3 GetMouseWorldPosition()
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
    public Vector3 GetExactCenter(Vector3 incomingVector)
    {
        Vector3 center = new Vector3((int)incomingVector.x, (int)incomingVector.y, (int)incomingVector.z);
        center.x += 0.5f;
        center.y += 0.5f;
        center.z += 0.5f;

        return center;

    }

}



