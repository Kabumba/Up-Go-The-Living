using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum RoomType
{
    Start = 0,
    Boss = 1,
    Loot = 2,
    Enemy4 = 3,
    Enemy3 = 4,
    Enemy2 = 5,
    Enemy1 = 6
}

public enum Direction
{
    up = 0,
    right= 1,
    down = 2,
    left = 3,
}



public class RoomNode
{
    public RoomNode Up { get; set; }
    public RoomNode Down { get; set; }
    public RoomNode Left { get; set; }
    public RoomNode Right { get; set; }
    public RoomType Type { get; set; }
    public Vector2Int Position { get; set; }
    public LayoutGenerator Lg;

    public RoomNode(RoomType t)
    {
        Type = t;
    }

    public RoomNode(RoomType t, Vector2Int pos)
    {
        Type = t;
        Position = pos;
    }

    public int MaxDoors()
    {
        switch (Type)
        {
            case RoomType.Start:
                return 4;
            case RoomType.Boss:
                return 1;
            case RoomType.Loot:
                return 1;
            case RoomType.Enemy1:
                return 1;
            case RoomType.Enemy2:
                return 2;
            case RoomType.Enemy3:
                return 3;
            case RoomType.Enemy4:
                return 4;
        }
        return 4;
    }

    public int DoorCount()
    {
        int count = 0;
        foreach(Direction dir in Enum.GetValues(typeof(Direction)))
        {
            if (Get(dir) != null)
            {
                count++;
            }
        }
        return count;
    }

    public static readonly Dictionary<Direction, Vector2Int> directionMovementMap = new Dictionary<Direction, Vector2Int>
    {
        {Direction.up, Vector2Int.up },
        {Direction.left, Vector2Int.left },
        {Direction.down, Vector2Int.down },
        {Direction.right, Vector2Int.right }
    };

    //Erzeugt einen neuen Raum vom Typ t in Richtung dir vom ausrufenden Raum aus.
    public RoomNode Create(RoomType t, Direction dir)
    {
        RoomNode newR = new RoomNode(t);
        switch (dir)
        {
            case Direction.up:
                Up = newR;
                newR.Down = this;
                break;
            case Direction.down:
                Down = newR;
                newR.Up = this;
                break;
            case Direction.left:
                Left = newR;
                newR.Right = this;
                break;
            case Direction.right:
                Right = newR;
                newR.Left = this;
                break;
        }
        newR.Position = Position + directionMovementMap[dir];
        Lg.rooms.Add(newR.Position,newR);
        Lg.roomList.Add(newR);
        newR.Lg = Lg;

        //Raumanzahl updaten
        switch (t)
        {
            case RoomType.Loot:
                Lg.numberOfLootRooms++;
                break;
            case RoomType.Boss:
                Lg.numberOfBossRooms++;
                break;
            default:
                Lg.maxNumberOfNonSpecialRooms++;
                break;
        }
        return newR;
    }

    public RoomNode Get(Direction dir)
    {
        switch (dir)
        {
            case Direction.up:
                return Up;
            case Direction.down:
                return Down;
            case Direction.left:
                return Left;
            case Direction.right:
                return Right;
        }
        return null;
    }

}

public  class LayoutGenerator : MonoBehaviour
{
    public Dictionary<Vector2Int,RoomNode> rooms;
    public List<RoomNode> roomList;

    [HideInInspector]
    public int numberOfBossRooms = 0;

    [HideInInspector]
    public int maxNumberOfBossRooms;

    [HideInInspector]
    public int numberOfLootRooms = 0;

    [HideInInspector]
    public int maxNumberOfLootRooms;

    [HideInInspector]
    public int numberOfNonSpecialRooms = 0;

    [HideInInspector]
    public int maxNumberOfNonSpecialRooms;

    [HideInInspector]
    public int maxNumberOfRooms;

    private List<RoomGenerationRule> rules;

    private DungeonGenerationData Dgd;

    public void Awake()
    {
        Dgd = (DungeonGenerationData) ScriptableObject.CreateInstance("DungeonGenerationData"); ;
        maxNumberOfBossRooms = Dgd.numberOfBossRooms;
        maxNumberOfLootRooms = Dgd.numberOfLootRooms;
        maxNumberOfRooms = Mathf.RoundToInt(UnityEngine.Random.Range(Dgd.minNumberOfRooms - 0.5f, Dgd.maxNumberOfRooms + 0.5f));
        maxNumberOfNonSpecialRooms = MaxNumberOfNonSpecialRooms();
        InitializeRooms();
        InitializeRules();
        GenerateLayout();
    }

    private int MaxNumberOfNonSpecialRooms()
    {
        return maxNumberOfRooms - maxNumberOfLootRooms - maxNumberOfBossRooms;
    }

    private void InitializeRooms()
    {
        rooms = new Dictionary<Vector2Int, RoomNode>();
        roomList = new List<RoomNode>();
        RoomNode start = new RoomNode(RoomType.Start, new Vector2Int(0, 0));
        start.Lg = this;

        rooms.Add(new Vector2Int(0, 0), start);
        roomList.Add(start);
    }

    public Dictionary<Vector2Int, RoomNode> GetLayout()
    {
        return rooms;
    }

    /*
     * Hier werden die Regeln angewandt um neue Räume zu generieren.
     * Ein zufälliger existierender Raum wird ausgesucht. Es wird geprüft welche Regeln auf diesen Raum anwendbar sind.
     * Wenn es keine gibt wird ein anderer zufälliger Raum ausgesucht. Wenn es keine Räume mit anwendbaren Regeln gibt bricht es ab.
     * Wenn die zufällige Regel auf den zufälligen Raum anwendbar ist wird sie angewandt und das ganze wiederholt sich.
     */
    private void GenerateLayout()
    {
        List<RoomGenerationRule> applicableRules;
        List<RoomNode> workableRooms = new List<RoomNode>();
        RoomGenerationRule randomRule;
        foreach (RoomNode rn in roomList)
        {
            workableRooms.Add(rn);
        }
        while (workableRooms.Count > 0) {
            RoomNode randomRoom = workableRooms[Mathf.RoundToInt(UnityEngine.Random.Range(-0.5f, workableRooms.Count - 0.5f))];
            applicableRules = new List<RoomGenerationRule>();
            foreach (RoomGenerationRule rule in rules)
            {
                if (rule.IsApplicable(randomRoom))
                {
                    applicableRules.Add(rule);
                }
            }
            if (applicableRules.Count == 0)
            {
                workableRooms.Remove(randomRoom);
            }
            else
            {
                randomRule = applicableRules[Mathf.RoundToInt(UnityEngine.Random.Range(-0.5f, applicableRules.Count - 0.5f))];
                randomRule.Apply(randomRoom);
                workableRooms = new List<RoomNode>();
                foreach (RoomNode rn in roomList)
                {
                    workableRooms.Add(rn);
                }
            }
        }
    }
    
    private void InitializeRules()
    {
        rules = new List<RoomGenerationRule>();
        foreach (Direction dir in Enum.GetValues(typeof(Direction))) {
            foreach (RoomType addTo in Enum.GetValues(typeof(RoomType)))
            {
                //An Räumen mit nur einer Tür kann kein neuer Raum generiert werden 
                if (addTo != RoomType.Boss && addTo != RoomType.Loot && addTo != RoomType.Enemy1)
                {
                    //Der Bossraum kann nicht am Startraum generiert werden
                    if (addTo != RoomType.Start)
                    {
                        //Bossraumregel hinzufügen
                        rules.Add(new AddBossRoom(addTo, dir));
                    }
                    //Lootraumregel hinzufügen
                    rules.Add(new AddLootRoom(addTo, dir));
                    foreach (RoomType toAdd in Enum.GetValues(typeof(RoomType)))
                    {
                        //Starträume können nicht generiert werden, Boss und Loot haben Sonderregeln
                        if(toAdd != RoomType.Start && toAdd != RoomType.Boss && toAdd != RoomType.Loot)
                        {
                            //Gegnerraumregel hinzufügen
                            rules.Add(new AddNonSpecialRoom(addTo, toAdd, dir));
                        }
                    }
                }
            }
        }

        //LayoutGenerator in den Regeln anmelden
        foreach(RoomGenerationRule rule in rules)
        {
            rule.Lg = this;
        }
    }

}
