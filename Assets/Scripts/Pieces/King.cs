using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class King : PieceGlobal
{

    public override void Setup(bool newWhiteTeam, PieceManager newPieceManager)
    {
        base.Setup(newWhiteTeam, newPieceManager);

        canBeChecked = true;
        Sprite[] sprites = Resources.LoadAll<Sprite>("pieces");
        GetComponent<Image>().sprite = sprites.Single(s => s.name == ((whiteTeam ? "w_" : "b_") + "king"));
    }

    public override void CalculatePath()
    {
        AddHighlightedCells(-1, 0, 1);
        AddHighlightedCells(1, 0, 1);
        AddHighlightedCells(0, -1, 1);
        AddHighlightedCells(0, 1, 1);
        AddHighlightedCells(-1, -1, 1);
        AddHighlightedCells(1, 1, 1);
        AddHighlightedCells(-1, 1, 1);
        AddHighlightedCells(1, -1, 1);
    }
}
