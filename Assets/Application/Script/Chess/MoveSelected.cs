using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSelected : MonoBehaviour
{
    public GameObject moveLocationPrefab;
    public GameObject tileHighlightPrefab;
    public GameObject attackLocationPrefab;

    private GameObject tileHighlight;
    private GameObject movingPiece;
    private List<Vector2Int> moveLocations;
    private List<GameObject> locationHighlights;

    void Start ()
    {
        this.enabled = false;
        tileHighlight = Instantiate(tileHighlightPrefab, ChessBoard.PointFromBlock(new Vector2Int(0, 0)),
            Quaternion.identity, gameObject.transform);
        tileHighlight.SetActive(false);
    }

    void Update ()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Vector3 point = hit.point;
            Vector2Int gridPoint = ChessBoard.BlockFromPoint(point);

            tileHighlight.SetActive(true);
            tileHighlight.transform.position = ChessBoard.PointFromBlock(gridPoint);
            if (Input.GetMouseButtonDown(0))
            {
                // Reference Point 2: check for valid move location
                if (!moveLocations.Contains(gridPoint))
                {
                    return;
                }

                if (GameController.instance.CoinAtPoint(gridPoint) == null)
                {
                    GameController.instance.MoveComponent(movingPiece, gridPoint);
                }
                else
                {
                    GameController.instance.CaptureCoinAt(gridPoint);
                    GameController.instance.MoveComponent(movingPiece, gridPoint);
                }
                // Reference Point 3: capture enemy piece here later
                ExitState();
            }
        }
        else
        {
            tileHighlight.SetActive(false);
        }
    }

    private void CancelMove()
    {
        this.enabled = false;

        foreach (GameObject highlight in locationHighlights)
        {
            Destroy(highlight);
        }

        GameController.instance.CoinDeSelect(movingPiece);
        BlockSelected selector = GetComponent<BlockSelected>();
        selector.EnableState();
    }

    public void EnterState(GameObject piece)
    {
        movingPiece = piece;
        this.enabled = true;

        moveLocations = GameController.instance.MovesForCoin(movingPiece);
        locationHighlights = new List<GameObject>();

        if (moveLocations.Count == 0)
        {
            CancelMove();
        }

        foreach (Vector2Int loc in moveLocations)
        {
            GameObject highlight;
            if (GameController.instance.CoinAtPoint(loc))
            {
                highlight = Instantiate(attackLocationPrefab, ChessBoard.PointFromBlock(loc), Quaternion.identity, gameObject.transform);
            }
            else
            {
                highlight = Instantiate(moveLocationPrefab, ChessBoard.PointFromBlock(loc), Quaternion.identity, gameObject.transform);
            }
            locationHighlights.Add(highlight);
        }
    }

    private void ExitState()
    {
        this.enabled = false;
        BlockSelected selector = GetComponent<BlockSelected>();
        tileHighlight.SetActive(false);
        GameController.instance.CoinDeSelect(movingPiece);
        movingPiece = null;
        GameController.instance.NextPlayer();
        selector.EnableState();
        foreach (GameObject highlight in locationHighlights)
        {
            Destroy(highlight);
        }
    }

}
