using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGame : MonoBehaviour
{
    private Map Map;
    private bool PlayerTurn = true;

    [SerializeField] int Width = 5;
    [SerializeField] int Height = 5;
    [SerializeField] Vector2 CellSize;
    [SerializeField] Vector3 MapOrigin;
    [SerializeField] Cursor Cursor;
    [SerializeField] Unit[] TeamPlayer;
    [SerializeField] Unit[] TeamEnemy;

    // Start is called before the first frame update
    void Awake()
    {
        PlayerTurn = true;
        Map = new Map(Width, Height, CellSize, MapOrigin);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(PlayerTurn)
                TeamPlayer[0].SetWorldPosition(Cursor.Position, CellSize);
            else
                TeamEnemy[0].SetWorldPosition(Cursor.Position, CellSize);

            ChangeTurns();
        }
    }
    public int GetMapWidth() { return Width; }
    public int GetMapHeight() { return Height; }
    public Vector2 GetCellSize() { return CellSize; }

    private void ChangeTurns()
    {
        PlayerTurn = !PlayerTurn;
    }
}
