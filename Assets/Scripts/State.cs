using System;
using UnityEngine;

[Serializable]

public class State
{
    public CellType[] grid;
    public int gridWidth;
    public Vector2Int player;
    public Vector2Int enemy;
    public Vector2Int goal;
    public bool playerTurn;

    public CellType GetCell(Vector2Int index)
    {
        return grid[index.x + index.y * gridWidth];
    }

}
