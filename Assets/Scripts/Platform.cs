using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        checkPlatformType();
    }

	private void checkPlatformType()
	{
		switch (platformType)
		{
			case PlatformType.White:
				boxCollider.excludeLayers = LayerMask.GetMask();
				spriteRenderer.enabled = true;
				spriteRenderer.color = Color.white;
				break;
			case PlatformType.Thomas:
				boxCollider.excludeLayers = LayerMask.GetMask("Claire", "John");
				spriteRenderer.enabled = true;
				spriteRenderer.color = Color.red;
				break;
			case PlatformType.John:
				boxCollider.excludeLayers = LayerMask.GetMask("Claire", "Thomas");
				spriteRenderer.enabled = true;
				spriteRenderer.color = Color.yellow;
				break;
			case PlatformType.Claire:
				boxCollider.excludeLayers = LayerMask.GetMask("John", "Thomas");
				spriteRenderer.enabled = true;
				spriteRenderer.color = Color.blue;
				break;
			case PlatformType.Inactive:
				boxCollider.excludeLayers = LayerMask.GetMask("Claire", "John", "Thomas");
				spriteRenderer.enabled = false;
				break;
		}
	}
}
