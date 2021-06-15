using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public Rigidbody2D rb;

    Vector2 movement;
    Vector2 dir;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);

        if (movement.x != 0 || movement.y != 0)
        {
            float angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg - 90f;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }

        //changeDirection();
    }

    void changeDirection()
    {
        if(movement.x < -0.01)//left
        {
            if(movement.y < -0.01)//left down
            {
                transform.rotation = Quaternion.Euler(0, 0, 135);
            }
            else if(movement.y > 0.01)//left up
            {
                transform.rotation = Quaternion.Euler(0, 0, 45);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 0, 90);
            }
        }
        else if(movement.x > 0.01)//right
        {
            if (movement.y < -0.01)//right down
            {
                transform.rotation = Quaternion.Euler(0, 0, 225);
            }
            else if (movement.y > 0.01)//right up
            {
                transform.rotation = Quaternion.Euler(0, 0, -45);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 0, 270);
            }
        }
        else if(movement.y < -0.01)//down
        {
            transform.rotation = Quaternion.Euler(0, 0, 180);
        }
        else if(movement.y > 0.01)//up
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
