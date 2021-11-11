using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerManager : MonoBehaviour
{

    #region Variables
    //List<BuildingTracker> buildingList;
    Dictionary<Vector3Int, BuildingTracker> buildingList;
    [SerializeField] private GameObject tempBuildingObj;

    private PlayerInput playerInput;
    public TileBase selectedBuilding;
    private Vector3Int highlightedPosition;
    private bool canBuild = true;
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


    // for camera controls (temporary until after milestone 2, just for working sake)
    private Vector2 move;
    [SerializeField] private float cameraSpeed = 10f;
    #endregion

    void Awake()
    {
        playerInput = new PlayerInput();
        Cursor.visible = true;
        tiles = new List<TileBase>();
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
    }

    private void Update()
    {
        move = playerInput.Keyboard.Movement.ReadValue<Vector2>();
        Vector3 movement = new Vector3(move.x * cameraSpeed * Time.deltaTime, move.y * cameraSpeed * Time.deltaTime, 0f);
        mainCamera.transform.Translate(movement);

        CreateResourceClicker();
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
            
            highlight.ClearAllTiles();
            highlight.SetTile(gridPosition, highlightTile);

            // Place Building at Selected Tile
            // canBuild necessary so that we dont make copies - Shobhit
            canBuild = !buildings.HasTile(gridPosition) && highlight.HasTile(gridPosition) ? true : false;
            if (canBuild)
                highlightedPosition = gridPosition;

            ResourceClicker(gridPosition);

        }
    }

    public void SelectBuilding(TileBase tile)
    {
        // Scale the building down = Done :+1: - Durrell
        // Need to build when requirements are met
        if (canBuild)
        {
            GameObject temp = Instantiate(tempBuildingObj);

            if (ResourceManager.instance.CheckRequirements(GameManager.instance.dataIDList.FindDataID(tile.name.ToLower()), 1))
            {
                buildings.SetTile(highlightedPosition, tile);
                switch (tile.name.ToLower())
                {
                    case "towncenter":
                        temp.AddComponent<TownCenter>();
                        break;
                    case "farm":
                        temp.AddComponent<Farm>();
                        break;
                    case "house":
                        temp.AddComponent<House>();
                        break;
                    case "factory":
                        temp.AddComponent<Factory>();
                        break;
                    case "filterationplant":
                        temp.AddComponent<FilterPlants>();
                        break;
                }
                BuildingTracker building = new BuildingTracker(highlightedPosition, tile, temp);
                buildingList.Add(highlightedPosition, building);
                canBuild = false;
            }
        }
    }

    /// <summary>
    /// Runs on coroutine to create new clicker tiles and swaps them out when timer is complete, rinse and repeat.
    /// -- Durrell
    /// </summary>
    private void CreateResourceClicker()
    {
        int randomX = Random.Range(-3, 4);
        int randomY = Random.Range(-3, 3);

        // get random position on tilemap.
        Vector3Int position = new Vector3Int(randomX, randomY, 0);

        if(map.HasTile(position))
        {
            // make sure a building doesn't exist on that tile.
            if (buildingList.ContainsKey(position)) // if so, find another position.
            {
                // Create new random position.
                randomX = Random.Range(-3, 4);
                randomY = Random.Range(-3, 3);

                // get random position on tilemap.
                position = new Vector3Int(randomX, randomY, 0);
            }
            else // if not, proceed.
            {
                // random clicker tile
                int random = Random.Range(0, 2);

                // swap tile with resource clicker tile.
                // 0 = Wood, 1 = Stone, 2 = Metal
                switch (random)
                {
                    case 0:
                        map.SwapTile(map.GetTile(position), woodTile);
                        print("resource clicker created");
                        break;
                    case 1:
                        print("resource clicker created");
                        map.SwapTile(map.GetTile(position), stoneTile);
                        break;
                    case 2:
                        print("resource clicker created");
                        map.SwapTile(map.GetTile(position), metalTile);
                        break;
                    default:
                        break;
                }
            }


            // replace tile when completed.
            map.SwapTile(map.GetTile(position), grassTile);
        }

    }

    /// <summary>
    /// Checks if tile is a resource clicker or not.
    /// -- Durrell
    /// </summary>
    /// <param name="position"></param>
    private void ResourceClicker(Vector3Int position)
    {
        // Create cookie clicker element on tiles - Durrell
        // check if tile has resource item on it.
        var tile = map.GetTile(position);
        switch (tile.name)
        {
            case "wood_clicker":
                // Add resources for wood when clicked.
                break;
            case "stone_clicker":
                // Add resources for stone when clicked.
                break;
            case "metal_clicker":
                // Add resources for metal when clicked.
                break;
            default:
                print("Tile is not a clicker!");
                break;
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
