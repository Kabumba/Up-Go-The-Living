using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DungeonGenerationData.asset", menuName = "DungeonGenerationData/Dungeon Data")]

public class DungeonGenerationData : ScriptableObject
{
    public int minNumberOfRooms = 7;

    public int maxNumberOfRooms = 10;

    public int numberOfBossRooms = 1;

    public int numberOfLootRooms = 1;
}
