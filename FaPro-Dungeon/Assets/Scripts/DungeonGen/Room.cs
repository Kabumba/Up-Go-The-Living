using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class Room : MonoBehaviour
{

    public int Width;

    public int Height;

    public int X;

    public int Y;
    // Start is called before the first frame update
    void Start()
    {
        if(RoomController.instance == null)
        {
            Debug.Log("Wrong scene!");
            return;
        }

        //Wenn RoomController läuft(korrekte Szene gestartet, dann registriere diesen Raum)
        RoomController.instance.RegisterRoom(this);
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
