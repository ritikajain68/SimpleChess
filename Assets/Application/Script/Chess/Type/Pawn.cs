using System.Collections.Generic;
using UnityEngine;

public class Pawn : Coin
{
    public override List<Vector2Int> MoveLocations(Vector2Int gridPoint)
    {
        List<Vector2Int> locations = new List<Vector2Int>();

        int forwardDirection = GameController.instance.Player1.v2;
        Vector2Int forwardOne = new Vector2Int(gridPoint.x, gridPoint.y + forwardDirection);
        if (GameController.instance.CoinAtPoint(forwardOne) == false)
        {
            locations.Add(forwardOne);
        }

        Vector2Int forwardTwo = new Vector2Int(gridPoint.x, gridPoint.y + 2 * forwardDirection);
        if (GameController.instance.IsPawnMoved(gameObject) == false 
        && GameController.instance.CoinAtPoint(forwardTwo) == false)
        {
            locations.Add(forwardTwo);
        }

        Vector2Int forwardRight = new Vector2Int(gridPoint.x + 1, gridPoint.y + forwardDirection);
        if (GameController.instance.CoinAtPoint(forwardRight))
        {
            locations.Add(forwardRight);
        }

        Vector2Int forwardLeft = new Vector2Int(gridPoint.x - 1, gridPoint.y + forwardDirection);
        if (GameController.instance.CoinAtPoint(forwardLeft))
        {
            locations.Add(forwardLeft);
        }

        return locations;
    }
}
