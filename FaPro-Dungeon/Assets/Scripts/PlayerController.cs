using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    
    public ShooterController shc;

    public Rigidbody2D rb;

    // Start is called before the first frame update

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        shc = gameObject.GetComponent<ShooterController>();
        
    }
    
    // Update is called once per frame
    void Update()
    {
        shc.fireDelay = GameController.GetDelayBetweenShots();
        speed = GameController.GetMoveSpeed();
        shc.bulletSpeed = GameController.GetBulletSpeed();

        //Inputs
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        float shootHorizontal = Input.GetAxis("ShootHorizontal");
        float shootVertical = Input.GetAxis("ShootVertical");

        //basic Movement bei dem man sich diagonal schneller bewegt
        //rigidbody.velocity = new Vector3(horizontal * speed, vertical * speed, 0); 

        float unadjustedSpeed = Mathf.Sqrt(horizontal * horizontal + vertical * vertical);
        float speedfactor = unadjustedSpeed == 0 ? 0 : speed / unadjustedSpeed;
        rb.velocity = new Vector3(horizontal * speedfactor, vertical * speedfactor, 0);

        float x = 0;
        float y = 0;
        
        //Schüsse nur Vertikal oder Horizontal
        if (shootVertical != 0)
        {
            y = shootVertical;
        }
        else
            if (shootHorizontal != 0)
        {
            x = shootHorizontal;
        }

        float fixedX = (x < 0) ? Mathf.Floor(x) : Mathf.Ceil(x);
        float fixedY = (y < 0) ? Mathf.Floor(y) : Mathf.Ceil(y);
        if (fixedX != 0 || fixedY != 0)
        {
            if (fixedX == 0)
            {
                transform.rotation = Quaternion.Euler(0, 0, 90f - 90f * fixedY);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 0, -90f * fixedX);
            }
            shc.TryShoot();
        }


    }
}
