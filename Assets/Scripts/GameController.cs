using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    [SerializeField]
    Text collectedText;

    List<GameObject> starList;
    int collectedStars = 0;

	// Use this for initialization
	void Start () {
        starList = new List<GameObject>();
        starList.AddRange(GameObject.FindGameObjectsWithTag("Star"));

        collectedText.text = "Stars Collected: 0";
	}

    public void CollectedStar()
    {
        collectedStars++;

        collectedText.text = "Stars Collected: " + collectedStars;
        
        if (collectedStars == starList.Count)
        {
            FindObjectOfType<DoorScript>().OpenDoor();
        }
    }


}
