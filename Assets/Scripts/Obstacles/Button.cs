using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;
using Utils;

public class Button : MonoBehaviour
{
	////////// properties
	# region button events
	public UnityEvent onButtonPressed;
	public UnityEvent onButtonReleased;
	# endregion

	# region button properties
	[SerializeField] private bool isToggle = false;
	[SerializeField] private WorldObjectType buttonType = WorldObjectType.White;
	# endregion

	# region button state
	private bool isPressed = false;
	private bool isActive = true;
	# endregion

	////////// methods

    // Start is called before the first frame update
    void Start()
    {
		# region button type
		switch (buttonType)
		{
			case WorldObjectType.White:
				transform.Find("Sprite").GetComponent<SpriteRenderer>().color = Color.white;
				isActive = true;
				break;
			case WorldObjectType.Thomas:
				transform.Find("Sprite").GetComponent<SpriteRenderer>().color = CharacterColor.ThomasColor;
				isActive = true;
				break;
			case WorldObjectType.Claire:
				transform.Find("Sprite").GetComponent<SpriteRenderer>().color = CharacterColor.ClaireColor;
				isActive = true;
				break;
			case WorldObjectType.John:
				transform.Find("Sprite").GetComponent<SpriteRenderer>().color = CharacterColor.JohnColor;
				isActive = true;
				break;
			case WorldObjectType.Inactive:
				transform.Find("Sprite").GetComponent<SpriteRenderer>().color = Color.gray;
				isActive = false;
				break;
		}
		#endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	void OnTriggerEnter2D(Collider2D other)
	{
		if (!isActive)
		{
			return;
		}
		if (isToggle || (!isToggle && !isPressed))
		{
			if (other.gameObject.tag == "Player" && (buttonType == WorldObjectType.White || other.gameObject.name == buttonType.ToString()))
			{
				isPressed = true;
				Debug.Log("Button pressed");
				// scale object to indicate button press
				transform.localScale = new Vector3(1.0f, 0.3f, 1.0f);
				// call the onButtonPressed event
				onButtonPressed.Invoke();
			}
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (!isActive)
		{
			return;
		}
		if (isToggle)
		{
			if (other.gameObject.tag == "Player" && (buttonType == WorldObjectType.White || other.gameObject.name == buttonType.ToString()))
			{
				Debug.Log("Button released");
				transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
				onButtonReleased.Invoke();
			}
		}
	}
}
