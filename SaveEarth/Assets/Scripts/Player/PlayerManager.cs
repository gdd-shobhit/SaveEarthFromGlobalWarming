using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerManager : MonoBehaviour
{

    #region Variables
    private MouseInput mouseInput;
    private Cam cameraInput;
    public TileBase selectedBuilding;
    private Vector3Int highlightedPosition;
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
            if (!buildings.HasTile(gridPosition) && highlight.HasTile(gridPosition))
            {
                Debug.Log("here");
                highlightedPosition = gridPosition;
            }

        }
    }

    public void SelectBuilding(TileBase tile)
    {
        // Scale the building down = Done :+1: - Durrell
        // Need to build when requirements are met
        buildings.SetTile(highlightedPosition, tile);
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
