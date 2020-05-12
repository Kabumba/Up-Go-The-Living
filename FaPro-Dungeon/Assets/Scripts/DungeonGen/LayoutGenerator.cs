using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum RoomType
{
    Start,
    Boss,
    Loot,
    Enemy,
}



public class RoomNode
{
    RoomNode Up { get; set; }
    RoomNode Down { get; set; }
    RoomNode Left { get; set; }
    RoomNode Right { get; set; }
    RoomType Type { get; set; }
    Vector2Int Position { get; set; }

    public RoomNode(RoomType t)
    {
        Type = t;
    }

    public RoomNode(RoomType t, Vector2Int pos)
    {
        Type = t;
        Position = pos;
    }

    //Erzeugt einen neuen Raum vom Typ t in Richtung dir vom ausrufenden Raum aus.
    public RoomNode Create(RoomType t, Direction dir)
    {
        RoomNode newR = new RoomNode(t);
        switch (dir)
        {
            case Direction.up:
                Up = newR;
                newR.Down = this;
                newR.Position = Position + Vector2Int.up;
                break;
            case Direction.down:
                Down = newR;
                newR.Up = this;
                newR.Position = Position + Vector2Int.down;
                break;
            case Direction.left:
                Left = newR;
                newR.Right = this;
                newR.Position = Position + Vector2Int.left;
                break;
            case Direction.right:
                Right = newR;
                newR.Left = this;
                newR.Position = Position + Vector2Int.right;
                break;
        }
        return newR;
    }

}

public class LayoutGenerator : MonoBehaviour
{
    public List<RoomNode> rooms;
    public List<Vector2> positionsVisited;

    void Start()
    {
        InitializeRooms();
        GenerateLayout();
    }

    public void InitializeRooms()
    {
        rooms = new List<RoomNode>
        {
            new RoomNode(RoomType.Start, new Vector2Int(0,0))
        };
    }

    public List<RoomNode> GetLayout()
    {
        return rooms;
    }

    public void GenerateLayout()
    {

    }

}
