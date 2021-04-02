using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    // Initialise ChessBoard component
    public ChessBoard chessBoard;
    public GameObject gameOver;
    // Initialise Black coins
    public GameObject rookB;
    public GameObject knightB;
    public GameObject bishopB;
    public GameObject kingB;
    public GameObject queenB;
    public GameObject pawnB;    

    // Initialise White coins
    public GameObject rookW;
    public GameObject knightW;
    public GameObject bishopW;
    public GameObject kingW;
    public GameObject queenW;
    public GameObject pawnW;    

    private Movement BlackMovement;
    private Movement WhiteMovement;

    public Movement Player1;
    public Movement Player2;

    private GameObject[,] coins;
    private List<GameObject> movedP;
    
    void Awake(){
        instance = this;
    }
    public void Start(){
        // Number of coin of each colour is 8
        coins = new GameObject[8,8];
        movedP = new List<GameObject>();

        WhiteMovement = new Movement("WhiteMovement",true);

        BlackMovement = new Movement("BlackMovement",false);

        //Set Player 1
        Player1 = WhiteMovement;

        //Set Player 2
        Player2 = BlackMovement;
        
        SetPosition();
    }
    public void SetPosition(){

        // Set position of White coins
        SetCoin(rookW, WhiteMovement, 0,0);
        SetCoin(knightW, WhiteMovement, 1,0);
        SetCoin(bishopW, WhiteMovement, 2,0);
        SetCoin(queenW, WhiteMovement, 3,0);
        SetCoin(kingW, WhiteMovement, 4,0);
        SetCoin(bishopW, WhiteMovement, 5,0);
        SetCoin(knightW, WhiteMovement, 6,0);
        SetCoin(rookW, WhiteMovement, 7,0);


         // Set position of Black coins
        SetCoin(rookB, BlackMovement, 0,7);
        SetCoin(knightB, BlackMovement, 1,7);
        SetCoin(bishopB, BlackMovement, 2,7);
        SetCoin(queenB, BlackMovement, 3,7);
        SetCoin(kingB, BlackMovement, 4,7);
        SetCoin(bishopB, BlackMovement, 5,7);
        SetCoin(knightB, BlackMovement, 6,7);
        SetCoin(rookB, BlackMovement, 7,7);


        // Set the position of each pawns in white colour

        for(int pawnCount = 0; pawnCount <8 ; pawnCount++){
            SetCoin(pawnW, WhiteMovement, pawnCount,1);
        }
   
        // Set the position of each pawns in black colour

        for(int pawnCount2 = 0; pawnCount2 <8 ; pawnCount2++){
            SetCoin(pawnB, BlackMovement, pawnCount2,6);
        }

    }

    public void makeSelection(Vector2Int selectedPoint){
        // Set the grid for seleted Coin
        GameObject coinSelected = coins[selectedPoint.x, selectedPoint.y];

        if(coinSelected){
            // Pass selectes coin 
            chessBoard.CoinSelect(coinSelected);
        }

    }

    public void SetCoin(GameObject obj,Movement movt, int column, int row){
        GameObject coin = chessBoard.SetCoin(obj,column,row);
        movt.coins.Add(coin);
        coins[column,row] = coin;
    }

    public void MoveComponent(GameObject coin , Vector2Int point){
        // Get the Coin component 
        Coin coinComponent = coin.GetComponent<Coin>();
        
        // Check the component type and its movement
        if (coinComponent.type == CoinType.Pawn && !IsPawnMoved(coin))
        {
            // Add the coin to moved list
            movedP.Add(coin);
        }

        Vector2Int startGridPoint = GridForCoin(coin);
        coins[startGridPoint.x, startGridPoint.y] = null;
        coins[point.x, point.y] = coin;
        chessBoard.MoveCoin(coin, point);
    }

    public List<Vector2Int> MovesForCoin(GameObject obj)
    {
        Coin coinComponent = obj.GetComponent<Coin>();
        Vector2Int point = GridForCoin(obj);
        List<Vector2Int> locations = coinComponent.MoveLocations(point);

        // filter out offboard locations
        locations.RemoveAll(p => p.x < 0 || p.x > 7 || p.y < 0 || p.y > 7);

        // filter out locations
        locations.RemoveAll(p => CoinAt(p));

        return locations;
    }

    public Vector2Int GridForCoin(GameObject coin)
    {
        for (int i = 0; i < 8; i++) 
        {
            for (int j = 0; j < 8; j++)
            {
                if (coins[i, j] == coin)
                {
                    return new Vector2Int(i, j);
                }
            }
        }

        return new Vector2Int(-1, -1);
    }

    public void MovedPawn(GameObject pawn){
        movedP.Add(pawn);
    }

    public bool IsPawnMoved(GameObject pawn){
        return movedP.Contains(pawn);
    }

    public void CaptureCoinAt(Vector2Int point)
    {
        GameObject pieceToCapture = CoinAtPoint(point);
        if (pieceToCapture.GetComponent<Coin>().type == CoinType.King)
        {
            Debug.Log(Player1.name + " wins!");
            Destroy(chessBoard.GetComponent<BlockSelected>());
            Destroy(chessBoard.GetComponent<MoveSelected>());

            gameOver.SetActive(true);            

        }
        Player1.captured.Add(pieceToCapture);
        coins[point.x, point.y] = null;
        Destroy(pieceToCapture);
    }
    
    public void CoinSelect(GameObject coin){
        chessBoard.CoinSelect(coin);
    }
    public void CoinDeSelect(GameObject coin){
        chessBoard.CoinDeSelect(coin);
    }

    public bool IsCurrentPlayerCoin(GameObject coin){
        return Player1.coins.Contains(coin);
    }

    public GameObject CoinAtPoint(Vector2Int point){
        if (point.x > 7 || point.y > 7 || point.x < 0 || point.y < 0)
        {
            return null;
        }
        return coins[point.x, point.y];
    }

    public void NextPlayer(){
        // Swap Player 1 and 2 move
        Movement temp = Player1;
        Player1 = Player2;
        Player2 = temp;
    
    }

    public bool CoinAt(Vector2Int point){
        GameObject coin = CoinAtPoint(point);
        
        if(coin== null){
            return false;
        }
        if(Player2.coins.Contains(coin)){
            return false;
        }
        return true;
    }

}