using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
	public GameObject bulletPrefab; // Reference to the bullet prefab
	[SerializeField] private float fireRate = 1.0f; // Time between bullet spawns (in seconds)
	[SerializeField] private float bulletSpeed = 10.0f; // Speed of the bullet
	[SerializeField] private float bulletStartRange = 1.0f; // Distance from the turret where the bullet will spawn

	private float timeSinceLastShot = 0.0f;

	void Update()
	{
		timeSinceLastShot += Time.deltaTime;
	}

	void FireBullet()
	{
		// Instantiate the bullet prefab
		GameObject bullet = Instantiate(bulletPrefab, transform.position + transform.up * bulletStartRange, transform.rotation);

		// Optionally add initial force or velocity to the bullet
		if (bullet.GetComponent<Rigidbody2D>() != null)
		{
			bullet.GetComponent<Rigidbody2D>().AddForce(transform.up * bulletSpeed, ForceMode2D.Impulse);
		}
	}

	void OnTriggerStay2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player")
		{
			// Rotate towards the player
			Vector3 direction = (other.transform.position - transform.position).normalized;
			float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
			transform.rotation = Quaternion.Euler(0, 0, angle);
			// Fire bullet
			if (timeSinceLastShot >= fireRate)
			{
				FireBullet();
				timeSinceLastShot = 0.0f;
			}

		}
	}
}
