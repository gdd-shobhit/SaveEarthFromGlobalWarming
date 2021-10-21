using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerManager : MonoBehaviour
{
    private MouseInput mouseInput;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Tilemap map;
    [SerializeField] private Tilemap buildings;
    [SerializeField] private Tilemap highlight;
    [SerializeField] private Grid grid;

    [SerializeField] private Tile grassTile;
    [SerializeField] private Tile dirtTile;

    void Awake()
    {
        mouseInput = new MouseInput();
        Cursor.visible = true;
    }

    void OnEnable()
    {
        mouseInput.Enable();
    }

    void OnDisable()
    {
        mouseInput.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        mouseInput.Mouse.MouseClick.performed += _ => MouseClick();
    }

    void MouseClick()
    {
        map.SwapTile(dirtTile, grassTile);

        Vector2 mousePosition = mouseInput.Mouse.MousePosition.ReadValue<Vector2>();
        mousePosition = mainCamera.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, 4.5f));
        Vector3Int gridPosition = grid.WorldToCell(mousePosition);

        
        // On Tile Click
        if (map.HasTile(gridPosition))
        {
            //Put tile selection code here.
            print($"Get Tile: {map.GetTile(gridPosition)}");
            print($"Mouse pos: {mousePosition}");
            print($"Grid Pos: {gridPosition}");

            map.SetTile(gridPosition, dirtTile);
        }


        if (buildings.HasTile(gridPosition))
        {

        }
    }

    void MoveCamera()
    {
        float speed = 3f;
        Vector2 mousePosition = mouseInput.Mouse.MousePosition.ReadValue<Vector2>();
        mainCamera.transform.position = new Vector3(mousePosition.x * speed * Time.deltaTime, mousePosition.y * speed * Time.deltaTime, -4.5f);
    }

    // Update is called once per frame
    void Update()
    {
        //MoveCamera();
    }
}
