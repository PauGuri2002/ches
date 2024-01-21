using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Horse : PieceGlobal
{
    public override void Setup(bool newWhiteTeam, PieceManager newPieceManager)
    {
        base.Setup(newWhiteTeam, newPieceManager);

        Sprite[] sprites = Resources.LoadAll<Sprite>("pieces");
        GetComponent<Image>().sprite = sprites.Single(s => s.name == ((whiteTeam ? "w_" : "b_") + "horse"));
    }

    public override void CalculatePath()
    {
        AddHighlightedCells(1, 2, 1);
        AddHighlightedCells(-1, 2, 1);
        AddHighlightedCells(-2, 1, 1);
        AddHighlightedCells(-2, -1, 1);
        AddHighlightedCells(-1, -2, 1);
        AddHighlightedCells(1, -2, 1);
        AddHighlightedCells(2, -1, 1);
        AddHighlightedCells(2, 1, 1);
    }
}
