using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class Platform : MonoBehaviour
{
	private enum PlatformType
	{
		Inactive,
		White,
		Thomas,
		John,
		Claire
	}
	private BoxCollider2D boxCollider;
	private SpriteRenderer spriteRenderer;
	[SerializeField] private PlatformType platformType = PlatformType.White;
	[SerializeField] private Vector3 initialPosition;
	[SerializeField] private Vector3 deltaPosition = new Vector3(0.0f, 0.0f, 0.0f);
	[SerializeField] private float timeElapsed = 0.0f;
	[SerializeField] private float timePeriod = 1.0f;
	[SerializeField] private bool isMoving = false;

    // Start is called before the first frame update
    void Start()
    {
		if ((boxCollider = GetComponent<BoxCollider2D>()) == null)
			Debug.LogError("BoxCollider2D component not found on the platform object");
		if ((spriteRenderer = GetComponent<SpriteRenderer>()) == null)
			Debug.LogError("SpriteRenderer component not found on the platform object");
		// Set the initial position of the platform
		initialPosition = transform.position;
		switch (platformType)
		{
			case PlatformType.Thomas:
				ChangeTypeThomas();
				break;
			case PlatformType.John:
				ChangeTypeJohn();
				break;
			case PlatformType.Claire:
				ChangeTypeClaire();
				break;
			case PlatformType.White:
				ChangeTypeWhite();
				break;
			case PlatformType.Inactive:
				ChangeTypeInactive();
				break;
		}
    }

    // Update is called once per frame
    void Update()
    {
		if (Input.GetKeyDown(KeyCode.Alpha7))
			ChangeTypeThomas();
		else if (Input.GetKeyDown(KeyCode.Alpha8))
			ChangeTypeJohn();
		else if (Input.GetKeyDown(KeyCode.Alpha9))
			ChangeTypeClaire();
		else if (Input.GetKeyDown(KeyCode.Alpha0))
			ChangeTypeWhite();
		else if (Input.GetKeyDown(KeyCode.Minus))
			ChangeTypeInactive();
		if (isMoving)
		{
			timeElapsed += Time.deltaTime;
			transform.position = initialPosition + deltaPosition * (Mathf.Sin(2 * Mathf.PI * (timeElapsed - timePeriod / 4f) / timePeriod) * 0.5f + 0.5f);
		}
    }

	public void StartMoving()
	{
		isMoving = true;
	}

	// private void changeType(PlatformType platformType, Color color, LayerMask layersToExclude)
	// {
	// 	this.platformType = platformType;
	// 	spriteRenderer.color = color;
	// 	boxCollider.excludeLayers = layersToExclude;
	// }

	public void ChangeTypeThomas()
	{
		platformType = PlatformType.Thomas;
		spriteRenderer.color = CharacterColor.ThomasColor;
		boxCollider.excludeLayers = LayerMask.GetMask("John", "Claire");
	}

	public void ChangeTypeJohn()
	{
		platformType = PlatformType.John;
		spriteRenderer.color = CharacterColor.JohnColor;
		boxCollider.excludeLayers = LayerMask.GetMask("Thomas", "Claire");
	}

	public void ChangeTypeClaire()
	{
		platformType = PlatformType.Claire;
		spriteRenderer.color = CharacterColor.ClaireColor;
		boxCollider.excludeLayers = LayerMask.GetMask("Thomas", "John");
	}

	public void ChangeTypeWhite()
	{
		platformType = PlatformType.White;
		spriteRenderer.color = Color.white;
		boxCollider.excludeLayers = LayerMask.GetMask();
	}

	public void ChangeTypeInactive()
	{
		platformType = PlatformType.Inactive;
		spriteRenderer.color = Color.clear;
		boxCollider.excludeLayers = LayerMask.GetMask("Thomas", "John", "Claire");
	}
}
