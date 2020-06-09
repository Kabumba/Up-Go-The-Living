using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    //Directional Acceleration
    public Vector2 acceleration;

    //Acceleration Stops when this directional velocity is reached
    public Vector2 desiredVelocity;

    //Max absolute velocity
    public float speed;

    //Absolute Acceleration
    public float accelerationValue;

    //RigisBody that is controlled by this
    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Returns a normalized Vector pointing in the current forward direction of the Rigidbody
    public Vector2 Rotation()
    {
        float angle = (rb.rotation+90) * Mathf.Deg2Rad;
        return new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
    }

    public void MoveForward()
    {
        acceleration = Rotation() * accelerationValue;
        desiredVelocity = acceleration.normalized * speed;
        if (rb.velocity.magnitude == 0)
        {
            rb.velocity = acceleration;
        }
        else
        {
            if (rb.velocity != desiredVelocity)
            {
                rb.velocity += acceleration;
            }
            if (rb.velocity.magnitude > speed)
            {

                rb.velocity = speed * rb.velocity.normalized;
            }
        }
    }

    public void SlowDown()
    {
        acceleration = Rotation() * accelerationValue;
        desiredVelocity = new Vector2(0, 0);
        if (rb.velocity != desiredVelocity)
        {
            Vector2 vchange = accelerationValue * rb.velocity;
            if (vchange.magnitude <= rb.velocity.magnitude)
            {
                rb.velocity -= accelerationValue * rb.velocity;
            }
            else
            {
                rb.velocity = desiredVelocity;
            }
        }
    }
}
