using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerManager : MonoBehaviour
{

    #region Variables
    List<BuildingTracker> buildingList;
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

    // for camera controls (temporary until after milestone 2, just for working sake)
    private Vector2 move;
    [SerializeField] private float cameraSpeed = 10f;
    #endregion

    void Awake()
    {
        playerInput = new PlayerInput();
        Cursor.visible = true;
        tiles = new List<TileBase>();
        buildingList = new List<BuildingTracker>();
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
            //Put tile selection code here.

            highlight.ClearAllTiles();
            highlight.SetTile(gridPosition, highlightTile);

            // Place Building at Selected Tile
            // canBuild necessary so that we dont make copies - Shobhit
            canBuild = !buildings.HasTile(gridPosition) && highlight.HasTile(gridPosition) ? true : false;
            if (canBuild)
                highlightedPosition = gridPosition;
            

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
                buildingList.Add(building);
                canBuild = false;
            }
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
