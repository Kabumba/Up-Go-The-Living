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

public enum Direction
{
    up,
    down,
    left,
    right
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

    public LayoutGenerator()
    {
        Dgd = new DungeonGenerationData();
        maxNumberOfBossRooms = Dgd.numberOfBossRooms;
        maxNumberOfLootRooms = Dgd.numberOfLootRooms;
        maxNumberOfRooms = Mathf.RoundToInt(Random.Range(Dgd.minNumberOfRooms - 0.5f, Dgd.maxNumberOfRooms + 0.5f));
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
            RoomNode randomRoom = workableRooms[Mathf.RoundToInt(Random.Range(-0.5f, workableRooms.Count - 0.5f))];
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
                randomRule = applicableRules[Mathf.RoundToInt(Random.Range(-0.5f, applicableRules.Count - 0.5f))];
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
        rules.Add(new Rule1());
        rules.Add(new Rule2());
        rules.Add(new Rule3());
        rules.Add(new Rule4());
        rules.Add(new Rule5());
        rules.Add(new Rule6());
        rules.Add(new Rule7());
        rules.Add(new Rule8());
        rules.Add(new Rule9());
        rules.Add(new Rule10());
        rules.Add(new Rule11());
        rules.Add(new Rule12());
        rules.Add(new Rule13());
        rules.Add(new Rule14());
        rules.Add(new Rule15());
        rules.Add(new Rule16());
        rules.Add(new Rule17());
        rules.Add(new Rule18());
        rules.Add(new Rule19());
        rules.Add(new Rule20());

        foreach(RoomGenerationRule rule in rules)
        {
            rule.Lg = this;
            //print(rule.ToString());
        }
    }

}
