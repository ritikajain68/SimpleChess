using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSelected : MonoBehaviour
{
    public GameObject highlightedBlockPrefab;

    private GameObject tileHighlight;

    void Start ()
    {
        Vector2Int gridPoint = ChessBoard.BlockPoint(0, 0);
        Vector3 point = ChessBoard.PointFromBlock(gridPoint);
        tileHighlight = Instantiate(highlightedBlockPrefab, point, Quaternion.identity, gameObject.transform);
        tileHighlight.SetActive(false);
    }

    void Update ()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Vector3 point = hit.point;
            Vector2Int pointed = ChessBoard.BlockFromPoint(point);

            tileHighlight.SetActive(true);
            tileHighlight.transform.position = ChessBoard.PointFromBlock(pointed);
            if (Input.GetMouseButtonDown(0))
            {
                GameObject selectedPiece = GameController.instance.CoinAtPoint(pointed);
                if (GameController.instance.IsCurrentPlayerCoin(selectedPiece))
                {
                    GameController.instance.CoinSelect(selectedPiece);
                    // Reference Point 1: add ExitState call here later
                    DisableState(selectedPiece);
                }
            }
        }
        else
        {
            tileHighlight.SetActive(false);
        }
    }

    public void EnableState()
    {
        enabled = true;
    }

    private void DisableState(GameObject movingPiece)
    {
        this.enabled = false;
        tileHighlight.SetActive(false);
        MoveSelected move = GetComponent<MoveSelected>();
        move.EnterState(movingPiece);
    }
}
