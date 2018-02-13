using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    List<GameObject> starList;
    int collectedStars = 0;

	// Use this for initialization
	void Start () {
        starList = new List<GameObject>();
        starList.AddRange(GameObject.FindGameObjectsWithTag("Star"));

	}

    public void CollectedStar()
    {
        collectedStars++;
        
        if (collectedStars == starList.Count)
        {
            FindObjectOfType<DoorScript>().OpenDoor();
        }
    }


}
