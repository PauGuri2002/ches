using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Pawn : PieceGlobal
{
    public override void Setup(bool newWhiteTeam, PieceManager newPieceManager)
    {
        base.Setup(newWhiteTeam, newPieceManager);

        Sprite[] sprites = Resources.LoadAll<Sprite>("pieces");
        GetComponent<Image>().sprite = sprites.Single(s => s.name == ((whiteTeam ? "w_" : "b_") + "pawn"));
    }

    public override void CalculatePath()
    {
        int distance = firstMove ? 2 : 1;
        int yDir = whiteTeam ? 1 : -1;

        for(int i = 1; i <= distance; i++)
        {
            if (currentCell.board.CheckCellState(currentCell.boardPosition.x, currentCell.boardPosition.y + i*yDir, this) == Board.CellState.Free)
            {
                AddHighlightedCells(0, yDir, distance);
            }
        }
        

        if(currentCell.board.CheckCellState(currentCell.boardPosition.x + 1, currentCell.boardPosition.y + yDir, this) == Board.CellState.Enemy)
        {
            AddHighlightedCells(1, yDir, 1);
        }
        if (currentCell.board.CheckCellState(currentCell.boardPosition.x - 1, currentCell.boardPosition.y + yDir, this) == Board.CellState.Enemy)
        {
            AddHighlightedCells(-1, yDir, 1);
        }
    }


}
