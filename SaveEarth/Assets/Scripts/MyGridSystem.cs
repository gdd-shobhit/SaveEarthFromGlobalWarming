using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
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
        House,
        Factory,
        Farm,
        FilterationPlant
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
    public GameObject treeCluster;
    public GameObject rocks;
    public GameObject crystals;
    public GameObject health;
    public TextMeshProUGUI healthValue;
    public GameObject costPanel;
    public TextMeshProUGUI currentWorker;
    public TextMeshProUGUI costPanelHeading;
    public List<TextMeshProUGUI> costText;

    public List<Material> buildMaterials;


    private void Awake()
    {
        currentMode = Mode.Selection;
        myGrid = GetComponent<Grid>();
        gridObjects = new List<GridObject>();
        cellSize = myGrid.cellSize;
        gridSize = new Vector2(30, 30);
        ghost = Instantiate(testBuilding);
        ghost.GetComponent<GhostBuilding>().costPanel = costPanel;
        ghost.SetActive(false);
        currentBuildingBrush = BuildingBrush.Towncenter;
        //ghost.AddComponent<GhostBuilding>();

        for (int i = 0; i < gridSize.x; i++)
        {
            for (int j = 0; j < gridSize.y; j++)
            {
                Vector3 worldLocation = myGrid.GetCellCenterWorld(new Vector3Int(i * (int)cellSize.x, j * (int)cellSize.y, 0));
                GridObject toAddInList = new GridObject(Instantiate(gridLines, worldLocation, Quaternion.identity), myGrid);
                int randomNess = UnityEngine.Random.Range(1, 101);
                //int randomNessStone = UnityEngine.Random.Range(1, 101);
                if (randomNess % 4 == 0)
                {
                    worldLocation.y = 0.75f;
                    toAddInList.buildingObject = Instantiate(treeCluster, worldLocation, Quaternion.identity);
                    toAddInList.resourceSO = GameManager.instance.resourceSOs[1];
                    toAddInList.type = GridObject.Type.Resource;
                    toAddInList.buildingObject.transform.Rotate(Vector3.up, UnityEngine.Random.Range(0, 360));
                    toAddInList.canBuild = false;
                }
                else if (randomNess % 13 == 0)
                {
                    worldLocation.y = 0.8f;
                    toAddInList.buildingObject = Instantiate(rocks, worldLocation, Quaternion.identity);
                    toAddInList.resourceSO = GameManager.instance.resourceSOs[2];
                    toAddInList.type = GridObject.Type.Resource;
                    toAddInList.buildingObject.transform.Rotate(Vector3.up, UnityEngine.Random.Range(0, 360));
                    toAddInList.canBuild = false;

                }
                else if (randomNess % 41 == 0)
                {
                    worldLocation.y = 1f;
                    toAddInList.buildingObject = Instantiate(crystals, worldLocation, Quaternion.identity);
                    toAddInList.resourceSO = GameManager.instance.resourceSOs[0];
                    toAddInList.type = GridObject.Type.Resource;
                    toAddInList.buildingObject.transform.Rotate(Vector3.up, UnityEngine.Random.Range(0, 360));
                    toAddInList.canBuild = false;
                }
                else
                {
                    toAddInList.type = GridObject.Type.Empty;
                }
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
        public ResourceSO resourceSO;
        public Type type;
        public enum Type
        {
            Building,
            Resource,
            Empty
        }
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
                costPanel.SetActive(false);
            }
            else
            {
                currentMode = Mode.Brush;
                ghost.SetActive(true);
                costPanel.SetActive(true);
                costPanelHeading.text = "Town Center";
                costText[0].text = ghost.GetComponent<GhostBuilding>().buildingData.costProg.foodValues[0].ToString();
                costText[1].text = ghost.GetComponent<GhostBuilding>().buildingData.costProg.woodValues[0].ToString();
                costText[2].text = ghost.GetComponent<GhostBuilding>().buildingData.costProg.metalValues[0].ToString(); 
                costText[3].text = ghost.GetComponent<GhostBuilding>().buildingData.costProg.goldValues[0].ToString();
                costText[4].text = ghost.GetComponent<GhostBuilding>().buildingData.costProg.stoneValues[0].ToString();
                costText[5].text = ghost.GetComponent<GhostBuilding>().buildingData.pollutionProg.levelProg[1].ToString();

            }
        }

        if (currentMode == Mode.Brush)
        {
            bool canBuild = false;
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                if (!GameManager.instance.townHallPresent)
                    ghost.GetComponent<GhostBuilding>().currentType = GhostBuilding.BuildingType.Towncenter;
                else
                {
                    switch (currentBuildingBrush)
                    {
                        case BuildingBrush.House:
                            currentBuildingBrush = BuildingBrush.Factory;
                            ghost.GetComponent<GhostBuilding>().currentType = GhostBuilding.BuildingType.Factory;
                            break;
                        case BuildingBrush.Factory:
                            currentBuildingBrush = BuildingBrush.Farm;
                            ghost.GetComponent<GhostBuilding>().currentType = GhostBuilding.BuildingType.Farm;
                            break;
                        case BuildingBrush.Farm:
                            currentBuildingBrush = BuildingBrush.FilterationPlant;
                            ghost.GetComponent<GhostBuilding>().currentType = GhostBuilding.BuildingType.FilterationPlant;
                            break;
                        case BuildingBrush.FilterationPlant:
                            currentBuildingBrush = BuildingBrush.House;
                            ghost.GetComponent<GhostBuilding>().currentType = GhostBuilding.BuildingType.House;
                            break;
                    }
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

            if (gridObject1 != null && gridObject1.canBuild)
            {
                if(!ResourceManager.instance.CheckRequirements(ghost.GetComponent<GhostBuilding>().buildingData, 1))
                {
                    ghost.GetComponent<GhostBuilding>().rendererForBuilding.GetComponent<Renderer>().material = buildMaterials[1];
                    canBuild = false;
                }

                else if (ghost.GetComponent<GhostBuilding>().buildingData != null && ghost.GetComponent<GhostBuilding>().buildingData.size == 2)
                {
                    Vector3 something = gridObject1.visualObject.transform.position;

                    if (!GetGridObject(something.x + 1f, something.z).canBuild || !GetGridObject(something.x + 1f, something.z + 1).canBuild || !GetGridObject(something.x, something.z + 1f).canBuild)
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
                ResourceManager.instance.UpdateResources(int.Parse(costText[0].text), int.Parse(costText[1].text),
                    int.Parse(costText[2].text), int.Parse(costText[3].text), int.Parse(costText[4].text),false);
                switch (currentBuildingBrush)
                {
                    case BuildingBrush.House: ghost.GetComponent<GhostBuilding>().rendererForBuilding.GetComponent<Renderer>().material = buildMaterials[2];
                        ghost.AddComponent<House>();
                        break;
                    case BuildingBrush.Towncenter:
                        ghost.GetComponent<GhostBuilding>().rendererForBuilding.GetComponent<Renderer>().material = buildMaterials[4];
                        ghost.AddComponent<TownCenter>();
                        break;
                    case BuildingBrush.Factory:
                        ghost.GetComponent<GhostBuilding>().rendererForBuilding.GetComponent<Renderer>().material = buildMaterials[3];
                        ghost.AddComponent<Factory>();
                        break;
                    case BuildingBrush.FilterationPlant:
                        ghost.GetComponent<GhostBuilding>().rendererForBuilding.GetComponent<Renderer>().material = buildMaterials[5];
                        ghost.AddComponent<FilterPlants>();
                        break;
                    case BuildingBrush.Farm:
                        ghost.GetComponent<GhostBuilding>().rendererForBuilding.GetComponent<Renderer>().material = buildMaterials[6];
                        ghost.AddComponent<Farm>();
                        break;
                }
                Destroy(ghost.GetComponent<GhostBuilding>());
                
                Vector3 positionToBePlaced = GetExactCenter(GetMouseWorldPosition());
                if (currentBuildingBrush == BuildingBrush.Towncenter)
                    GameManager.instance.townHallPresent = true;

                GridObject gridObject = GetGridObject(positionToBePlaced.x, positionToBePlaced.z);
                if (gridObject != null && gridObject.canBuild)
                {
                    gridObject.buildingObject = Instantiate(ghost);
                    GameObject ps = Instantiate(buildingPS, positionToBePlaced, Quaternion.identity);
                    Vector3 actualPosition = ghost.transform.position;
                    actualPosition.y = -1.25f;
                    if (ghost.GetComponent<GhostBuilding>().buildingData != null && ghost.GetComponent<GhostBuilding>().buildingData.size == 2)
                    {
                        OccupyMoreSpace(gridObject);
                    }
                    gridObject.type = GridObject.Type.Building;
                    gridObject.buildingObject.transform.position = Vector3.Lerp(gridObject.buildingObject.transform.position, actualPosition, Time.deltaTime * 20f);
                    gridObject.canBuild = false;
                    StopCoroutine(SelectionOfBuilding(gridObject));
                    currentBuildingBrush = BuildingBrush.House;
                    CheckBuildingBrush();
                    return;
                }
                else
                {
                    StartCoroutine(SelectionOfBuilding(gridObject));
                    // for testing purposes
                    gridObject.buildingObject.GetComponent<Building>().LevelUp();
                }
            }
            else
            {
                //cannot build
                // HandleError();
            }
        }
        else
        {
            if (Input.GetMouseButton(1))
            {
                Vector3 position = GetExactCenter(GetMouseWorldPosition());
                GridObject gridObject = GetGridObject(position.x, position.z);
                if (gridObject.buildingObject == null && gridObject.resourceSO == null)
                    return;
                SelectObject(gridObject);
            }
        }
      

    }

    /// <summary>
    /// Floating text in case cannot build
    /// </summary>
    /// <param name="error"></param>
    private void HandleError(string error)
    {
        // textMeshPro.text = error;s
    }

    private void SelectObject(GridObject gridObject)
    {
        if (gridObject.resourceSO != null && GameManager.instance.currentWorker > 0)
        {
            health.SetActive(true);
            Slider slider = health.GetComponent<Slider>();
            slider.maxValue = gridObject.resourceSO.hitPoints;
            slider.value = gridObject.resourceSO.hitPoints;
            healthValue.text = slider.value.ToString();
            Slider resourceSlider = null;
            switch (gridObject.resourceSO.dataId.name)
            {
                case "metal":
                    resourceSlider = ResourceManager.instance.metalSlider;
                    break;
                case "wood":
                    resourceSlider = ResourceManager.instance.woodSlider;
                    break;
                case "crystal":
                    resourceSlider = ResourceManager.instance.crystalSlider;
                    break;
            }
            GameManager.instance.currentWorker--;
            currentWorker.text = GameManager.instance.currentWorker.ToString();

            StartCoroutine(GettingResource(gridObject, resourceSlider, slider));
        }
    }

    IEnumerator GettingResource(GridObject gridObject, Slider resourceToReflectOn, Slider resourceHealth)
    {
        while (resourceHealth.value > 0)
        {
            resourceHealth.value -= 10;
            resourceToReflectOn.value += 10;
            healthValue.text = resourceHealth.value.ToString();
            yield return new WaitForSeconds(1f);
            if (resourceHealth.value <= 0)
                break;
        }
        GameManager.instance.currentWorker++;
        currentWorker.text = GameManager.instance.currentWorker.ToString();
        gridObject.resourceSO = null;
        Destroy(gridObject.buildingObject);
        gridObject.type = GridObject.Type.Empty;
        gridObject.canBuild = true;
        health.SetActive(false);
        yield return null;
    }

    private void OccupyMoreSpace(GridObject go)
    {
        Vector3 something = go.visualObject.transform.position;
        GetGridObject(something.x + 1f, something.z).canBuild = false;

        GetGridObject(something.x + 1f, something.z + 1).canBuild = false;


        GetGridObject(something.x, something.z + 1f).canBuild = false;
    }

    private void CheckBuildingBrush()
    {
        Destroy(ghost);
        switch (currentBuildingBrush)
        {
            case BuildingBrush.Towncenter:
                ghost = Instantiate(testBuildingsPrefabs[1]);
                costPanelHeading.text = "Town Center";
                break;
            case BuildingBrush.House:
                ghost = Instantiate(testBuildingsPrefabs[0]);
                costPanelHeading.text = "House";
                break;
            case BuildingBrush.Factory:
                ghost = Instantiate(testBuildingsPrefabs[2]);
                costPanelHeading.text = "Factoru";
                break;

            case BuildingBrush.FilterationPlant:
                ghost = Instantiate(testBuildingsPrefabs[4]);
                costPanelHeading.text = "Filteration Plant";
                break;
            case BuildingBrush.Farm:
                ghost = Instantiate(testBuildingsPrefabs[3]);
                costPanelHeading.text = "Farm";
                break;
        }

        costText[0].text = ghost.GetComponent<GhostBuilding>().buildingData.costProg.foodValues[0].ToString();
        costText[1].text = ghost.GetComponent<GhostBuilding>().buildingData.costProg.woodValues[0].ToString();
        costText[2].text = ghost.GetComponent<GhostBuilding>().buildingData.costProg.metalValues[0].ToString();
        costText[3].text = ghost.GetComponent<GhostBuilding>().buildingData.costProg.goldValues[0].ToString();
        costText[4].text = ghost.GetComponent<GhostBuilding>().buildingData.costProg.stoneValues[0].ToString();
        costText[5].text = ghost.GetComponent<GhostBuilding>().buildingData.pollutionProg.levelProg[1].ToString();
        ghost.GetComponent<GhostBuilding>().costPanel = costPanel;
    }

    private void ChangeRotation(GhostBuilding ghost)
    {
        switch (ghost.currentDir)
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



