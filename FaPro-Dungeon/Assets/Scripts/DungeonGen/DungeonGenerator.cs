using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{
    public DungeonGenerationData dungeonGenerationData;

    public LayoutGenerator Lg;

    private List<Vector2Int> dungeonRooms;

    private Dictionary<Vector2Int, RoomNode> rooms;


    private void Start()
    {
        //dungeonRooms = DungeonCrawlerController.GenerateDungeon(dungeonGenerationData);
        rooms = Lg.GetLayout();

        SpawnRooms(rooms);
    }

    private void SpawnRooms(Dictionary<Vector2Int, RoomNode> rooms)
    {
        RoomController.instance.LoadRoom("Start", 0, 0);
        foreach(Vector2Int roomLocation in rooms.Keys)
        {
            RoomController.instance.LoadRoom("Empty", roomLocation.x, roomLocation.y);
        }
    }
}
