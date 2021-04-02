using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessBoard : MonoBehaviour
{
    public Material defaultMaterial;
    public Material selectedMaterial;

    public GameObject SetCoin(GameObject coin, int column, int row)
    {
        Vector2Int point = BlockPoint(column, row);
        GameObject newPiece = Instantiate(coin, PointFromBlock(point), Quaternion.identity, gameObject.transform);
        return newPiece;
    }

    public void RemoveCoin(GameObject piece)
    {
        Destroy(piece);
    }

    public void MoveCoin(GameObject piece, Vector2Int point)
    {
        piece.transform.position = PointFromBlock(point);
    }
     // Select the Coin on board
    public void CoinSelect(GameObject coin)
    {
        MeshRenderer renderers = coin.GetComponentInChildren<MeshRenderer>();
        renderers.material = selectedMaterial;
    }

    // Deselect the selected Coin
    public void CoinDeSelect(GameObject coin)
    {
        MeshRenderer renderers = coin.GetComponentInChildren<MeshRenderer>();
        renderers.material = defaultMaterial;
    }

    // Height doesn't change so y = 0 , but x and z do change
    static public Vector3 PointFromBlock(Vector2Int point)
    {
        float x = -3.5f + 1.0f * point.x;
        float z = -3.5f + 1.0f * point.y;
        return new Vector3(x, 0, z);
    }

    // Return the row and column of the block
    static public Vector2Int BlockPoint(int column, int row)
    {
        return new Vector2Int(column, row);
    }

    static public Vector2Int BlockFromPoint(Vector3 point)
    {
        int row = Mathf.FloorToInt(4.0f + point.z);
        int column = Mathf.FloorToInt(4.0f + point.x);

        return new Vector2Int(column, row);
    }
}
