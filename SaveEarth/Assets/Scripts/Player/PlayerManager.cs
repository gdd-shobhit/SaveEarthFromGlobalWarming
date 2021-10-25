using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerManager : MonoBehaviour
{
    private MouseInput mouseInput;
    private Cam cameraInput;

    [SerializeField] private Camera mainCamera;
    [SerializeField] private Tilemap map;
    [SerializeField] private Tilemap buildings;
    [SerializeField] private Tilemap highlight;
    [SerializeField] private Grid grid;

    [SerializeField] private Tile grassTile;
    [SerializeField] private Tile dirtTile;
    [SerializeField] private Tile highlightTile;

    void Awake()
    {
        mouseInput = new MouseInput();
        cameraInput = new Cam();
        Cursor.visible = true;
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
        map.SwapTile(dirtTile, grassTile);

        Vector2 mousePosition = mouseInput.Mouse.MousePosition.ReadValue<Vector2>();
        mousePosition = mainCamera.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, 7f));
        Vector3Int gridPosition = grid.WorldToCell(mousePosition);

        // On Tile Click
        if (map.HasTile(gridPosition))
        {
            //Put tile selection code here.
            print($"Get Tile: {map.GetTile(gridPosition)}");
            print($"Mouse pos: {mousePosition}");
            print($"Grid Pos: {gridPosition}");

            //map.SetTile(gridPosition, dirtTile);
            highlight.ClearAllTiles();
            highlight.SetTile(gridPosition, highlightTile);

        }

        // Place Building at Selected Tile
        if (buildings.HasTile(gridPosition) && highlight.HasTile(gridPosition))
        {

        }
    }

    void MoveCamera()
    {
        float speed = 20f;
        Vector2 movement = cameraInput.Keyboard.Keyboard.ReadValue<Vector2>();
        mainCamera.transform.position += new Vector3(movement.x * speed * Time.deltaTime, movement.y * speed * Time.deltaTime, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
