using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 public enum CoinType {King, Queen, Bishop, Knight, Rook, Pawn};

public abstract class Coin : MonoBehaviour
{
    public CoinType type;
    protected Vector2Int[] RookInstruction = {new Vector2Int(0,1), new Vector2Int(1, 0),
        new Vector2Int(0, -1), new Vector2Int(-1, 0)};
    protected Vector2Int[] BishopInstrcution = {new Vector2Int(1,1), new Vector2Int(1, -1),
        new Vector2Int(-1, -1), new Vector2Int(-1, 1)};

    public abstract List<Vector2Int> MoveLocations(Vector2Int point);

}
