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

class AddSingleRoom : RoomGenerationRule
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
        return CanAddSingleRoom(rn);
    }

    public bool CanAddSingleRoom(RoomNode rn)
    {
        return Lg.roomList.Count < Lg.maxNumberOfRooms && rn.DoorCount() < RoomNode.MaxDoors(rn.Type) && rn.Type == addTo && rn.Get(dir) == null && !Lg.rooms.ContainsKey(rn.Position + directionMovementMap[dir]);
    }

    public override string ToString()
    {
        return "addTo: " + addTo + ", toAdd: " + toAdd + ", dir: " + dir;
    }

}

class MoveSingleRoom : AddSingleRoom
{
    public RoomType replaceWith;

    public override void Apply(RoomNode rn)
    {
        rn.Create(toAdd, dir);
        switch (toAdd)
        {
            case RoomType.Loot:
                Lg.numberOfLootRooms--;
                break;
            case RoomType.Boss:
                Lg.numberOfBossRooms--;
                break;
            default:
                Lg.numberOfNonSpecialRooms--;
                break;
        }
        rn.Type = replaceWith;
        switch (replaceWith)
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
    }

    public override bool IsApplicable(RoomNode rn)
    {
        return CanMoveSingleRoom(rn);
    }

    public bool CanMoveSingleRoom(RoomNode rn)
    {
        return Lg.roomList.Count < Lg.maxNumberOfRooms && rn.Type == addTo && rn.Get(dir) == null && !Lg.rooms.ContainsKey(rn.Position + directionMovementMap[dir]) && Lg.numberOfNonSpecialRooms < Lg.maxNumberOfNonSpecialRooms && replaceWith != RoomType.Start && rn.DoorCount() < RoomNode.MaxDoors(replaceWith);
    }

    public override string ToString()
    {
        return "addTo: " + addTo + ", replaceWith: " + replaceWith + ", dir: " + dir;
    }

    public MoveSingleRoom(RoomType aT, RoomType rW, Direction d)
    {
        addTo = aT;
        replaceWith = rW;
        dir = d;
    }
}

class AddNonSpecialRoom : AddSingleRoom
{
    public override bool IsApplicable(RoomNode rn)
    {
        return CanAddSingleRoom(rn) && Lg.numberOfNonSpecialRooms < Lg.maxNumberOfNonSpecialRooms && toAdd != RoomType.Boss && toAdd != RoomType.Loot;
    }

    public AddNonSpecialRoom(RoomType aT, RoomType tA, Direction d)
    {
        addTo = aT;
        toAdd = tA;
        dir = d;
    }
}

class AddLootRoom : AddSingleRoom
{
    public override void Apply(RoomNode rn)
    {
        rn.Create(RoomType.Loot, dir);
    }

    public override bool IsApplicable(RoomNode rn)
    {
        return CanAddSingleRoom(rn) && Lg.numberOfLootRooms < Lg.maxNumberOfLootRooms;
    }
    public override string ToString()
    {
        return "addTo: " + addTo + ", toAdd: Loot"+ ", dir: " + dir;
    }
    public AddLootRoom(RoomType aT, Direction d)
    {
        addTo = aT;
        toAdd = RoomType.Loot;
        dir = d;
    }
}

class AddBossRoom : AddSingleRoom
{
    public override void Apply(RoomNode rn)
    {
        rn.Create(RoomType.Boss, dir);
    }

    public override bool IsApplicable(RoomNode rn)
    {
        return CanAddSingleRoom(rn) && Lg.numberOfBossRooms < Lg.maxNumberOfBossRooms;
    }
    public override string ToString()
    {
        return "addTo: " + addTo + ", toAdd: Boss" + ", dir: " + dir;
    }
    public AddBossRoom(RoomType aT, Direction d)
    {
        addTo = aT;
        toAdd = RoomType.Boss;
        dir = d;
    }
}