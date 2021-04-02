using System.Collections.Generic;
using UnityEngine;

public class Rook : Coin
{
    public override List<Vector2Int> MoveLocations(Vector2Int gridPoint)
    {
        List<Vector2Int> locations = new List<Vector2Int>();

        foreach (Vector2Int dir in RookInstruction)
        {
            for (int i = 1; i < 8; i++)
            {
                Vector2Int nextGridPoint = new Vector2Int(gridPoint.x + i * dir.x, gridPoint.y + i * dir.y);
                locations.Add(nextGridPoint);
                if (GameController.instance.CoinAtPoint(nextGridPoint))
                {
                    break;
                }
            }
        }

        return locations;
    }
}
