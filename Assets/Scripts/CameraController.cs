using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	// Public variables
	public GameObject[] players;

	// Private variables
	private GameObject activePlayer;

    // Start is called before the first frame update
    void Start()
    {
        if (players.Length == 0)
			Debug.LogError("No players assigned to the camera controller");
    }

    // Update is called once per frame
    void Update()
    {
		activePlayer = null;
        foreach (GameObject player in players)
		{
			if (player.GetComponent<PlayerController>().IsActive())
			{
				activePlayer = player;
				break;
			}
		}

		if (activePlayer != null)
		{
			transform.position = new Vector3(activePlayer.transform.position.x, activePlayer.transform.position.y, transform.position.z);
		}
    }
}
