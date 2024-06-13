using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WorldBound : MonoBehaviour
{
	[SerializeField] private UnityEvent onPlayerExit;

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			Debug.Log("Player exited the world bounds!");
			onPlayerExit.Invoke();
		}
		Destroy(other.gameObject);
	}
}
