using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    [SerializeField]            // Offentliggör variabeln i inspektorn utan att använda "public"
    float moveSpeed = 10f;
    float movement;

    [SerializeField]
    float jumpForce = 100f;
    bool grounded = true;       // Håller koll på om vi är på marken eller ej
    bool doubleJump = true;

    [SerializeField]
    Slider healthSlider;        // vi använder en UI slider för att visa hälsa ovanför spelaren
    int health = 100;
    bool invincible = false;
    Vector3 respawnPoint;       // Håller koordinater för senast besökta respawn point

    Rigidbody2D rb;
    Animator anim;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();   // Vi hämtar komponenterna från vårat gameobject
        anim = GetComponent<Animator>();
	}

	// Update is called once per frame
	void Update () {
        movement = Input.GetAxis("Horizontal"); // Vi tilldelar movement ett floatvärde mellan -1 till +1 beroende på knapp

        if (Input.GetButtonDown("Jump"))    // Vid knapptryck för "Jump" (spacebar) får vi ett boolvärde av true
            Jump(); // Anropas vid true


        if (!IsAlive())     // Om vi inte lever
        {
            gameObject.SetActive(false);        // Sätt spelaren till inaktiv
            transform.position = respawnPoint;  // Sätt spelarens position till senaste respawnpoint
            gameObject.SetActive(true);         // Sätt spelaren till aktiv
            health = 100;                       // Tilldela full hälsa
        }

        healthSlider.value = health;            // Sätt vår UI sliders värde till värdet av vår hälsa

        Animations();
	}

    // Ansvarar för animationer och localScale
    void Animations()
    {
        anim.SetFloat("walk", Mathf.Abs(movement));     // Tilldelar det absoluta värdet av movement till vår float-parameter(anim)
        anim.SetBool("jump", !grounded);                // Tilldelar värdet av !grounded(motsatt värde av grounded)

        // Här ändrar vi X positionen i localScale beroende på vilket håll vi rör oss
        // (Vi ändrar håll vi tittar åt)
        if (movement >= 0)
            transform.localScale = new Vector3(1, 1, 1);
        else
            transform.localScale = new Vector3(-1, 1, 1);
    }

    // Körs every other frame
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(movement * moveSpeed, rb.velocity.y);
    }

    bool IsAlive()
    {
        return health > 0;
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
        switch (collision.gameObject.tag)
        {
            case "Ground":
                grounded = true;
                doubleJump = true;
                break;

            case "DeathTrap":
                health -= 100;
                break;

            case "Respawn":
                respawnPoint = collision.gameObject.transform.position;
                respawnPoint.y++;
                break;
        }

        if (collision.gameObject.GetComponent<DamageController>() && !invincible)
        {
            health -= collision.gameObject.GetComponent<DamageController>().DealDamage();
            invincible = true;
            Invoke("BecomeMortal", 1.5f);
        }
    }

    void BecomeMortal()
    {
        invincible = false;
    }
}
