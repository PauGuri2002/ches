using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
    [HideInInspector] public Vector2Int boardPosition;
    [HideInInspector] public Board board;
    [HideInInspector] public RectTransform rectTransform;
    public GameObject cellFrame;
    private Sprite[] cellFrameSprites;
    [HideInInspector] public PieceGlobal currentPiece = null;

    public void Setup(Vector2Int newBoardPosition, Board newBoard){
        boardPosition = newBoardPosition;
        board = newBoard;
        rectTransform = GetComponent<RectTransform>();
        cellFrameSprites = Resources.LoadAll<Sprite>("cellFrame");
    }

    public void OnHoverEnter()
    {
        cellFrame.GetComponent<Image>().sprite = cellFrameSprites[0];
    }
    public void OnHoverExit()
    {
        cellFrame.GetComponent<Image>().sprite = cellFrameSprites[1];
    }
}
