using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerManager : MonoBehaviour
{
    private MouseInput mouseInput;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Tilemap map;
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
        //Vector3 mousePosition = new Vector3(mouseInput.Mouse.MousePosition.ReadValue<Vector2>().x, mouseInput.Mouse.MousePosition.ReadValue<Vector2>().y, -4.5f);
        mousePosition = mainCamera.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, 4.5f));
        Vector3Int gridPosition = grid.WorldToCell(mousePosition);

        //Vector2 mousePosition = mainCamera.ScreenToWorldPoint(mouseInput.Mouse.MousePosition.ReadValue<Vector2>());
        //Vector3Int gridPosition = map.WorldToCell(mousePosition);
        
        if (map.HasTile(gridPosition))
        {
            //Put tile selection code here.
            print($"Get Tile: {map.GetTile(gridPosition)}");
            print($"Mouse pos: {mousePosition}");
            print($"Grid Pos: {gridPosition}");

            map.SetTile(gridPosition, dirtTile);
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
