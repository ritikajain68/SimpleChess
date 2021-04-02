﻿using System.Collections.Generic;
using UnityEngine;

public class King : Coin
{
    public override List<Vector2Int> MoveLocations(Vector2Int gridPoint)
    {
        List<Vector2Int> locations = new List<Vector2Int>();
        List<Vector2Int> directions = new List<Vector2Int>(BishopInstrcution);
        directions.AddRange(RookInstruction);

        foreach (Vector2Int dir in directions)
        {
            Vector2Int nextGridPoint = new Vector2Int(gridPoint.x + dir.x, gridPoint.y + dir.y);
            locations.Add(nextGridPoint);
        }

        return locations;
    }
}
