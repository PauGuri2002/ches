using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    public Board board;
    public PieceManager pieceManager;

    void Start(){
        board.Create();
        pieceManager.Setup(board);
        pieceManager.SwitchTurn(true);
    }
}
