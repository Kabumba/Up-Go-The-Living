using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{
    public LayoutGenerator Lg;

    private List<RoomNode> rooms;


    private void Start()
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
            if (room.Up != null && room.Right == null && room.Down == null && room.Left == null)
            {
                RoomController.instance.LoadRoom("4", room.Position.x, room.Position.y);
            }
            if (room.Up == null && room.Right != null && room.Down == null && room.Left == null)
            {
                RoomController.instance.LoadRoom("1", room.Position.x, room.Position.y);
            }
            if (room.Up == null && room.Right == null && room.Down != null && room.Left == null)
            {
                RoomController.instance.LoadRoom("3", room.Position.x, room.Position.y);
            }
            if (room.Up == null && room.Right == null && room.Down == null && room.Left != null)
            {
                RoomController.instance.LoadRoom("2", room.Position.x, room.Position.y);
            }
            if (room.Up != null && room.Right != null && room.Down != null && room.Left != null)
            {
                RoomController.instance.LoadRoom("11", room.Position.x, room.Position.y);
            }
            if (room.Up != null && room.Right != null && room.Down == null && room.Left != null)
            {
                RoomController.instance.LoadRoom("7", room.Position.x, room.Position.y);
            }
            if (room.Up == null && room.Right != null && room.Down != null && room.Left != null)
            {
                RoomController.instance.LoadRoom("8", room.Position.x, room.Position.y);
            }
            if (room.Up != null && room.Right == null && room.Down != null && room.Left != null)
            {
                RoomController.instance.LoadRoom("9", room.Position.x, room.Position.y);
            }
            if (room.Up != null && room.Right != null && room.Down != null && room.Left == null)
            {
                RoomController.instance.LoadRoom("10", room.Position.x, room.Position.y);
            }
            if (room.Up != null && room.Right == null && room.Down != null && room.Left == null)
            {
                RoomController.instance.LoadRoom("6", room.Position.x, room.Position.y);
            }
            if (room.Up == null && room.Right != null && room.Down == null && room.Left != null)
            {
                RoomController.instance.LoadRoom("5", room.Position.x, room.Position.y);
            }
            if (room.Up == null && room.Right != null && room.Down != null && room.Left == null)
            {
                RoomController.instance.LoadRoom("13", room.Position.x, room.Position.y);
            }
            if (room.Up == null && room.Right == null && room.Down != null && room.Left != null)
            {
                RoomController.instance.LoadRoom("14", room.Position.x, room.Position.y);
            }
            if (room.Up != null && room.Right == null && room.Down == null && room.Left != null)
            {
                RoomController.instance.LoadRoom("15", room.Position.x, room.Position.y);
            }
            if (room.Up != null && room.Right != null && room.Down == null && room.Left == null)
            {
                RoomController.instance.LoadRoom("12", room.Position.x, room.Position.y);
            }
        }
    }

    private void SpawnStartRoom (List<RoomNode> rooms)
    {
        RoomNode startRoom = rooms[0];
        if (startRoom.Up != null && startRoom.Right == null && startRoom.Down == null && startRoom.Left == null)
        {
            RoomController.instance.LoadRoom("Start4", 0, 0);
        }
        if (startRoom.Up == null && startRoom.Right != null && startRoom.Down == null && startRoom.Left == null)
        {
            RoomController.instance.LoadRoom("Start1", 0, 0);
        }
        if (startRoom.Up == null && startRoom.Right == null && startRoom.Down != null && startRoom.Left == null)
        {
            RoomController.instance.LoadRoom("Start3", 0, 0);
        }
        if (startRoom.Up == null && startRoom.Right == null && startRoom.Down == null && startRoom.Left != null)
        {
            RoomController.instance.LoadRoom("Start2", 0, 0);
        }
        if (startRoom.Up != null && startRoom.Right != null && startRoom.Down != null && startRoom.Left != null)
        {
            RoomController.instance.LoadRoom("Start11", 0, 0);
        }
        if (startRoom.Up != null && startRoom.Right != null && startRoom.Down == null && startRoom.Left != null)
        {
            RoomController.instance.LoadRoom("Start7", 0, 0);
        }
        if (startRoom.Up == null && startRoom.Right != null && startRoom.Down != null && startRoom.Left != null)
        {
            RoomController.instance.LoadRoom("Start8", 0, 0);
        }
        if (startRoom.Up != null && startRoom.Right == null && startRoom.Down != null && startRoom.Left != null)
        {
            RoomController.instance.LoadRoom("Start9", 0, 0);
        }
        if (startRoom.Up != null && startRoom.Right != null && startRoom.Down != null && startRoom.Left == null)
        {
            RoomController.instance.LoadRoom("Start10", 0, 0);
        }
        if (startRoom.Up != null && startRoom.Right == null && startRoom.Down != null && startRoom.Left == null)
        {
            RoomController.instance.LoadRoom("Start6", 0, 0);
        }
        if (startRoom.Up == null && startRoom.Right != null && startRoom.Down == null && startRoom.Left != null)
        {
            RoomController.instance.LoadRoom("Start5", 0, 0);
        }
        if (startRoom.Up == null && startRoom.Right != null && startRoom.Down != null && startRoom.Left == null)
        {
            RoomController.instance.LoadRoom("Start13", 0, 0);
        }
        if (startRoom.Up == null && startRoom.Right == null && startRoom.Down != null && startRoom.Left != null)
        {
            RoomController.instance.LoadRoom("Start14", 0, 0);
        }
        if (startRoom.Up != null && startRoom.Right == null && startRoom.Down == null && startRoom.Left != null)
        {
            RoomController.instance.LoadRoom("Start15", 0, 0);
        }
        if (startRoom.Up != null && startRoom.Right != null && startRoom.Down == null && startRoom.Left == null)
        {
            RoomController.instance.LoadRoom("Start12", 0, 0);
        }
    }
}
