using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceHelper : MonoBehaviour
{
    void Update()
    {
        GameObject parent = transform.parent.gameObject;
        GetComponent<Rigidbody2D>().velocity = parent.GetComponent<Rigidbody2D>().velocity;
        transform.position = parent.transform.position;
        transform.rotation = parent.transform.rotation;
        GetComponent<Rigidbody2D>().position = parent.GetComponent<Rigidbody2D>().position;
    }
}
