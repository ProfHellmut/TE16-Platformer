using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour {

    [SerializeField]
    GameObject projectile;

    [SerializeField]
    float speed = 15f;
	
	// Update is called once per frame
	void Update () {
		
        if (Input.GetKeyDown(KeyCode.F))
        {
            GameObject clone = Instantiate(projectile, transform.position, Quaternion.identity);

            if (transform.position.x > GameObject.FindObjectOfType<PlayerController>().transform.position.x)
                clone.GetComponent<Rigidbody2D>().velocity = Vector2.right * speed * Time.deltaTime;
            else
                clone.GetComponent<Rigidbody2D>().velocity = Vector2.left * speed * Time.deltaTime;
        }

	}
}
