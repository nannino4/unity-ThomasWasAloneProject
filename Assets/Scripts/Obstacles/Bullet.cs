using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class Bullet : MonoBehaviour
{
	////////// attributes
	# region references
	private LevelScript levelScript;
	# endregion

	# region bullet properties
	[SerializeField] float maxLifeTime = 5.0f;
	# endregion

	# region bullet state
	[SerializeField] private WorldObjectType bulletType;
	private float lifeTime = 0.0f;
	# endregion

	////////// methods

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
			Destroy(transform.parent.gameObject);
    }

	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.tag == "Player" && (other.gameObject.name == bulletType.ToString() || bulletType == WorldObjectType.White))
		{
			Debug.Log("Player hit by bullet! GAME OVER!");
			levelScript.ResetLevel();
		}
		Destroy(transform.parent.gameObject);
	}

	# region bullet type
	public void setBulletType(WorldObjectType inType)
	{
		bulletType = inType;
	}
	# endregion
}
