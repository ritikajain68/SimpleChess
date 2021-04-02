using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class GameData{
    public string application_name;
    public string boardSize;
    public int gameTimer;
    public List<ChessPieceType> chessPieceType = new List<ChessPieceType>();
}

[Serializable]
public class ChessPieceType{
    public int id;
    public string name;
    public string description;
}