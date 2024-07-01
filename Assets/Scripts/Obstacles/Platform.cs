using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class Platform : MonoBehaviour
{
	////////// private attributes
	# region references
	private BoxCollider2D boxCollider;
	private SpriteRenderer spriteRenderer;
	# endregion

	# region platform type
	[SerializeField] private WorldObjectType platformType = WorldObjectType.White;
	# endregion

	# region platform movement
	[SerializeField] private Vector3 initialPosition;
	[SerializeField] private Vector3 deltaPosition = new Vector3(0.0f, 0.0f, 0.0f);
	[SerializeField] private float timeElapsed = 0.0f;
	[SerializeField] private float timePeriod = 1.0f;
	[SerializeField] private bool isMoving = false;
	# endregion

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

	public void StopMoving()
	{
		isMoving = false;
	}

	// private void changeType(PlatformType platformType, Color color, LayerMask layersToExclude)
	// {
	// 	this.platformType = platformType;
	// 	spriteRenderer.color = color;
	// 	boxCollider.excludeLayers = layersToExclude;
	// }

	public void ChangeTypeThomas()
	{
		platformType = WorldObjectType.Thomas;
		spriteRenderer.color = CharacterColor.ThomasColor;
		boxCollider.excludeLayers = LayerMask.GetMask("John", "Claire");
	}

	public void ChangeTypeJohn()
	{
		platformType = WorldObjectType.John;
		spriteRenderer.color = CharacterColor.JohnColor;
		boxCollider.excludeLayers = LayerMask.GetMask("Thomas", "Claire");
	}

	public void ChangeTypeClaire()
	{
		platformType = WorldObjectType.Claire;
		spriteRenderer.color = CharacterColor.ClaireColor;
		boxCollider.excludeLayers = LayerMask.GetMask("Thomas", "John");
	}

	public void ChangeTypeWhite()
	{
		platformType = WorldObjectType.White;
		spriteRenderer.color = Color.white;
		boxCollider.excludeLayers = LayerMask.GetMask();
	}

	public void ChangeTypeInactive()
	{
		platformType = WorldObjectType.Inactive;
		spriteRenderer.color = Color.clear;
		boxCollider.excludeLayers = LayerMask.GetMask("Thomas", "John", "Claire");
	}
}
