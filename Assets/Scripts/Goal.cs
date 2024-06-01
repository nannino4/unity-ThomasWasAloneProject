using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Goal : MonoBehaviour
{
	public string characterName = "Claire";
	[SerializeField] private bool isGoalReached = false;

	private BoxCollider2D goalCollider;
    // Start is called before the first frame update
    void Start()
    {
        goalCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.transform.parent.name == characterName)
		{
			// Debug.Log(characterName + " has reached the goal!");
			isGoalReached = true;
		}
		else
		{
			// Debug.Log("This character: '" + other.transform.parent.name + "' has reached the goal but it's not the right character!");
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.transform.parent.name == characterName)
		{
			// Debug.Log(characterName + " has left the goal!");
			isGoalReached = false;
		}
		else
		{
			// Debug.Log("This character: '" + other.transform.parent.name + "' has left the goal but it's not the right character!");
		}
	}

	public bool IsGoalReached()
	{
		return isGoalReached;
	}
}
