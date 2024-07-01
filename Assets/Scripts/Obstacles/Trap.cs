using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
	private LevelScript levelScript;
    // Start is called before the first frame update
    void Start()
    {
		levelScript = GameObject.Find("LevelObject").GetComponent<LevelScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	void OnTriggerEnter2D(Collider2D other)
	{
		Debug.Log("Trap triggered");
		if (other.gameObject.tag == "Player")
		{
			// GAME OVER
			Debug.Log("Player hit a trap");
			levelScript.ResetLevel();
		}
	}
}
