using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RoomGenerationRule
{
    public LayoutGenerator Lg { set; get; }

    public float weight = 1f;

    public RoomNode roomNode;

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
        roomNode = rn;
    }

    public override bool IsApplicable(RoomNode rn)
    {
        return CanAddSingleRoom(rn);
    }

    public bool CanAddSingleRoom(RoomNode rn)
    {
        return Lg.roomList.Count < Lg.maxNumberOfRooms && rn.DoorCount() <RoomNode.MaxDoors(rn.Type) && rn.Type == addTo && rn.Get(dir) == null && !Lg.rooms.ContainsKey(rn.Position + directionMovementMap[dir]) && toAdd!=RoomType.Start;
    }

    public override string ToString()
    {
        return "AddSingleRoom: addTo: " + addTo + " at " + roomNode.Position + ", toAdd: " + toAdd + ", dir: " + dir;
    }

}

class MoveSingleRoom : AddSingleRoom
{
    public RoomType replaceWith;

    public override void Apply(RoomNode rn)
    {
        rn.Create(addTo, dir);
        switch (addTo)
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
        roomNode = rn;
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
        return "MoveSingleRoom: addTo: " + addTo + " at " + roomNode.Position + ", replaceWith: " + replaceWith + ", dir: " + dir;
    }

    public MoveSingleRoom(RoomType aT, RoomType rW, Direction d)
    {
        addTo = aT;
        replaceWith = rW;
        dir = d;
        switch (replaceWith)
        {
            case RoomType.Enemy2:
                weight = 2f;
                break;
            case RoomType.Enemy4:
                weight = 0.7f;
                break;
            default:
                break;
        }
        switch (addTo)
        {
            case RoomType.Enemy1:
                weight *= 1f/3f;
                break;
            case RoomType.Loot:
                weight *= 1f / 3f;
                break;
            case RoomType.Boss:
                weight *= 1f / 3f;
                break;
            case RoomType.Enemy2:
                weight *= 2f / 3f;
                break;
            default:
                break;
        }
        weight *= 0.4f;
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
        switch (toAdd)
        {
            case RoomType.Enemy2:
                weight = 2f;
                break;
            case RoomType.Enemy4:
                weight = 0.7f;
                break;
            default:
                break;
        }
    }
}

class AddLootRoom : AddSingleRoom
{
    public override void Apply(RoomNode rn)
    {
        rn.Create(RoomType.Loot, dir);
        roomNode = rn;
    }

    public override bool IsApplicable(RoomNode rn)
    {
        return CanAddSingleRoom(rn) && Lg.numberOfLootRooms < Lg.maxNumberOfLootRooms;
    }
    public override string ToString()
    {
        return "AddSingleRoom: addTo: " + addTo + " at " + roomNode.Position + ", toAdd: Loot, dir: " + dir;
    }
    public AddLootRoom(RoomType aT, Direction d)
    {
        addTo = aT;
        toAdd = RoomType.Loot;
        dir = d;
        weight = 0.5f;
    }
}

class AddBossRoom : AddSingleRoom
{
    public override void Apply(RoomNode rn)
    {
        rn.Create(RoomType.Boss, dir);
        roomNode = rn;
    }

    public override bool IsApplicable(RoomNode rn)
    {
        return CanAddSingleRoom(rn) && Lg.numberOfBossRooms < Lg.maxNumberOfBossRooms;
    }
    public override string ToString()
    {
        return "AddSingleRoom: addTo: " + addTo + " at " + roomNode.Position + ", toAdd: Boss, dir: " + dir;
    }
    public AddBossRoom(RoomType aT, Direction d)
    {
        addTo = aT;
        toAdd = RoomType.Boss;
        dir = d;
        weight = 0.05f;
    }
}

class ForceRoom : AddSingleRoom
{
    public override void Apply(RoomNode rn)
    {
        rn.Create(toAdd, dir);
        if(RoomNode.MaxDoors(rn.Type) < rn.DoorCount())
        {
            rn.Type = PlusOne(rn.Type);
        }
        roomNode = rn;
    }

    public RoomType PlusOne(RoomType rt)
    {
        switch (rt)
        {
            case (RoomType.Enemy1):
                return RoomType.Enemy2;
            case (RoomType.Enemy2):
                return RoomType.Enemy3;
            case (RoomType.Enemy3):
                return RoomType.Enemy4;
            default:
                throw new System.ArgumentException();
        }
    }

    public override bool IsApplicable(RoomNode rn)
    {
        return CanForceSingleRoom(rn);
    }

    public bool CanForceSingleRoom(RoomNode rn)
    {
        return isExtendable(rn) && Lg.roomList.Count < Lg.maxNumberOfRooms && rn.Type == addTo && rn.Get(dir) == null && !Lg.rooms.ContainsKey(rn.Position + directionMovementMap[dir]) && toAdd != RoomType.Start;
    }

    public bool isExtendable(RoomNode rn)
    {
        switch (rn.Type)
        {
            case (RoomType.Enemy1):
                return true;
            case (RoomType.Enemy2):
                return true;
            case (RoomType.Enemy3):
                return true;
            case (RoomType.Enemy4):
                return rn.DoorCount() < 4;
            default:
                return false;
        }
    }
    

    public override string ToString()
    {
        return "ForceSingleRoom: addTo: " + addTo + " at " + roomNode.Position + ", toAdd: " + toAdd + ", dir: " + dir;
    }

}

class ForceBossRoom : ForceRoom
{
    public override void Apply(RoomNode rn)
    {
        rn.Create(RoomType.Boss, dir);
        if (RoomNode.MaxDoors(rn.Type) < rn.DoorCount())
        {
            rn.Type = PlusOne(rn.Type);
        }
        roomNode = rn;
    }

    public override bool IsApplicable(RoomNode rn)
    {
        return CanForceSingleRoom(rn) && Lg.numberOfBossRooms < Lg.maxNumberOfBossRooms;
    }

    public override string ToString()
    {
        return "ForceSingleRoom: addTo: " + addTo + " at " + roomNode.Position + ", toAdd: Boss, dir: " + dir;
    }

    public ForceBossRoom(Direction d)
    {
        toAdd = RoomType.Loot;
        dir = d;
        weight = 0.0001f;
    }
}

class ForceLootRoom : ForceRoom
{
    public override void Apply(RoomNode rn)
    {
        rn.Create(RoomType.Loot, dir);
        if (RoomNode.MaxDoors(rn.Type) < rn.DoorCount())
        {
            rn.Type = PlusOne(rn.Type);
        }
        roomNode = rn;
    }

    public override bool IsApplicable(RoomNode rn)
    {
        return CanForceSingleRoom(rn) && Lg.numberOfLootRooms < Lg.maxNumberOfLootRooms;
    }

    public override string ToString()
    {
        return "ForceSingleRoom: addTo: " + addTo + " at " + roomNode.Position + ", toAdd: Loot, dir: " + dir;
    }

    public ForceLootRoom(Direction d)
    {
        toAdd = RoomType.Loot;
        dir = d;
        weight = 0.0001f;
    }
}