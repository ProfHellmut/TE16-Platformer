using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySnail : MonoBehaviour {

    [SerializeField]
    float moveSpeed = 1f;

    [SerializeField]
    Transform groundCheck;

    bool movingLeft = true;
    Vector2 direction;
	
	// Update is called once per frame
	void Update () {

        // TERNARY
        direction = movingLeft ? Vector2.left : Vector2.right;

        //if (movingLeft)
        //    direction = Vector2.left;
        //else
        //    direction = Vector2.right;

        transform.Translate(direction * moveSpeed * Time.deltaTime);

        RaycastHit2D hit = Physics2D.Raycast(groundCheck.position, Vector2.down, 1f);
        

        if (hit.collider == false)
        {
            ChangeDirection();
        }
	}

    void ChangeDirection()
    {
        
        if (movingLeft)
            transform.localScale = new Vector3(-2, 2, 2);
        else
            transform.localScale = new Vector3(2, 2, 2);

        movingLeft = !movingLeft;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Projectile")
        {
            ChangeDirection();
        }
    }
}
