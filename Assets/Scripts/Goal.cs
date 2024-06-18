using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using Utils;


public class Goal : MonoBehaviour
{
	public string characterName = "Claire";
	private Color characterColor;
	[SerializeField] private bool isGoalReached = false;

	private BoxCollider2D goalCollider;
    // Start is called before the first frame update
    void Start()
    {
        goalCollider = GetComponent<BoxCollider2D>();

		if (characterName == "Claire")
		{
			characterColor = CharacterColor.ClaireColor;
		}
		else if (characterName == "John")
		{
			characterColor = CharacterColor.JohnColor;
		}
		else if (characterName == "Thomas")
		{
			characterColor = CharacterColor.ThomasColor;
		}
		else
		{
			Debug.LogError("Character name is not valid!");
		}
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
			other.transform.parent.Find("Square").GetComponent<SpriteRenderer>().color = Color.white;
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
			other.transform.parent.Find("Square").GetComponent<SpriteRenderer>().color = characterColor;
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
