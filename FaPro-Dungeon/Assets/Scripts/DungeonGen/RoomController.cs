﻿using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomInfo
{
    public string name;

    public int X;

    public int Y;
}

public class RoomController : MonoBehaviour
{

    public static RoomController instance;

    string currentWorldName = "Basement";

    RoomInfo currentLoadRoomData;

    Queue<RoomInfo> loadRoomQueue = new Queue<RoomInfo>();

    public List<Room> loadedRooms = new List<Room>();

    bool isLoadingRoom = false;

    void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        LoadRoom("Start", 0, 0);
        LoadRoom("Start", 0, 1);
        LoadRoom("Start", 0, -1);
        LoadRoom("Start", 1, 0);
        LoadRoom("Start", -1, 0);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateRoomQueue();
    }

    //Stellt regelmäßig sicher, dass zu ladende Räume auch geladen und registriert werden
    void UpdateRoomQueue()
    {
        if(isLoadingRoom)
        {
            return;
        }

        if(loadRoomQueue.Count == 0)
        {
            return;
        }

        currentLoadRoomData = loadRoomQueue.Dequeue();
        isLoadingRoom = true;

        StartCoroutine(LoadRoomRoutine(currentLoadRoomData));
    }

    //Erstellt zu ladenden Raum mit Parametern und fügt einer Warteschlange hinzu
    public void LoadRoom(string name, int x, int y)
    {
        if(DoesRoomExist(x, y))
        {
            return;
        }
        RoomInfo newRoomData = new RoomInfo();
        newRoomData.name = name;
        newRoomData.X = x;
        newRoomData.Y = y;

        loadRoomQueue.Enqueue(newRoomData);
    }

    //Ermöglicht das Laden der korrekten Szenen (z.B. roomName = BasementStart -> BasementStart Szene wird geladen)
    IEnumerator LoadRoomRoutine(RoomInfo info)
    {
        string roomName = currentWorldName + info.name;

        AsyncOperation loadRoom = SceneManager.LoadSceneAsync(roomName, LoadSceneMode.Additive);

        while(loadRoom.isDone == false)
        {
            yield return null;
        }
    }

    public void RegisterRoom(Room room)
    {
        //transform Position für neuen Raum setzen
        room.transform.position = new Vector3(          
                currentLoadRoomData.X * room.Width,
                currentLoadRoomData.Y * room.Height,
                0
            );

        //Attribute für Room Objekt setzen
        room.X = currentLoadRoomData.X;
        room.Y = currentLoadRoomData.Y;
        room.name = currentWorldName + "-" + currentLoadRoomData.name + " " + room.X + ", " + room.Y;

        //hierarchische Unterordnung der generierten Räume unter dem RoomController
        room.transform.parent = transform;

        isLoadingRoom = false;

        loadedRooms.Add(room);
    }

    public bool DoesRoomExist( int x, int y)
    {
        return loadedRooms.Find(item => item.X == x && item.Y == y) != null;
    }
}
