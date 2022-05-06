using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.Experimental.Rendering.Universal;
using TMPro;

/// <summary>
/// A lot of it will get deprecated due to shifting to 3D
/// </summary>
public class PlayerManager : MonoBehaviour
{

    #region Variables
    //List<BuildingTracker> buildingList;
    Dictionary<Vector3Int, BuildingTracker> buildingList;
    [SerializeField] private GameObject tempBuildingObj;

    private PlayerInput playerInput;
    public GameObject resourcesRequiredPanel; 
    private Vector3Int highlightedPosition;
    private bool canBuild = true;
    public string buildingToBeBuild = "";
    private TileBase tileToBePlace = null;
    [SerializeField] private List<TileBase> tiles;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Tilemap map;
    [SerializeField] private Tilemap buildings;
    [SerializeField] private Tilemap highlight;
    [SerializeField] private Grid grid;

    [SerializeField] private Tile grassTile;
    [SerializeField] private Tile dirtTile;
    [SerializeField] private Tile highlightTile;
    [SerializeField] private Tile woodTile;
    [SerializeField] private Tile stoneTile;
    [SerializeField] private Tile metalTile;

    [SerializeField] private float clickerTimer = 10.0f;
    private IEnumerator coroutine;
    private Vector3Int clickerPosition;

    // for camera controls (temporary until after milestone 2, just for working sake)
    private Vector2 move;
    [SerializeField] private float cameraSpeed = 10f;
    #endregion

    void Awake()
    {
        map.CompressBounds();
        playerInput = new PlayerInput();
        Cursor.visible = true;
        buildingList = new Dictionary<Vector3Int, BuildingTracker>();
    }

    void OnEnable()
    {
        playerInput.Enable();
    }

    void OnDisable()
    {
        playerInput.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        playerInput.Mouse.MouseClick.performed += _ => MouseClick();
        coroutine = CreateClicker(clickerTimer);
        StartCoroutine(coroutine);
    }

    private void Update()
    {
        move = playerInput.Keyboard.Movement.ReadValue<Vector2>();
        Vector3 movement = new Vector3(move.x * cameraSpeed * Time.deltaTime, move.y * cameraSpeed * Time.deltaTime, 0f);
        mainCamera.transform.Translate(movement);
    }

    /// <summary>
    /// Main method for tile selection.
    /// </summary>
    void MouseClick()
    {
        Vector2 mousePosition = playerInput.Mouse.MousePosition.ReadValue<Vector2>();
        mousePosition = mainCamera.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, 7f));
        Vector3Int gridPosition = grid.WorldToCell(mousePosition);

        // On Tile Click
        if (map.HasTile(gridPosition))
        {
            print($"Grid Position: {gridPosition}");

            //Put tile selection code here.
            //Deprecated Code for highlighting
            //highlight.ClearAllTiles();
            //highlight.SetTile(gridPosition, highlightTile);
            
            // Highlighting Tile with Light 2D - Shobhit Dhamania
            Transform something = GameObject.FindGameObjectWithTag("SelectionLight").transform;
            something.position = grid.GetCellCenterWorld(gridPosition);
            StopAllCoroutines();
            StartCoroutine(Fade(something.gameObject));
            //GameObject.FindGameObjectWithTag("SelectionLight").transform.position = new Vector3(tx,ty,0);
            // Place Building at Selected Tile
            // canBuild necessary so that we dont make copies - Shobhit
            canBuild = !buildings.HasTile(gridPosition)? true : false;
            if (canBuild)
                highlightedPosition = gridPosition;
            else
            {
                //check for what building
                Building selectedBuilding = buildingList[gridPosition].GameObj.GetComponent<Building>();

                selectedBuilding.LevelUp();
                // get its information 
                // check if can upgrade - probably make a method
                // display the upgrade UI somehow
                // change the level and output rates according to the level
                // play some Particle System
            }

        }
    }


    // Coroutine for selection of Tile 
    IEnumerator Fade(GameObject gameObject)
    {
        Light2D lt = gameObject.GetComponent<Light2D>();
        float c = lt.intensity;
        for (float alpha = 0.44f; alpha >= 0; alpha -= 0.03f)
        {
            Debug.Log("here");
            c = alpha;
            lt.intensity = c;
            yield return new WaitForSeconds(0.05f);
        }

        for (float alpha = 0f; alpha <= 0.44; alpha += 0.03f)
        {
            Debug.Log("here");
            c = alpha;
            lt.intensity = c;
            yield return new WaitForSeconds(0.05f);
        }

        StartCoroutine(Fade(gameObject));

    }

    public void SelectBuilding(GameObject building)
    {
        // Scale the building down = Done :+1: - Durrell
        // Need to build when requirements are met
        //buildingToBeBuild = building.name.ToLower();
        //ResourceManager.instance.DisplayInfo(building.name, 1);
    }

    public void BuildBuilding()
    {
        if (canBuild)
        {
            GameObject temp = Instantiate(tempBuildingObj);
            //if (ResourceManager.instance.CheckRequirements(buil, 1))
            //{
                switch (buildingToBeBuild)
                {
                    case "towncenter":
                        {
                            buildings.SetTile(highlightedPosition, tiles[0]);
                            temp.AddComponent<TownCenter>();
                        }
                       
                        break;
                    case "farm":
                        {
                            buildings.SetTile(highlightedPosition, tiles[1]);
                            temp.AddComponent<Farm>();
                        }                     
                        break;
                    case "factory":
                        {
                            buildings.SetTile(highlightedPosition, tiles[2]);
                            temp.AddComponent<Factory>();
                        }
                      
                        break;
                    case "filterationplant":
                        {
                            buildings.SetTile(highlightedPosition, tiles[3]);
                            temp.AddComponent<FilterPlants>();
                        }
                        break;
                    case "house":
                        {
                            buildings.SetTile(highlightedPosition, tiles[4]);
                            temp.AddComponent<House>();
                        }
                        
                        break;
                }
                BuildingTracker building = new BuildingTracker(highlightedPosition, tileToBePlace, temp);
                buildingList.Add(highlightedPosition, building);
                canBuild = false;
          
                Destroy(temp);
            

        }
        
    }

    /// <summary>
    /// Runs on coroutine to create new clicker tiles and swaps them out when timer is complete, rinse and repeat.
    /// -- Durrell
    /// </summary>
    private void CreateResourceClicker()
    {
        // Get random position on tilemap.
        var gridSize = map.size;
        var randomPos = new Vector3Int(Random.Range(0, gridSize.x), Random.Range(0, gridSize.y), Random.Range(0, gridSize.z));
        var randomTile = map.GetTile(randomPos);

        // Check if randomposition exists on existing tiles.
        if (randomTile != null)
        {
            // If building exists on tile, re-run our randomizer
            if (buildingList.ContainsKey(randomPos))
            {
                CreateResourceClicker();
            }
            else
            {
                // Randomize which tile it becomes
                // 0 - Wood     1 - Stone       2 - Metal
                int random = Random.Range(0, 3);
                
                switch (random)
                {
                    case 0:
                        map.SetTile(randomPos, woodTile);
                        break;
                    case 1:
                        map.SetTile(randomPos, stoneTile);
                        break;
                    case 2:
                        map.SetTile(randomPos, metalTile);
                        break;
                    default:
                        break;
                }
            }

            clickerPosition = randomPos;
        }
        else // If tile doesn't exist on the map, find another random position.
        {
            CreateResourceClicker();
        }
    }

    private IEnumerator CreateClicker(float timer)
    {
        while (true)
        {
            yield return new WaitForSeconds(timer);
            map.SetTile(clickerPosition, grassTile);
            CreateResourceClicker();
        }
    }

}

public class BuildingTracker
{
    protected int pollution;
    protected Vector3Int position;
    protected string building;
    protected TileBase tile;
    protected GameObject gameObject;

    public Vector3Int Position { get => position; set => position = value; }
    public int Pollution { get => pollution; }
    public string Building { get => building; }
    public TileBase Tile { get => tile; }
    public GameObject GameObj { get => gameObject; }

    public BuildingTracker(Vector3Int pos, TileBase _tile, GameObject _gameObj)
    {
        position = pos;
        //building = _building;
        tile = _tile;
        gameObject = _gameObj;
    }
}
