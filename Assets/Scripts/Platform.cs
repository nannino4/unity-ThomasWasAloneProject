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
	// variable with the color 424BADFF
	private BoxCollider2D boxCollider;
	private SpriteRenderer spriteRenderer;
	[SerializeField] private PlatformType platformType = PlatformType.White;

    // Start is called before the first frame update
    void Start()
    {
		if ((boxCollider = GetComponent<BoxCollider2D>()) == null)
			Debug.LogError("BoxCollider2D component not found on the platform object");
		if ((spriteRenderer = GetComponent<SpriteRenderer>()) == null)
			Debug.LogError("SpriteRenderer component not found on the platform object");
    }

    // Update is called once per frame
    void Update()
    {
		if (Input.GetKeyDown(KeyCode.Alpha7))
			changeType(PlatformType.Thomas, CharacterColor.ThomasColor, LayerMask.GetMask("John", "Claire"));
		else if (Input.GetKeyDown(KeyCode.Alpha8))
			changeType(PlatformType.John, CharacterColor.JohnColor, LayerMask.GetMask("Thomas", "Claire"));
		else if (Input.GetKeyDown(KeyCode.Alpha9))
			changeType(PlatformType.Claire, CharacterColor.ClaireColor, LayerMask.GetMask("Thomas", "John"));
		else if (Input.GetKeyDown(KeyCode.Alpha0))
			changeType(PlatformType.White, Color.white, LayerMask.GetMask());
		else if (Input.GetKeyDown(KeyCode.Minus))
			changeType(PlatformType.Inactive, Color.clear, LayerMask.GetMask("Thomas", "John", "Claire"));
    }

	private void changeType(PlatformType platformType, Color color, LayerMask layersToExclude)
	{
		this.platformType = platformType;
		spriteRenderer.color = color;
		boxCollider.excludeLayers = layersToExclude;
	}
}
