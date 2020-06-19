using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{
    public LayoutGenerator Lg;

    private List<RoomNode> rooms;

    public GameObject player;

    private void Start()
    {
        NewFloor();
    }

    public void NewFloor()
    {
        Lg.Initialize();
        rooms = Lg.GetLayout();
        SpawnRooms(rooms);
    }

    private void SpawnRooms(List<RoomNode> rooms)
    {
        SpawnStartRoom(rooms);
        foreach(var room in rooms)
        {
            if (room.Type.Equals(RoomType.Boss))
            {
                SpawnEndRoom(room);
            }
            if( room.Type.Equals(RoomType.Loot))
            {
                SpawnItemRoom(room);
            }
            if( !room.Type.Equals(RoomType.Boss) && !room.Type.Equals(RoomType.Loot) && !room.Type.Equals(RoomType.Start))
            {
                SpawnEnemyRooms(room);
            }
        }
    }

    private void SpawnEnemyRooms(RoomNode room)
    {
        if (room.Up != null && room.Right == null && room.Down == null && room.Left == null)
        {
            RoomController.instance.LoadRoom("U", room.Position.x, room.Position.y);
        }
        if (room.Up == null && room.Right != null && room.Down == null && room.Left == null)
        {
            RoomController.instance.LoadRoom("R", room.Position.x, room.Position.y);
        }
        if (room.Up == null && room.Right == null && room.Down != null && room.Left == null)
        {
            RoomController.instance.LoadRoom("D", room.Position.x, room.Position.y);
        }
        if (room.Up == null && room.Right == null && room.Down == null && room.Left != null)
        {
            RoomController.instance.LoadRoom("L", room.Position.x, room.Position.y);
        }
        if (room.Up != null && room.Right != null && room.Down != null && room.Left != null)
        {
            RoomController.instance.LoadRoom("URDL", room.Position.x, room.Position.y);
        }
        if (room.Up != null && room.Right != null && room.Down == null && room.Left != null)
        {
            RoomController.instance.LoadRoom("URL", room.Position.x, room.Position.y);
        }
        if (room.Up == null && room.Right != null && room.Down != null && room.Left != null)
        {
            RoomController.instance.LoadRoom("RDL", room.Position.x, room.Position.y);
        }
        if (room.Up != null && room.Right == null && room.Down != null && room.Left != null)
        {
            RoomController.instance.LoadRoom("UDL", room.Position.x, room.Position.y);
        }
        if (room.Up != null && room.Right != null && room.Down != null && room.Left == null)
        {
            RoomController.instance.LoadRoom("URD", room.Position.x, room.Position.y);
        }
        if (room.Up != null && room.Right == null && room.Down != null && room.Left == null)
        {
            RoomController.instance.LoadRoom("UD", room.Position.x, room.Position.y);
        }
        if (room.Up == null && room.Right != null && room.Down == null && room.Left != null)
        {
            RoomController.instance.LoadRoom("RL", room.Position.x, room.Position.y);
        }
        if (room.Up == null && room.Right != null && room.Down != null && room.Left == null)
        {
            RoomController.instance.LoadRoom("RD", room.Position.x, room.Position.y);
        }
        if (room.Up == null && room.Right == null && room.Down != null && room.Left != null)
        {
            RoomController.instance.LoadRoom("DL", room.Position.x, room.Position.y);
        }
        if (room.Up != null && room.Right == null && room.Down == null && room.Left != null)
        {
            RoomController.instance.LoadRoom("UL", room.Position.x, room.Position.y);
        }
        if (room.Up != null && room.Right != null && room.Down == null && room.Left == null)
        {
            RoomController.instance.LoadRoom("UR", room.Position.x, room.Position.y);
        }
    }

    private void SpawnStartRoom (List<RoomNode> rooms)
    {
        RoomNode startRoom = rooms[0];
        if (startRoom.Up != null && startRoom.Right == null && startRoom.Down == null && startRoom.Left == null)
        {
            RoomController.instance.LoadRoom("StartU", 0, 0);
        }
        if (startRoom.Up == null && startRoom.Right != null && startRoom.Down == null && startRoom.Left == null)
        {
            RoomController.instance.LoadRoom("StartR", 0, 0);
        }
        if (startRoom.Up == null && startRoom.Right == null && startRoom.Down != null && startRoom.Left == null)
        {
            RoomController.instance.LoadRoom("StartD", 0, 0);
        }
        if (startRoom.Up == null && startRoom.Right == null && startRoom.Down == null && startRoom.Left != null)
        {
            RoomController.instance.LoadRoom("StartL", 0, 0);
        }
        if (startRoom.Up != null && startRoom.Right != null && startRoom.Down != null && startRoom.Left != null)
        {
            RoomController.instance.LoadRoom("StartURDL", 0, 0);
        }
        if (startRoom.Up != null && startRoom.Right != null && startRoom.Down == null && startRoom.Left != null)
        {
            RoomController.instance.LoadRoom("StartURL", 0, 0);
        }
        if (startRoom.Up == null && startRoom.Right != null && startRoom.Down != null && startRoom.Left != null)
        {
            RoomController.instance.LoadRoom("StartRDL", 0, 0);
        }
        if (startRoom.Up != null && startRoom.Right == null && startRoom.Down != null && startRoom.Left != null)
        {
            RoomController.instance.LoadRoom("StartUDL", 0, 0);
        }
        if (startRoom.Up != null && startRoom.Right != null && startRoom.Down != null && startRoom.Left == null)
        {
            RoomController.instance.LoadRoom("StartURD", 0, 0);
        }
        if (startRoom.Up != null && startRoom.Right == null && startRoom.Down != null && startRoom.Left == null)
        {
            RoomController.instance.LoadRoom("StartUD", 0, 0);
        }
        if (startRoom.Up == null && startRoom.Right != null && startRoom.Down == null && startRoom.Left != null)
        {
            RoomController.instance.LoadRoom("StartRL", 0, 0);
        }
        if (startRoom.Up == null && startRoom.Right != null && startRoom.Down != null && startRoom.Left == null)
        {
            RoomController.instance.LoadRoom("StartRD", 0, 0);
        }
        if (startRoom.Up == null && startRoom.Right == null && startRoom.Down != null && startRoom.Left != null)
        {
            RoomController.instance.LoadRoom("StartDL", 0, 0);
        }
        if (startRoom.Up != null && startRoom.Right == null && startRoom.Down == null && startRoom.Left != null)
        {
            RoomController.instance.LoadRoom("StartUL", 0, 0);
        }
        if (startRoom.Up != null && startRoom.Right != null && startRoom.Down == null && startRoom.Left == null)
        {
            RoomController.instance.LoadRoom("StartUR", 0, 0);
        }
    }

    public void SpawnEndRoom(RoomNode room)
    {
        if(room.Up != null)
        {
            RoomController.instance.LoadRoom("EndU", room.Position.x, room.Position.y);
        }
        if(room.Right != null)
        {
            RoomController.instance.LoadRoom("EndR", room.Position.x, room.Position.y);
        }        
        if(room.Down != null)
        {
            RoomController.instance.LoadRoom("EndD", room.Position.x, room.Position.y);
        }        
        if(room.Left != null)
        {
            RoomController.instance.LoadRoom("EndL", room.Position.x, room.Position.y);
        }
    }

    public void SpawnItemRoom(RoomNode room)
    {
        if (room.Up != null)
        {
            RoomController.instance.LoadRoom("ItemU", room.Position.x, room.Position.y);
        }
        if (room.Right != null)
        {
            RoomController.instance.LoadRoom("ItemR", room.Position.x, room.Position.y);
        }
        if (room.Down != null)
        {
            RoomController.instance.LoadRoom("ItemD", room.Position.x, room.Position.y);
        }
        if (room.Left != null)
        {
            RoomController.instance.LoadRoom("ItemL", room.Position.x, room.Position.y);
        }
    }
}
