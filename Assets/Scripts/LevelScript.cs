using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utils;

public class LevelScript : MonoBehaviour
{
	////////// attributes

	# region references
	private PlayerController[] characters;
	private Goal[] goals;
	# endregion

	private ActivationKey[] activationKeys;

	// [SerializeField] private ActivationKey[] activationKeys;

	////////// methods

	void Awake()
	{
		// fetch the characters
		characters = GameObject.FindGameObjectsWithTag("Player").Select(x => x.GetComponent<PlayerController>()).ToArray();
		if (characters.Length == 0)
		{
			Debug.LogError("LevelScript: No players assigned!");
		}

		// set the activation keys for the characters
		for (int i = 0; i < characters.Length; i++)
		{
			characters[i].setActivationKey(ActivationKey.Fire1 + i);
		}
		activationKeys = characters.Select(x => x.GetActivationKey()).ToArray();

		// fetch the goals for every character
		goals = GameObject.FindGameObjectsWithTag("Finish").Select(x => x.GetComponent<Goal>()).ToArray();
		if (goals.Length != characters.Length)
		{
			Debug.LogError("LevelScript: #goals != #characters");
		}
	}

    void Start()
    {
		characters[0].SetActive();
    }

    void Update()
    {
		// check if the level should be reset
        if (Input.GetKeyDown(KeyCode.R) || Input.GetKeyDown(KeyCode.Backspace))
			ResetLevel();

		// check character activation
		CheckCharacterActivation();

		// check if all goals are reached
		if (CheckGoals())
		{
			Debug.Log("All goals reached!");
			LoadNextLevel();
		}
	}

	void CheckCharacterActivation()
	{
		foreach(ActivationKey key in activationKeys)
		{
			// check if an activation key has been pressed
			if (Input.GetKeyDown((KeyCode)key))
			{
				// loop through all the characters
				foreach (PlayerController character in characters)
				{
					if (key == character.GetActivationKey())
					{
						// set the character active
						character.SetActive();
					}
					else
					{
						// set the character inactive
						character.SetNotActive();
					}
				}
				return ;
			}
		}
	}

	bool CheckGoals()
	{
		foreach (Goal goal in goals)
		{
			if (!goal.IsGoalReached())
			{
				return false;
			}
		}
		return true;
	}

	# region scene management
	public void ResetLevel()
	{
		Debug.Log("Resetting level");
		UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
	}

	void LoadNextLevel()
	{
		//TODO
		Debug.Log("Loading next level");
		// UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex + 1);
	}
	# endregion

	public PlayerController[] GetCharacters()
	{
		return characters;
	}

}

