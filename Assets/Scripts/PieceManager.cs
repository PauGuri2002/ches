using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PieceManager : MonoBehaviour
{
    public GameObject piecePrefab;

    [HideInInspector] public List<PieceGlobal> whitePieces;
    [HideInInspector] public List<PieceGlobal> blackPieces;

    private System.Type[] pieceLayout = new System.Type[16]
    {
        typeof(Pawn), typeof(Pawn), typeof(Pawn), typeof(Pawn), typeof(Pawn), typeof(Pawn), typeof(Pawn), typeof(Pawn),
        typeof(Rook), typeof(Horse), typeof(Bishop), typeof(King), typeof(Queen), typeof(Bishop), typeof(Horse), typeof(Rook)
    };

    public void Setup(Board board)
    {
        whitePieces = CreatePieces(true);
        blackPieces = CreatePieces(false);

        PlacePieces(true, whitePieces, board);
        PlacePieces(false, blackPieces, board);
    }

    List<PieceGlobal> CreatePieces(bool whiteTeam)
    {
        List<PieceGlobal> newPieces = new List<PieceGlobal>();

        for (int i = 0; i < pieceLayout.Length; i++)
        {
            GameObject newPieceObject = Instantiate(piecePrefab);
            newPieceObject.transform.SetParent(transform);

            PieceGlobal newPiece = (PieceGlobal)newPieceObject.AddComponent(pieceLayout[i]);
            newPiece.Setup(whiteTeam, this);

            newPieces.Add(newPiece);
        }

        return newPieces;
    }

    void PlacePieces(bool whiteTeam, List<PieceGlobal> pieces, Board board)
    {
        int pawnRow = whiteTeam ? 1 : 6;
        int mainRow = whiteTeam ? 0 : 7;

        for(int i = 0; i < 8; i++)
        {
            pieces[i].Place(board.allCells[i, pawnRow]);
            pieces[i+8].Place(board.allCells[i, mainRow]);
        }
    }

    public void SwitchTurn(bool whiteTeam)
    {
        SetInteractive(whitePieces, whiteTeam);
        SetInteractive(blackPieces, !whiteTeam);
    }
    void SetInteractive(List<PieceGlobal> pieces, bool value)
    {
        foreach (PieceGlobal piece in pieces)
        {
            piece.enabled = value;
        }
    }

    public void End(PieceGlobal deadPiece)
    {
        Debug.Log("GAME OVER. " + (deadPiece.whiteTeam ? "BLACK" : "WHITE") + " TEAM WON");
    }
}
