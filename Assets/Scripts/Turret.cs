using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Utils;

public class Turret : MonoBehaviour
{
	////////// private attributes

	# region turret state
	[SerializeField] private WorldObjectType turretType = WorldObjectType.White;
	[SerializeField] private bool isActive = true;
	# endregion

	# region turret shooting
	private List<GameObject> playersInRange = new List<GameObject>();
	[SerializeField] private GameObject bulletPrefab; // Reference to the bullet prefab
	[SerializeField] private float fireRate = 1.0f; // Time between bullet spawns (in seconds)
	[SerializeField] private float bulletSpeed = 10.0f; // Speed of the bullet
	[SerializeField] private float bulletStartRange = 1.0f; // Distance from the turret where the bullet will spawn
	private float timeSinceLastShot = 0.0f;
	# endregion

	////////// methods

	// Start is called before the first frame update
	void Start()
	{
		switch (turretType)
		{
			case WorldObjectType.Thomas:
				ChangeTypeThomas();
				break;
			case WorldObjectType.John:
				ChangeTypeJohn();
				break;
			case WorldObjectType.Claire:
				ChangeTypeClaire();
				break;
			case WorldObjectType.White:
				ChangeTypeWhite();
				break;
			case WorldObjectType.Inactive:
				ChangeTypeInactive();
				break;
		}
	}

	void Update()
	{
		timeSinceLastShot += Time.deltaTime;
		if (isActive)
		{
			// Rotate towards the player
			if (playersInRange.Count > 0)
			{
				LookAtCloserPlayer();
				if (timeSinceLastShot >= fireRate)
				{
					FireBullet();
					timeSinceLastShot = 0.0f;
				}
			}
			else
			{
				// default rotation
				transform.rotation = UnityEngine.Quaternion.Euler(0, 0, 0);
			}
		}
	}

	# region turret type
	void ChangeTypeThomas()
	{
		turretType = WorldObjectType.Thomas;
		// Set the color of the turret
		transform.Find("Sprite").GetComponent<SpriteRenderer>().color = CharacterColor.ThomasColor;
		isActive = true;
	}

	void ChangeTypeJohn()
	{
		turretType = WorldObjectType.John;
		// Set the color of the turret
		transform.Find("Sprite").GetComponent<SpriteRenderer>().color = CharacterColor.JohnColor;
		isActive = true;
	}

	void ChangeTypeClaire()
	{
		turretType = WorldObjectType.Claire;
		// Set the color of the turret
		transform.Find("Sprite").GetComponent<SpriteRenderer>().color = CharacterColor.ClaireColor;
		isActive = true;
	}

	void ChangeTypeWhite()
	{
		turretType = WorldObjectType.White;
		// Set the color of the turret
		transform.Find("Sprite").GetComponent<SpriteRenderer>().color = Color.white;
		isActive = true;
	}

	void ChangeTypeInactive()
	{
		turretType = WorldObjectType.Inactive;
		// Set the color of the turret
		transform.Find("Sprite").GetComponent<SpriteRenderer>().color = Color.clear;
		isActive = false;
	}
	# endregion

	# region turret shooting
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player")
		{
			// add to the list of players in range
			playersInRange.Add(other.gameObject);
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player")
		{
			// remove from the list of players in range
			playersInRange.Remove(other.gameObject);
		}
	}

	void LookAtCloserPlayer()
	{
		// Find the closest player
		GameObject closestPlayer = playersInRange[0];
		float closestDistance = UnityEngine.Vector3.Distance(transform.position, closestPlayer.transform.position);
		foreach (GameObject player in playersInRange)
		{
			float distance = UnityEngine.Vector3.Distance(transform.position, player.transform.position);
			if (distance < closestDistance)
			{
				closestPlayer = player;
				closestDistance = distance;
			}
		}
		// Rotate towards the closest player
		UnityEngine.Vector3 direction = (closestPlayer.transform.position - transform.position).normalized;
		float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
		transform.rotation = UnityEngine.Quaternion.Euler(0, 0, angle);
	}

	void FireBullet()
	{
		// Instantiate the bullet prefab
		GameObject bullet = Instantiate(bulletPrefab, transform.position + transform.up * bulletStartRange, transform.rotation);
		// Set the bullet type
		bullet.transform.Find("Sprite").GetComponent<Bullet>().setBulletType(turretType);

		// Add initial impulse to the bullet
		if (bullet.GetComponent<Rigidbody2D>() != null)
		{
			bullet.GetComponent<Rigidbody2D>().AddForce(transform.up * bulletSpeed, ForceMode2D.Impulse);
		}
	}
	# endregion
}
