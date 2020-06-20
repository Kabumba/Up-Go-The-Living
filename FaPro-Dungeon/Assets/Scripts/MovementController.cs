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

    public bool bounceOffObstacle;
    public bool bounceOffPlayer;
    public bool bounceOffEnemy;
    public bool bounceOffProjectile;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    //Returns a normalized Vector pointing in the current forward direction of the Rigidbody
    public Vector2 GetRotation()
    {
        float angle = (rb.rotation + 90) * Mathf.Deg2Rad;
        return new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
    }

    public void SetRotation(Vector2 setTo)
    {
        float angle = (rb.rotation + 90) * Mathf.Deg2Rad;
        rb.rotation = Vector2.SignedAngle(new Vector2(0, 1), setTo);
        transform.rotation = Quaternion.Euler(new Vector3(0,0,rb.rotation));
    }

    public void RotateTowards(GameObject towards)
    {
        SetRotation(towards.transform.position - transform.position);
    }

    public void MoveForward()
    {
        acceleration = GetRotation() * accelerationValue;
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

    public void MoveForwardFixedSpeed()
    {
        rb.velocity = speed * GetRotation();
    }

    public void SlowDown()
    {
        acceleration = GetRotation() * accelerationValue;
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

    public void Dash(float dashSpeed, float dashTime)
    {
        
        Vector2 dash = GetRotation() * dashSpeed;
        rb.velocity = dash;
        StartCoroutine(DashSlowDown(dash, dashTime));
    }

    IEnumerator DashSlowDown(Vector2 dash, float dashTime)
    {
        Vector2 vel = dash;
        float StartTime = Time.time;
        float EndTime = StartTime + dashTime;
        while (Time.time <= EndTime)
        {
            vel = GetRotation() * vel.magnitude;
            rb.velocity = vel * (EndTime - Time.time) / dashTime;
            yield return null;
        }
        rb.velocity = new Vector2(0, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        bool bounceOff = false;
        switch (collision.gameObject.tag)
        {
            case "Player":
                bounceOff = bounceOff || bounceOffPlayer;
                break;
            case "Enemy":
                bounceOff = bounceOff || bounceOffEnemy;
                break;
            case "Projectile":
                bounceOff = bounceOff || bounceOffProjectile;
                break;
            default:
                bounceOff = bounceOff || bounceOffObstacle;
                break;
        }
        if (bounceOff)
        {
            Vector2 n = collision.GetContact(0).normal;
            Vector2 d = GetRotation();
            Vector2 r = d - 2 * Vector2.Dot(d, n) * n;
            SetRotation(r);
        }
    }
}
