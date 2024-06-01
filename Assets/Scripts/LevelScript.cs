using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelScript : MonoBehaviour
{
	public Goal[] goals;
    // Start is called before the first frame update
    void Start()
    {
		if (goals.Length == 0)
		{
			Debug.LogError("LevelScript: No goals assigned!");
		}
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) || Input.GetKeyDown(KeyCode.Backspace))
		{
			ResetLevel();
		}
		if (CheckGoals())
		{
			Debug.Log("All goals reached!");
			LoadNextLevel();
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

	public void ResetLevel()
	{
		Debug.Log("Resetting level");
		UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
	}

	public void LoadNextLevel()
	{
		//TODO
		Debug.Log("Loading next level");
		// UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex + 1);
	}
}
