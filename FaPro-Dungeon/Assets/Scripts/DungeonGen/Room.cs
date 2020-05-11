using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class Room : MonoBehaviour
{

    public int Width;

    public int Height;

    public int X;

    public int Y;

    public Door leftDoor, rightDoor, topDoor, bottomDoor;

    public List<Door> doors = new List<Door>();

    // Start is called before the first frame update
    void Start()
    {
        if(RoomController.instance == null)
        {
            Debug.Log("Wrong scene!");
            return;
        }

        Door[] ds = GetComponentsInChildren<Door>();
        foreach (Door d in ds)
        {
            doors.Add(d);
            if (d.doorType == Door.DoorType.right)
            {
                rightDoor = d;
            }
            if(d.doorType == Door.DoorType.left)
            {
                leftDoor = d;
            }
            if(d.doorType == Door.DoorType.top)
            {
                topDoor = d;
            }
            if(d.doorType == Door.DoorType.bottom)
            {
                bottomDoor = d;
            }
        }

        //Wenn RoomController läuft(korrekte Szene gestartet, dann registriere diesen Raum)
        RoomController.instance.RegisterRoom(this);
    }

    public void RemoveUnconnectedDoors()
    {
        foreach(Door door in doors)
        {
            if (door.doorType == Door.DoorType.right)
            {
                if(GetRight() == null)
                {
                    door.gameObject.SetActive(false);
                }
            }
            if (door.doorType == Door.DoorType.left)
            {
                if (GetLeft() == null)
                {
                    door.gameObject.SetActive(false);
                }
            }
            if (door.doorType == Door.DoorType.top)
            {
                if (GetTop() == null)
                {
                    door.gameObject.SetActive(false);
                }
            }
            if (door.doorType == Door.DoorType.bottom)
            {
                if (GetBottom() == null)
                {
                    door.gameObject.SetActive(false);
                }
            }
        }
    }

    public Room GetRight()
    {
        if(RoomController.instance.DoesRoomExist(X + 1, Y))
        {
            return RoomController.instance.FindRoom(X + 1, Y);
        }
        return null;
    }

    public Room GetLeft()
    {
        if(RoomController.instance.DoesRoomExist(X - 1, Y))
        {
            return RoomController.instance.FindRoom(X - 1, Y);
        }
        return null;
    }

    public Room GetTop()
    {
        if(RoomController.instance.DoesRoomExist(X, Y + 1))
        {
            return RoomController.instance.FindRoom(X, Y + 1);
        }
        return null;
    }

    public Room GetBottom()
    {
        if(RoomController.instance.DoesRoomExist(X, Y - 1))
        {
            return RoomController.instance.FindRoom(X, Y - 1);
        }
        return null;
    }

    //Visualisierung des Raumes durch Linien
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(Width, Height, 0));
    }

    public Vector3 GetRoomCentre()
    {
        return new Vector3(X * Width, Y * Height);
    }

    //Starte Kamerabewegung zu neuem Raum, wenn Raumcollider durch Spieler ausgelöst wird
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            RoomController.instance.OnPlayerEnterRoom(this);
        }
    }
}
