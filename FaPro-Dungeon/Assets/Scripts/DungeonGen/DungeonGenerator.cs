using System.Collections;
using System.Collections.Generic;
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
        RoomController.instance.LoadRoom("Start", 0, 0);
        foreach(Vector2Int roomLocation in rooms.Keys)
        {
            int layout = Random.Range(1, 4);
            RoomController.instance.LoadRoom(layout.ToString(), roomLocation.x, roomLocation.y);
        }
    }
}
