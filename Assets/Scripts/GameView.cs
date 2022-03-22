using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]

public class GameView : MonoBehaviour
{
    private List<BoardPiece> boardPieces = new List<BoardPiece>(); 
    
    public void UpdateView(State state)
    {
        foreach (var boardPieces in this.boardPieces)
        {
            Destroy(boardPieces);
        }
        boardPieces.Clear();

        for (int x = 0; x < state.gridWidth; x++)
        {
            for (int y = 0; y < state.gridHeight; y++)
            {
                
            }
        }
    }
}
