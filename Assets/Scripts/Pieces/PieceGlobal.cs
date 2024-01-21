using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PieceGlobal : EventTrigger
{
    [HideInInspector] public bool whiteTeam = true;
    [HideInInspector] public bool firstMove = true;
    [HideInInspector] public PieceGlobal pieceOnCheck = null;
    [HideInInspector] public bool canBeChecked = false;
    public PieceManager pieceManager;
    [HideInInspector] public RectTransform rectTransform;

    [HideInInspector] public Cell currentCell = null;
    [HideInInspector] public Cell targetCell = null;

    public virtual void Setup(bool newWhiteTeam, PieceManager newPieceManager)
    {
        whiteTeam = newWhiteTeam;
        pieceManager = newPieceManager;
        rectTransform = GetComponent<RectTransform>();
    }

    public void Place(Cell newCell)
    {
        rectTransform.localScale = new Vector3(1, 1, 1);
        transform.position = newCell.transform.position;
        currentCell = newCell;
        currentCell.currentPiece = this;
    }

    public List<Cell> highlightedCells = new List<Cell>();
    public virtual void AddHighlightedCells(int fromX, int fromY, int distance)
    {
        int startX = currentCell.boardPosition.x;
        int startY = currentCell.boardPosition.y;

        for(int i = 0; i < distance; i++)
        {
            int targetX = startX + fromX * (i+1);
            int targetY = startY + fromY * (i+1);
            Board.CellState cellState = currentCell.board.CheckCellState(targetX, targetY, this);

            if(cellState != Board.CellState.OutOfBounds && cellState != Board.CellState.Friendly)
            {
                Cell selectedCell = currentCell.board.allCells[targetX, targetY];
                
                highlightedCells.Add(selectedCell);
                if (cellState == Board.CellState.Enemy)
                {
                    selectedCell.cellFrame.GetComponent<Image>().color = new Color32(255, 0, 0, 255);

                    if(selectedCell.currentPiece.canBeChecked)
                    {
                        pieceOnCheck = selectedCell.currentPiece;
                    }

                    break;
                }
                
            } else
            {
                break;
            }
            
        }
    }

    void ShowHighlightedCells()
    {
        foreach(Cell cell in highlightedCells)
        {
            cell.cellFrame.SetActive(true);
        }
    }

    public void RemoveHighlightedCells()
    {
        foreach (Cell cell in highlightedCells)
        {
            cell.cellFrame.SetActive(false);
            cell.cellFrame.GetComponent<Image>().color = new Color32(0, 166, 39, 255);
        }
        highlightedCells.Clear();
    }

    public virtual void CalculatePath()
    {
        // piece path
    }

    public virtual void Die()
    {
        gameObject.SetActive(false);
        currentCell.currentPiece = null;
        currentCell = null;

        if (canBeChecked)
        {
            pieceManager.End(this);
        }
    }

    void CheckPiece()
    {
        Debug.Log(pieceOnCheck.GetType() + " on " + pieceOnCheck.currentCell.boardPosition.x + " " + pieceOnCheck.currentCell.boardPosition.y + " got checked by " + GetType());
        pieceOnCheck = null;
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);

        transform.SetAsLastSibling();
        CalculatePath();
        ShowHighlightedCells();
    }

    public override void OnDrag(PointerEventData eventData)
    {
        base.OnDrag(eventData);

        transform.position += (Vector3)eventData.delta;

        foreach(Cell cell in highlightedCells)
        {
            
            if (RectTransformUtility.RectangleContainsScreenPoint(cell.rectTransform, Input.mousePosition))
            {
                targetCell = cell;
                break;
            }
            targetCell = null;
        }
        foreach(Cell cell in highlightedCells)
        {
            if(cell == targetCell)
            {
                cell.OnHoverEnter();
            } else {
                cell.OnHoverExit();
            }
        }

        
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        base.OnEndDrag(eventData);

        if (!targetCell) //placing failed
        {
            transform.position = currentCell.transform.position;
        } else // placing successful
        {
            if(targetCell.board.CheckCellState(targetCell,this) == Board.CellState.Enemy)
            {
                targetCell.currentPiece.Die();
            }

            transform.position = targetCell.transform.position;
            currentCell.currentPiece = null;
            targetCell.currentPiece = this;
            currentCell = targetCell;
            targetCell = null;
            if (firstMove){ firstMove = false; }

            CalculatePath();
            if (pieceOnCheck)
            {
                CheckPiece();
            }
            
            pieceManager.SwitchTurn(!whiteTeam);
        }

        RemoveHighlightedCells();
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);

        RemoveHighlightedCells();
    }
}
