using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WorldBound : MonoBehaviour
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
		if (other.gameObject.CompareTag("Player"))
		{
			Debug.Log("Player exited the world bounds!");
			levelScript.ResetLevel();
		}
		Destroy(other.gameObject);
	}
}
