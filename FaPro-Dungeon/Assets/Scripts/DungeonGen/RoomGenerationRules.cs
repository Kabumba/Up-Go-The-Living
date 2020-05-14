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

//ADD ENEMY4 TO START
class Rule1 : AddNonSpecialRoom
{
    public Rule1()
    {
        addTo = RoomType.Start;
        toAdd = RoomType.Enemy4;
        dir = Direction.up;
    }
}
class Rule2 : AddNonSpecialRoom
{
    public Rule2()
    {
        addTo = RoomType.Start;
        toAdd = RoomType.Enemy4;
        dir = Direction.down;
    }
}
class Rule3 : AddNonSpecialRoom
{
    public Rule3()
    {
        addTo = RoomType.Start;
        toAdd = RoomType.Enemy4;
        dir = Direction.left;
    }
}
class Rule4 : AddNonSpecialRoom
{
    public Rule4()
    {
        addTo = RoomType.Start;
        toAdd = RoomType.Enemy4;
        dir = Direction.right;
    }
}

//ADD ENEMY4 TO ENEMY4
class Rule5 : AddNonSpecialRoom
{
    public Rule5()
    {
        addTo = RoomType.Enemy4;
        toAdd = RoomType.Enemy4;
        dir = Direction.up;
    }
}
class Rule6 : AddNonSpecialRoom
{
    public Rule6()
    {
        addTo = RoomType.Enemy4;
        toAdd = RoomType.Enemy4;
        dir = Direction.down;
    }
}
class Rule7 : AddNonSpecialRoom
{
    public Rule7()
    {
        addTo = RoomType.Enemy4;
        toAdd = RoomType.Enemy4;
        dir = Direction.left;
    }
}
class Rule8 : AddNonSpecialRoom
{
    public Rule8()
    {
        addTo = RoomType.Enemy4;
        toAdd = RoomType.Enemy4;
        dir = Direction.right;
    }
}

//ADD LOOT TO START
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

//ADD LOOT TO ENEMY4
class Rule13 : AddLootRoom
{
    public Rule13()
    {
        addTo = RoomType.Enemy4;
        dir = Direction.up;
    }
}
class Rule14 : AddLootRoom
{
    public Rule14()
    {
        addTo = RoomType.Enemy4;
        dir = Direction.down;
    }
}
class Rule15 : AddLootRoom
{
    public Rule15()
    {
        addTo = RoomType.Enemy4;
        dir = Direction.left;
    }
}
class Rule16 : AddLootRoom
{
    public Rule16()
    {
        addTo = RoomType.Enemy4;
        dir = Direction.right;
    }
}

//ADD BOSS TO ENEMY4
class Rule17 : AddBossRoom
{
    public Rule17()
    {
        addTo = RoomType.Enemy4;
        dir = Direction.up;
    }
}
class Rule18 : AddBossRoom
{
    public Rule18()
    {
        addTo = RoomType.Enemy4;
        dir = Direction.down;
    }
}
class Rule19 : AddBossRoom
{
    public Rule19()
    {
        addTo = RoomType.Enemy4;
        dir = Direction.left;
    }
}
class Rule20 : AddBossRoom
{
    public Rule20()
    {
        addTo = RoomType.Enemy4;
        dir = Direction.right;
    }
}

//ADD ENEMY3 TO START
class Rule21 : AddNonSpecialRoom
{
    public Rule21()
    {
        addTo = RoomType.Start;
        toAdd = RoomType.Enemy3L;
        dir = Direction.up;
    }
}
class Rule22 : AddNonSpecialRoom
{
    public Rule22()
    {
        addTo = RoomType.Start;
        toAdd = RoomType.Enemy3D;
        dir = Direction.up;
    }
}
class Rule23 : AddNonSpecialRoom
{
    public Rule23()
    {
        addTo = RoomType.Start;
        toAdd = RoomType.Enemy3R;
        dir = Direction.up;
    }
}
class Rule24 : AddNonSpecialRoom
{
    public Rule24()
    {
        addTo = RoomType.Start;
        toAdd = RoomType.Enemy3L;
        dir = Direction.down;
    }
}
class Rule25 : AddNonSpecialRoom
{
    public Rule25()
    {
        addTo = RoomType.Start;
        toAdd = RoomType.Enemy3U;
        dir = Direction.down;
    }
}
class Rule26 : AddNonSpecialRoom
{
    public Rule26()
    {
        addTo = RoomType.Start;
        toAdd = RoomType.Enemy3R;
        dir = Direction.down;
    }
}
class Rule27 : AddNonSpecialRoom
{
    public Rule27()
    {
        addTo = RoomType.Start;
        toAdd = RoomType.Enemy3U;
        dir = Direction.left;
    }
}
class Rule28 : AddNonSpecialRoom
{
    public Rule28()
    {
        addTo = RoomType.Start;
        toAdd = RoomType.Enemy3R;
        dir = Direction.left;
    }
}
class Rule29 : AddNonSpecialRoom
{
    public Rule29()
    {
        addTo = RoomType.Start;
        toAdd = RoomType.Enemy3D;
        dir = Direction.left;
    }
}
class Rule30 : AddNonSpecialRoom
{
    public Rule30()
    {
        addTo = RoomType.Start;
        toAdd = RoomType.Enemy3U;
        dir = Direction.right;
    }
}
class Rule31 : AddNonSpecialRoom
{
    public Rule31()
    {
        addTo = RoomType.Start;
        toAdd = RoomType.Enemy3L;
        dir = Direction.right;
    }
}
class Rule32 : AddNonSpecialRoom
{
    public Rule32()
    {
        addTo = RoomType.Start;
        toAdd = RoomType.Enemy3D;
        dir = Direction.right;
    }
}

/*
//ADD ENEMY2 TO START
class Rule : AddNonSpecialRoom
{
    public Rule()
    {
        addTo = RoomType.Start;
        toAdd = RoomType.Enemy2DR;
        dir = Direction.left;
    }
}
class Rule : AddNonSpecialRoom
{
    public Rule()
    {
        addTo = RoomType.Start;
        toAdd = RoomType.Enemy2DR;
        dir = Direction.up;
    }
}
class Rule : AddNonSpecialRoom
{
    public Rule()
    {
        addTo = RoomType.Start;
        toAdd = RoomType.Enemy2LD;
        dir = Direction.right;
    }
}
class Rule : AddNonSpecialRoom
{
    public Rule()
    {
        addTo = RoomType.Start;
        toAdd = RoomType.Enemy2LD;
        dir = Direction.up;
    }
}
class Rule : AddNonSpecialRoom
{
    public Rule()
    {
        addTo = RoomType.Start;
        toAdd = RoomType.Enemy2LR;
        dir = Direction.left;
    }
}
class Rule : AddNonSpecialRoom
{
    public Rule()
    {
        addTo = RoomType.Start;
        toAdd = RoomType.Enemy2LR;
        dir = Direction.right;
    }
}
class Rule : AddNonSpecialRoom
{
    public Rule()
    {
        addTo = RoomType.Start;
        toAdd = RoomType.Enemy2UD;
        dir = Direction.up;
    }
}
class Rule : AddNonSpecialRoom
{
    public Rule()
    {
        addTo = RoomType.Start;
        toAdd = RoomType.Enemy2UD;
        dir = Direction.down;
    }
}
class Rule : AddNonSpecialRoom
{
    public Rule()
    {
        addTo = RoomType.Start;
        toAdd = RoomType.Enemy2UL;
        dir = Direction.right;
    }
}
class Rule : AddNonSpecialRoom
{
    public Rule()
    {
        addTo = RoomType.Start;
        toAdd = RoomType.Enemy2UL;
        dir = Direction.down;
    }
}
class Rule : AddNonSpecialRoom
{
    public Rule()
    {
        addTo = RoomType.Start;
        toAdd = RoomType.Enemy2UR;
        dir = Direction.down;
    }
}
class Rule : AddNonSpecialRoom
{
    public Rule()
    {
        addTo = RoomType.Start;
        toAdd = RoomType.Enemy2UR;
        dir = Direction.left;
    }
}

//ADD ENEMY1 TO START
class Rule : AddNonSpecialRoom
{
    public Rule()
    {
        addTo = RoomType.Start;
        toAdd = RoomType.Enemy1D;
        dir = Direction.up;
    }
}
class Rule : AddNonSpecialRoom
{
    public Rule()
    {
        addTo = RoomType.Start;
        toAdd = RoomType.Enemy1L;
        dir = Direction.right;
    }
}
class Rule : AddNonSpecialRoom
{
    public Rule()
    {
        addTo = RoomType.Start;
        toAdd = RoomType.Enemy1R;
        dir = Direction.left;
    }
}
class Rule : AddNonSpecialRoom
{
    public Rule()
    {
        addTo = RoomType.Start;
        toAdd = RoomType.Enemy1U;
        dir = Direction.down;
    }
}

*/


