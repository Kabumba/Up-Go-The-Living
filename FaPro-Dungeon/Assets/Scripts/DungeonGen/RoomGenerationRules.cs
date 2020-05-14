using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RoomGenerationRule
{
    public LayoutGenerator Lg { set; get; }

    public int weight;

    public abstract void Apply(RoomNode rn);

    public abstract bool IsApplicable(RoomNode rn);

    public static readonly Dictionary<Direction, Vector2Int> directionMovementMap = new Dictionary<Direction, Vector2Int>
    {
        {Direction.up, Vector2Int.up },
        {Direction.left, Vector2Int.left },
        {Direction.down, Vector2Int.down },
        {Direction.right, Vector2Int.right }
    };

    public abstract override string ToString();
}


class AddNonSpecialRoom : RoomGenerationRule
{
    public RoomType addTo;

    public RoomType toAdd;

    public Direction dir;


    public override void Apply(RoomNode rn)
    {
        rn.Create(toAdd, dir);
    }

    public override bool IsApplicable(RoomNode rn)
    {
        if(Lg.roomList.Count < Lg.maxNumberOfRooms && Lg.numberOfNonSpecialRooms < Lg.maxNumberOfNonSpecialRooms && rn.Type == addTo && rn.Get(dir) == null && !Lg.rooms.ContainsKey(rn.Position + directionMovementMap[dir]))
        {
            return true;
        }
        return false;
    }

    public override string ToString()
    {
        return "addTo: " + addTo + ", toAdd: " + toAdd + ", dir: " + dir;
    }
}

class AddLootRoom : RoomGenerationRule
{
    public RoomType addTo;

    public Direction dir;

    public override void Apply(RoomNode rn)
    {
        rn.Create(RoomType.Loot, dir);
    }

    public override bool IsApplicable(RoomNode rn)
    {
        if (Lg.roomList.Count < Lg.maxNumberOfRooms && Lg.numberOfLootRooms<Lg.maxNumberOfLootRooms && rn.Type == addTo && rn.Get(dir) == null && !Lg.rooms.ContainsKey(rn.Position + directionMovementMap[dir]))
        {
            return true;
        }
        return false;
    }

    public override string ToString()
    {
        return "addTo: " + addTo + ", toAdd: loot" + ", dir: " + dir;
    }
}

class AddBossRoom : RoomGenerationRule
{
    public RoomType addTo;

    public Direction dir;

    public override void Apply(RoomNode rn)
    {
        rn.Create(RoomType.Boss, dir);
    }

    public override bool IsApplicable(RoomNode rn)
    {
        if (Lg.roomList.Count < Lg.maxNumberOfRooms && Lg.numberOfBossRooms < Lg.maxNumberOfBossRooms && rn.Type == addTo && rn.Get(dir) == null && !Lg.rooms.ContainsKey(rn.Position + directionMovementMap[dir]))
        {
            return true;
        }
        return false;
    }

    public override string ToString()
    {
        return "addTo: " + addTo + ", toAdd: boss" + ", dir: " + dir;
    }
}

class Rule1 : AddNonSpecialRoom
{
    public Rule1()
    {
        addTo = RoomType.Start;
        toAdd = RoomType.Enemy;
        dir = Direction.up;
    }
}
class Rule2 : AddNonSpecialRoom
{
    public Rule2()
    {
        addTo = RoomType.Start;
        toAdd = RoomType.Enemy;
        dir = Direction.down;
    }
}
class Rule3 : AddNonSpecialRoom
{
    public Rule3()
    {
        addTo = RoomType.Start;
        toAdd = RoomType.Enemy;
        dir = Direction.left;
    }
}
class Rule4 : AddNonSpecialRoom
{
    public Rule4()
    {
        addTo = RoomType.Start;
        toAdd = RoomType.Enemy;
        dir = Direction.right;
    }
}
class Rule5 : AddNonSpecialRoom
{
    public Rule5()
    {
        addTo = RoomType.Enemy;
        toAdd = RoomType.Enemy;
        dir = Direction.up;
    }
}
class Rule6 : AddNonSpecialRoom
{
    public Rule6()
    {
        addTo = RoomType.Enemy;
        toAdd = RoomType.Enemy;
        dir = Direction.down;
    }
}
class Rule7 : AddNonSpecialRoom
{
    public Rule7()
    {
        addTo = RoomType.Enemy;
        toAdd = RoomType.Enemy;
        dir = Direction.left;
    }
}
class Rule8 : AddNonSpecialRoom
{
    public Rule8()
    {
        addTo = RoomType.Enemy;
        toAdd = RoomType.Enemy;
        dir = Direction.right;
    }
}
class Rule9 : AddLootRoom
{
    public Rule9()
    {
        addTo = RoomType.Start;
        dir = Direction.up;
    }
}
class Rule10 : AddLootRoom
{
    public Rule10()
    {
        addTo = RoomType.Start;
        dir = Direction.down;
    }
}
class Rule11 : AddLootRoom
{
    public Rule11()
    {
        addTo = RoomType.Start;
        dir = Direction.left;
    }
}
class Rule12 : AddLootRoom
{
    public Rule12()
    {
        addTo = RoomType.Start;
        dir = Direction.right;
    }
}
class Rule13 : AddLootRoom
{
    public Rule13()
    {
        addTo = RoomType.Enemy;
        dir = Direction.up;
    }
}
class Rule14 : AddLootRoom
{
    public Rule14()
    {
        addTo = RoomType.Enemy;
        dir = Direction.down;
    }
}
class Rule15 : AddLootRoom
{
    public Rule15()
    {
        addTo = RoomType.Enemy;
        dir = Direction.left;
    }
}
class Rule16 : AddLootRoom
{
    public Rule16()
    {
        addTo = RoomType.Enemy;
        dir = Direction.right;
    }
}
class Rule17 : AddBossRoom
{
    public Rule17()
    {
        addTo = RoomType.Enemy;
        dir = Direction.up;
    }
}
class Rule18 : AddBossRoom
{
    public Rule18()
    {
        addTo = RoomType.Enemy;
        dir = Direction.down;
    }
}
class Rule19 : AddBossRoom
{
    public Rule19()
    {
        addTo = RoomType.Enemy;
        dir = Direction.left;
    }
}
class Rule20 : AddBossRoom
{
    public Rule20()
    {
        addTo = RoomType.Enemy;
        dir = Direction.right;
    }
}

