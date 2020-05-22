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
    right = 1,
    down = 2,
    left = 3,
}

static class Directions
{
    public static readonly Dictionary<Direction, Vector2Int> dirToVector2 = new Dictionary<Direction, Vector2Int>
    {
        {Direction.up, Vector2Int.up },
        {Direction.left, Vector2Int.left },
        {Direction.down, Vector2Int.down },
        {Direction.right, Vector2Int.right }
    };

    public static readonly Dictionary<Direction, Direction> opposite = new Dictionary<Direction, Direction>
    {
        {Direction.up, Direction.down },
        {Direction.left, Direction.right },
        {Direction.down, Direction.up },
        {Direction.right, Direction.left }
    };
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

    public static int MaxDoors(RoomType t)
    {
        switch (t)
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
        foreach (Direction dir in Enum.GetValues(typeof(Direction)))
        {
            if (Get(dir) != null)
            {
                count++;
            }
        }
        return count;
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
        newR.Position = Position + Directions.dirToVector2[dir];
        Lg.rooms.Add(newR.Position, newR);
        Lg.roomList.Add(newR);
        newR.Lg = Lg;

        //Neuen Raum wenn möglich mit benachbarten Räumen verbinden
        int doorsleft = MaxDoors(newR.Type) - newR.DoorCount();
        if (doorsleft > 0)
        {
            List<Direction> doorlessWalls = newR.GetDoorlessWalls();
            for (int i = 0; i < doorsleft; i++)
            {
                int dlc = doorlessWalls.Count;
                for (int j = 0; j < dlc; j++)
                {
                    if (doorlessWalls.Count > 0)
                    {
                        Direction randomDirection = doorlessWalls[Mathf.RoundToInt(UnityEngine.Random.Range(-0.5f, doorlessWalls.Count - 0.5f))];
                        Vector2Int randomPostion = newR.Position + Directions.dirToVector2[randomDirection];
                        if (Lg.rooms.ContainsKey(randomPostion))
                        {
                            RoomNode connectTo = Lg.rooms[randomPostion];
                            if (connectTo.Get(Directions.opposite[randomDirection]) == null && MaxDoors(connectTo.Type) > connectTo.DoorCount())
                            {
                                newR.Set(randomDirection, connectTo);
                                connectTo.Set(Directions.opposite[randomDirection], newR);
                                break;
                            }
                        }
                        doorlessWalls.Remove(randomDirection);
                    }
                }
            }
        }

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
                Lg.numberOfNonSpecialRooms++;
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

    public void Set(Direction dir, RoomNode rn)
    {
        switch (dir)
        {
            case Direction.up:
                Up = rn;
                break;
            case Direction.down:
                Down = rn;
                break;
            case Direction.left:
                Left = rn;
                break;
            case Direction.right:
                Right = rn;
                break;
        }
    }
    public List<Direction> GetDoorlessWalls()
    {
        List<Direction> doorlessWalls = new List<Direction>();
        foreach (Direction dir in Enum.GetValues(typeof(Direction)))
        {
            if (Get(dir) == null)
            {
                doorlessWalls.Add(dir);
            }
        }
        return doorlessWalls;
    }

}


public class LayoutGenerator : MonoBehaviour
{


    public int numberOfBossRooms = 0;

    public int maxNumberOfBossRooms;

    public int numberOfLootRooms = 0;

    public int maxNumberOfLootRooms;

    public int numberOfNonSpecialRooms = 0;

    public int maxNumberOfNonSpecialRooms;

    public int maxNumberOfRooms;

    public Dictionary<Vector2Int, RoomNode> rooms;

    public List<RoomNode> roomList;

    private List<RoomGenerationRule>[][] rules;

    public DungeonGenerationData Dgd;

    public void Initialize()
    {
        maxNumberOfBossRooms = Dgd.numberOfBossRooms;
        maxNumberOfLootRooms = Dgd.numberOfLootRooms;
        maxNumberOfRooms = Mathf.RoundToInt(UnityEngine.Random.Range(Dgd.minNumberOfRooms - 0.5f, Dgd.maxNumberOfRooms + 0.5f));
        maxNumberOfNonSpecialRooms = MaxNumberOfNonSpecialRooms();
        InitializeRooms();
        InitializeRules();
        GenerateLayout();
        printLayout();
    }

    public void StartroomError()
    {
        int x = 0;
   
        foreach(RoomNode rn in roomList)
        {
            if (rn.Type == RoomType.Start)
            {
                x++;
            }
        }
        if (x > 1)
        {
            print("Achtung! Es gibt " + x + " Starträume!");
        }
    }

    public void printLayout()
    {
        print("printig Layout:");
        
        foreach (RoomNode rn in roomList)
        {
            print(rn.Position + " " + rn.Type);
            foreach (Direction dir in Enum.GetValues(typeof(Direction)))
            {
                if (rn.Get(dir) != null)
                {
                    print(dir);
                }
            }
        }
        
    }

    private int MaxNumberOfNonSpecialRooms()
    {
        return maxNumberOfRooms - maxNumberOfLootRooms - maxNumberOfBossRooms;
    }

    private void InitializeRooms()
    {
        rooms = new Dictionary<Vector2Int, RoomNode>();
        roomList = new List<RoomNode>();
        RoomNode start = new RoomNode(RoomType.Start, new Vector2Int(0, 0))
        {
            Lg = this
        };
        rooms.Add(new Vector2Int(0, 0), start);
        roomList.Add(start);
        StartroomError();
        numberOfNonSpecialRooms++;
    }

    public List<RoomNode> GetLayout()
    {
        return roomList;
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
        while (roomList.Count < maxNumberOfRooms && workableRooms.Count > 0)
        {
            RoomNode randomRoom = workableRooms[Mathf.RoundToInt(UnityEngine.Random.Range(-0.5f, workableRooms.Count - 0.5f))];
            applicableRules = new List<RoomGenerationRule>();
            List<Direction> doorlessWalls = randomRoom.GetDoorlessWalls();
            if (doorlessWalls.Count != 0)
            {
                Direction randomDirection = doorlessWalls[Mathf.RoundToInt(UnityEngine.Random.Range(-0.5f, doorlessWalls.Count - 0.5f))];
                List<RoomGenerationRule> narrowedRules = rules[(int)randomRoom.Type][(int)randomDirection];
                foreach (RoomGenerationRule rule in narrowedRules)
                {
                    if (rule.IsApplicable(randomRoom))
                    {
                        applicableRules.Add(rule);
                    }
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
                //print(randomRule.ToString());
                //print("numberOfnonspecialRooms: " + numberOfNonSpecialRooms);
                workableRooms = new List<RoomNode>();
                foreach (RoomNode rn in roomList)
                {
                    workableRooms.Add(rn);
                }
            }
        }
    }

    //Erstellt alle Ersetzungsregeln, nach denen Dungeonsräume angeordnet generiert werden können.
    private void InitializeRules()
    {
        //Rules Datenstruktor initialisieren
        int numberOfRoomTypes = 0;
        foreach (RoomType rT in Enum.GetValues(typeof(RoomType)))
        {
            numberOfRoomTypes++;
        }
        rules = new List<RoomGenerationRule>[numberOfRoomTypes][];
        for (int i = 0; i < numberOfRoomTypes; i++)
        {
            rules[i] = new List<RoomGenerationRule>[4];
            for (int j = 0; j < 4; j++)
            {
                rules[i][j] = new List<RoomGenerationRule>();
            }
        }

        //Regeln erstellen
        foreach (Direction dir in Enum.GetValues(typeof(Direction)))
        {
            foreach (RoomType addTo in Enum.GetValues(typeof(RoomType)))
            {
                //An Räumen mit nur einer Tür kann kein neuer Raum generiert werden 
                if (addTo != RoomType.Boss && addTo != RoomType.Loot && addTo != RoomType.Enemy1)
                {
                    //Der Bossraum kann nicht am Startraum generiert werden
                    if (addTo != RoomType.Start)
                    {
                        //Bossraumregel hinzufügen
                        rules[(int)addTo][(int)dir].Add(new AddBossRoom(addTo, dir));
                    }
                    //Lootraumregel hinzufügen
                    rules[(int)addTo][(int)dir].Add(new AddLootRoom(addTo, dir));
                    foreach (RoomType toAdd in Enum.GetValues(typeof(RoomType)))
                    {
                        //Starträume können nicht generiert werden, Boss und Loot haben Sonderregeln
                        if (toAdd != RoomType.Start && toAdd != RoomType.Boss && toAdd != RoomType.Loot)
                        {
                            //Gegnerraumregel hinzufügen
                            rules[(int)addTo][(int)dir].Add(new AddNonSpecialRoom(addTo, toAdd, dir));
                        }
                    }
                }
                //Verschieben von Räumen um Situationen aufzubrechen in denen kein weiterer Raum mehr generiert werden kann. 
                foreach (RoomType replaceWith in Enum.GetValues(typeof(RoomType)))
                {
                    if (RoomNode.MaxDoors(replaceWith) <= RoomNode.MaxDoors(addTo))
                    {
                        continue;
                    }
                    //Der Bossraum kann nur an das Ende von Gängen verschoben werden.
                    if (addTo == RoomType.Boss)
                    {
                        if (replaceWith == RoomType.Enemy2)
                        {
                            rules[(int)addTo][(int)dir].Add(new MoveSingleRoom(addTo, replaceWith, dir));
                        }
                    }
                    else
                    {
                        if(addTo != RoomType.Start)
                        {
                            rules[(int)addTo][(int)dir].Add(new MoveSingleRoom(addTo, replaceWith, dir));
                        }
                    }
                }
            }
        }

        //LayoutGenerator in den Regeln anmelden
        foreach (List<RoomGenerationRule>[] i in rules)
        {
            foreach (List<RoomGenerationRule> j in i)
            {
                foreach (RoomGenerationRule rule in j)
                {
                    rule.Lg = this;
                }
            }
        }
    }
}
