using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixRotation : MonoBehaviour
{
    public Quaternion rotation;
    
    // Start is called before the first frame update
    void Start()
    {
        rotation = gameObject.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.rotation = rotation;
    }
}
