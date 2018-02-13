using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorScript : MonoBehaviour {

    [SerializeField]
    GameObject doorTop, doorBottom;

    [SerializeField]
    Sprite dTop, dBottom;

    bool opened = false;

    public void OpenDoor()
    {
        doorTop.GetComponent<SpriteRenderer>().sprite = dTop;
        doorBottom.GetComponent<SpriteRenderer>().sprite = dBottom;
        opened = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && opened)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
        }
    }
}
