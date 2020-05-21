using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{
    public LayoutGenerator Lg;

    private Dictionary<Vector2Int, RoomNode> rooms;


    private void Start()
    {
        Lg.Initialize();
        rooms = Lg.GetLayout();
        SpawnRooms(rooms);
    }

    private void SpawnRooms(Dictionary<Vector2Int, RoomNode> rooms)
    {
        SpawnStartRoom(rooms);
        foreach(var room in rooms)
        {
            if (room.Value.Up != null && room.Value.Right == null && room.Value.Down == null && room.Value.Left == null)
            {
                RoomController.instance.LoadRoom("4", room.Key.x, room.Key.y);
            }
            if (room.Value.Up == null && room.Value.Right != null && room.Value.Down == null && room.Value.Left == null)
            {
                RoomController.instance.LoadRoom("1", room.Key.x, room.Key.y);
            }
            if (room.Value.Up == null && room.Value.Right == null && room.Value.Down != null && room.Value.Left == null)
            {
                RoomController.instance.LoadRoom("3", room.Key.x, room.Key.y);
            }
            if (room.Value.Up == null && room.Value.Right == null && room.Value.Down == null && room.Value.Left != null)
            {
                RoomController.instance.LoadRoom("2", room.Key.x, room.Key.y);
            }
            if (room.Value.Up != null && room.Value.Right != null && room.Value.Down != null && room.Value.Left != null)
            {
                RoomController.instance.LoadRoom("11", room.Key.x, room.Key.y);
            }
            if (room.Value.Up != null && room.Value.Right != null && room.Value.Down == null && room.Value.Left != null)
            {
                RoomController.instance.LoadRoom("7", room.Key.x, room.Key.y);
            }
            if (room.Value.Up == null && room.Value.Right != null && room.Value.Down != null && room.Value.Left != null)
            {
                RoomController.instance.LoadRoom("8", room.Key.x, room.Key.y);
            }
            if (room.Value.Up != null && room.Value.Right == null && room.Value.Down != null && room.Value.Left != null)
            {
                RoomController.instance.LoadRoom("9", room.Key.x, room.Key.y);
            }
            if (room.Value.Up != null && room.Value.Right != null && room.Value.Down != null && room.Value.Left == null)
            {
                RoomController.instance.LoadRoom("10", room.Key.x, room.Key.y);
            }
            if (room.Value.Up != null && room.Value.Right == null && room.Value.Down != null && room.Value.Left == null)
            {
                RoomController.instance.LoadRoom("6", room.Key.x, room.Key.y);
            }
            if (room.Value.Up == null && room.Value.Right != null && room.Value.Down == null && room.Value.Left != null)
            {
                RoomController.instance.LoadRoom("5", room.Key.x, room.Key.y);
            }
            if (room.Value.Up == null && room.Value.Right != null && room.Value.Down != null && room.Value.Left == null)
            {
                RoomController.instance.LoadRoom("13", room.Key.x, room.Key.y);
            }
            if (room.Value.Up == null && room.Value.Right == null && room.Value.Down != null && room.Value.Left != null)
            {
                RoomController.instance.LoadRoom("14", room.Key.x, room.Key.y);
            }
            if (room.Value.Up != null && room.Value.Right == null && room.Value.Down == null && room.Value.Left != null)
            {
                RoomController.instance.LoadRoom("15", room.Key.x, room.Key.y);
            }
            if (room.Value.Up != null && room.Value.Right != null && room.Value.Down == null && room.Value.Left == null)
            {
                RoomController.instance.LoadRoom("12", room.Key.x, room.Key.y);
            }
        }
    }

    private void SpawnStartRoom (Dictionary<Vector2Int, RoomNode> rooms)
    {
        RoomNode room = rooms.Single(s => s.Key.x == 0 && s.Key.y == 0).Value;
        if (room.Up != null && room.Right == null && room.Down == null && room.Left == null)
        {
            RoomController.instance.LoadRoom("Start4", 0, 0);
        }
        if (room.Up == null && room.Right != null && room.Down == null && room.Left == null)
        {
            RoomController.instance.LoadRoom("Start1", 0, 0);
        }
        if (room.Up == null && room.Right == null && room.Down != null && room.Left == null)
        {
            RoomController.instance.LoadRoom("Start3", 0, 0);
        }
        if (room.Up == null && room.Right == null && room.Down == null && room.Left != null)
        {
            RoomController.instance.LoadRoom("Start2", 0, 0);
        }
        if (room.Up != null && room.Right != null && room.Down != null && room.Left != null)
        {
            RoomController.instance.LoadRoom("Start11", 0, 0);
        }
        if (room.Up != null && room.Right != null && room.Down == null && room.Left != null)
        {
            RoomController.instance.LoadRoom("Start7", 0, 0);
        }
        if (room.Up == null && room.Right != null && room.Down != null && room.Left != null)
        {
            RoomController.instance.LoadRoom("Start8", 0, 0);
        }
        if (room.Up != null && room.Right == null && room.Down != null && room.Left != null)
        {
            RoomController.instance.LoadRoom("Start9", 0, 0);
        }
        if (room.Up != null && room.Right != null && room.Down != null && room.Left == null)
        {
            RoomController.instance.LoadRoom("Start10", 0, 0);
        }
        if (room.Up != null && room.Right == null && room.Down != null && room.Left == null)
        {
            RoomController.instance.LoadRoom("Start6", 0, 0);
        }
        if (room.Up == null && room.Right != null && room.Down == null && room.Left != null)
        {
            RoomController.instance.LoadRoom("Start5", 0, 0);
        }
        if (room.Up == null && room.Right != null && room.Down != null && room.Left == null)
        {
            RoomController.instance.LoadRoom("Start13", 0, 0);
        }
        if (room.Up == null && room.Right == null && room.Down != null && room.Left != null)
        {
            RoomController.instance.LoadRoom("Start14", 0, 0);
        }
        if (room.Up != null && room.Right == null && room.Down == null && room.Left != null)
        {
            RoomController.instance.LoadRoom("Start15", 0, 0);
        }
        if (room.Up != null && room.Right != null && room.Down == null && room.Left == null)
        {
            RoomController.instance.LoadRoom("Start12", 0, 0);
        }
    }
}
