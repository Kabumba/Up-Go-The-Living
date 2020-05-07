using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public static CameraController instance;

    public Room currRoom;

    public float moveSpeedRoomChange;

    void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePosition();
    }

    //Überprüfe regelmäßig, ob ein neuer Raum erreicht wurde, wenn einer erreicht wurde, aktualisiere Kameraposition von vorheriger Raummitte zu neuer Raummitte
    void UpdatePosition()
    {
        if(currRoom == null)
        {
            return;
        }

        Vector3 targetPos = GetCameraTargetPosition();

        transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * moveSpeedRoomChange);
    }

    //Bestimme und gebe Raummitte des neuen Raums aus
    Vector3 GetCameraTargetPosition()
    {
        if(currRoom == null)
        {
            return Vector3.zero;
        }

        Vector3 targetPos = currRoom.GetRoomCentre();
        targetPos.z = transform.position.z;

        return targetPos;
    }

    public bool IsSwitchingScene()
    {
        return transform.position.Equals(GetCameraTargetPosition());
    }
}
