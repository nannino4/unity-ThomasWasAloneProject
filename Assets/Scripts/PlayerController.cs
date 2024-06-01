using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Enum for the different KeyCode values for activation
public enum ActivationKey
{
	Fire1 = KeyCode.Alpha1,
	Fire2 = KeyCode.Alpha2,
	Fire3 = KeyCode.Alpha3,
}

public class PlayerController : MonoBehaviour
{
	// Public variables
	public float movementSpeed = 10.0f;
	public float maxSpeed = 10.0f;
	public float jumpForce = 10.0f;
	public ActivationKey activationKey = ActivationKey.Fire1;

	// Private variables
	[SerializeField] private bool isActive = false;
	[SerializeField] private bool isGrounded = false;
	private Rigidbody2D rb;
	private BoxCollider2D groundCollider;
	private bool hasJumped = false;

	// Start is called before the first frame update
	void Start()
    {
        rb = GetComponent<Rigidbody2D>();
		if (rb == null)
			Debug.LogError("Rigidbody component not found on the player object");
		// get a reference to the ground collider
		groundCollider = transform.Find("Square").gameObject.GetComponent<BoxCollider2D>();
		if (groundCollider == null)
			Debug.LogError("Ground collider not found on the player object");
		else if (groundCollider.isTrigger == false)
			Debug.LogError("Ground collider is not a trigger");
    }

    // Update is called once per frame
    void Update()
    {
		CheckActivation();
		// CheckGrounded();
		if (isActive)
			Jump();
    }

	void FixedUpdate()
	{
		CheckGrounded();
		if (isActive)
		{
			Move();
		}

		// Limit the speed of the player
		if (rb.velocity.magnitude > maxSpeed)
		{
			rb.velocity = rb.velocity.normalized * maxSpeed;
		}
	}

	void CheckActivation()
	{
		// loop through all the ActivationKey values
		foreach (ActivationKey key in System.Enum.GetValues(typeof(ActivationKey)))
		{
			// check if the key is pressed
			if (Input.GetKeyDown((KeyCode)key))
			{
				// check if the key is the same as the activationKey
				if (key == activationKey)
				{
					// the character is active
					if (!isActive)
					{
						isActive = true;
						// Make the 'active character pointer' visible
						transform.Find("Pointer").gameObject.GetComponent<SpriteRenderer>().enabled = true;
					}
				}
				else
				{
					// another character is active, and the current character is not active
					if (isActive)
					{
						isActive = false;
						// Make the 'active character pointer' invisible
						transform.Find("Pointer").gameObject.GetComponent<SpriteRenderer>().enabled = false;
					}
				}
			}
		}
	}

	void CheckGrounded()
	{
		// Check if the player is grounded
		if (groundCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
		{
			// Debug.Log(gameObject.name + " is grounded because it is touching the ground");
			isGrounded = true;
		}
		else if (groundCollider.IsTouchingLayers(LayerMask.GetMask("Player")))
		{
			// Debug.Log(gameObject.name + " is grounded because it is touching another player");
			isGrounded = true;
		}
		else
		{
			// Debug.Log(gameObject.name + " is not grounded");
			isGrounded = false;
		}
	}

	void Jump()
	{
		if (Input.GetButtonDown("Jump"))
		{
			if (isGrounded && !hasJumped)
			{
				rb.AddForce(UnityEngine.Vector2.up * jumpForce, ForceMode2D.Impulse);
				// isGrounded = false;
			}
			hasJumped = true;
		}
		else
			hasJumped = false;
	}

	void Move()
	{
		// Read movement input
		float movementInput = Input.GetAxisRaw("Horizontal");
		
		// Move the player
		// Vector3 movementVector = new Vector3(movementInput, rb.velocity.y, 0);
		// rb.velocity.Set(movementInput * movementSpeed, rb.velocity.y);
		// rb.MovePosition(transform.position + movementVector * movementSpeed * Time.deltaTime);
		transform.Translate(movementInput * movementSpeed * Time.fixedDeltaTime, 0, 0, Space.World);
	}

	public bool IsActive()
	{
		return isActive;
	}
}
