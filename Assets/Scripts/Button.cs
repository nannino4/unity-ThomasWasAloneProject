using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Button : MonoBehaviour
{
	public UnityEvent onButtonPressed;
	public UnityEvent onButtonReleased;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player")
		{
			// Debug.Log("Button pressed");
			// scale object to indicate button press
			transform.localScale.Set(1f, 0.1f, 1f);
			// call the onButtonPressed event
			onButtonPressed.Invoke();
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player")
		{
			// Debug.Log("Button released");
			transform.localScale.Set(1f, 1f, 1f);
			onButtonReleased.Invoke();
		}
	}
}
