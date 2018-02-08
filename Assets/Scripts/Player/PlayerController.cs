using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [SerializeField]            // Offentliggör variabeln i inspektorn utan att använda "public"
    float moveSpeed = 10f;
    float movement;
    [SerializeField]
    float jumpForce = 100f;
    bool grounded = true;
    bool doubleJump = true;


    Rigidbody2D rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        movement = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Jump"))
            Jump();
	}

    // Körs every other frame
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(movement * moveSpeed, rb.velocity.y);
    }

    void Jump()
    {
        if (grounded)
        {
            rb.AddForce(new Vector2(0, jumpForce));
            grounded = false;
        }
        else if (doubleJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);    // Nollställ Y-leddens velocity
            rb.AddForce(new Vector2(0, jumpForce / 1.5f));         // Addera ny hoppkraft
            doubleJump = false;
        }
    }

    // Körs när något kolliderar med vår trigger-collider
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            grounded = true;
            doubleJump = true;
        }
    }
}
