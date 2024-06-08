using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	private LevelScript levelScript;
	[SerializeField] float maxLifeTime = 5.0f;
	private float lifeTime = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        levelScript = GameObject.Find("LevelObject").GetComponent<LevelScript>();
    }

    // Update is called once per frame
    void Update()
    {
        lifeTime += Time.deltaTime;
		if (lifeTime >= maxLifeTime)
			Destroy(gameObject);
    }

	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.tag == "Player")
		{
			Debug.Log("Player hit by bullet! GAME OVER!");
			levelScript.ResetLevel();
		}
		Destroy(gameObject);
	}
}
