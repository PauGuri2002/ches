using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Board : MonoBehaviour
{
    public GameObject cellPrefab;

    public Cell[,] allCells = new Cell[8,8];

    public enum CellState
    {
        None,
        Friendly,
        Enemy,
        Free,
        OutOfBounds
    }

    public void Create(){
        for (int x = 0; x < 8; x++){
            for(int y = 0; y < 8; y++){
                GameObject newCell = Instantiate(cellPrefab,transform);
                allCells[x,y] = newCell.GetComponent<Cell>();
                newCell.GetComponent<RectTransform>().anchoredPosition = new Vector2((x*100)+50, (y*100)+50);

                allCells[x,y].Setup(new Vector2Int(x,y),this);

                if((y%2 == 0 && x%2 == 0) || (y%2 == 1 && x%2 == 1))
                {
                    allCells[x, y].GetComponent<Image>().color = new Color32(50, 50, 50, 255);
                }
            }
        }
    }

    public CellState CheckCellState(Cell cell, PieceGlobal checkingPiece){
        int x = cell.boardPosition.x;
        int y = cell.boardPosition.y;
        return CheckCellState(x, y, checkingPiece);
    }
    public CellState CheckCellState(int x, int y, PieceGlobal checkingPiece)
    {
        if (x < 0 || x > 7 || y < 0 || y > 7)
        {
            return CellState.OutOfBounds;
        }

        Cell cell = allCells[x, y];

        if (!cell.currentPiece)
        {
            return CellState.Free;
        }
        if (cell.currentPiece.whiteTeam == checkingPiece.whiteTeam)
        {
            return CellState.Friendly;
        }
        else
        {
            return CellState.Enemy;
        }
    }
}
