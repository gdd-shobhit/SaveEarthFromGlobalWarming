using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerManager : MonoBehaviour
{

    #region Variables
    List<BuildingTracker> buildingList;
    [SerializeField] private GameObject tempBuildingObj;

    private MouseInput mouseInput;
    private Cam cameraInput;
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


    #endregion

    void Awake()
    {
        mouseInput = new MouseInput();
        cameraInput = new Cam();
        Cursor.visible = true;
        tiles = new List<TileBase>();
        buildingList = new List<BuildingTracker>();
    }

    void OnEnable()
    {
        mouseInput.Enable();
        cameraInput.Enable();
    }

    void OnDisable()
    {
        mouseInput.Disable();
        cameraInput.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        mouseInput.Mouse.MouseClick.performed += _ => MouseClick();
        cameraInput.Keyboard.Keyboard.performed += _ => MoveCamera();
    }

    /// <summary>
    /// Main method for tile selection.
    /// </summary>
    void MouseClick()
    {

        Vector2 mousePosition = mouseInput.Mouse.MousePosition.ReadValue<Vector2>();
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
            if (tile.name.ToLower().Equals("towncenter"))
                temp.AddComponent<TownCenter>();

            else if (tile.name.ToLower().Equals("farm"))
                temp.AddComponent<Farm>();
            else if (tile.name.ToLower().Equals("house"))
                temp.AddComponent<House>();
            else if (tile.name.ToLower().Equals("filterationplant"))
                temp.AddComponent<FilterPlants>();
            else if (tile.name.ToLower().Equals("factory"))
                temp.AddComponent<Factory>();

            if (ResourceManager.instance.CheckRequirements(temp.GetComponent<Building>().DID, 1))
            {
                buildings.SetTile(highlightedPosition, tile);
                BuildingTracker building = new BuildingTracker(highlightedPosition, tile, temp);
                buildingList.Add(building);
                canBuild = false;
            }
            else
            {
                // NEED TO RETHINK THE LOGIC. DESTROY SHOULD NOT BE USED - Shobhit
                Destroy(temp);
            }
        }

    }

    /// <summary>
    /// Needs plenty of work to work smoothly - Can integrate Cinemachine. - Durrell
    /// </summary>
    void MoveCamera()
    {
        float speed = 10f;
        Vector2 movement = cameraInput.Keyboard.Keyboard.ReadValue<Vector2>();
        mainCamera.transform.Translate(new Vector3(movement.x * Time.deltaTime * speed, movement.y * Time.deltaTime * speed, 0f));
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
