using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class PlayerController : MonoBehaviour
{
	////////// Attributes
	# region movement
	[SerializeField] private float movementSpeed = 10.0f;
	[SerializeField] private float movementAcceleration = 10.0f;
	[SerializeField] private float movementDeceleration = 30.0f;
	[SerializeField] private float maxSpeed = 40.0f;
	[SerializeField] private float jumpForce = 10.0f;
	# endregion

	# region properties
	public ActivationKey activationKey;
	# endregion

	# region state
	[SerializeField] private bool isActive = false;
	[SerializeField] private bool isGrounded = false;
	private bool hasJumped = false;
	# endregion

	# region references
	private Rigidbody2D rb;
	private BoxCollider2D groundCollider;
	private Camera mainCamera;
	# endregion

	////////// Methods

	void Awake()
	{
		// get a reference to the Rigidbody component
        rb = GetComponent<Rigidbody2D>();
		if (rb == null)
			Debug.LogError("Rigidbody component not found on the player object");
		// get a reference to the ground collider
		groundCollider = transform.Find("Square").gameObject.GetComponent<BoxCollider2D>();
		if (groundCollider == null)
			Debug.LogError("Ground collider not found on the player object");
		// get a reference to the main camera
		mainCamera = Camera.main;
	}

    void Update()
    {
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
		else if (isGrounded)
		{
			ApplyFriction();
		}

		// Limit the speed of the player
		if (rb.velocity.magnitude > maxSpeed)
		{
			rb.velocity = rb.velocity.normalized * maxSpeed;
		}
	}

# region activation
	public ActivationKey GetActivationKey()
	{
		return activationKey;
	}

	public void setActivationKey(ActivationKey key)
	{
		activationKey = key;
	}

	public void SetActive()
	{
		if (!isActive)
		{
			isActive = true;
			// Make the 'active character pointer' visible
			transform.Find("Pointer").gameObject.GetComponent<SpriteRenderer>().enabled = true;
			// Set the camera target to the active character
			mainCamera.GetComponent<CameraController>().SetTarget(gameObject);
		}
	}

	public void SetNotActive()
	{
		if (isActive)
		{
			isActive = false;
			// Make the 'active character pointer' invisible
			transform.Find("Pointer").gameObject.GetComponent<SpriteRenderer>().enabled = false;
		}
	}

	public bool IsActive()
	{
		return isActive;
	}
# endregion

# region movement
	void CheckGrounded()
	{
		// Check if the player is grounded
		if (groundCollider.IsTouchingLayers(LayerMask.GetMask("Ground", "Thomas", "John", "Claire")))
		{
			// Debug.Log(gameObject.name + " is grounded because it is touching the ground");
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
		float targetSpeed = movementSpeed * movementInput;
		
		// Move the player
		float speedDifference = targetSpeed - rb.velocity.x;
		float acceleration = Math.Abs(targetSpeed) > 0.01f ? speedDifference * movementAcceleration : speedDifference * movementDeceleration;
		rb.AddForce(acceleration * Vector2.right, 0);
	}

	void ApplyFriction()
	{
		// Apply friction to the player
		rb.AddForce(-rb.velocity.x * movementAcceleration * Vector2.right, 0);
	}
# endregion

	public Color GetColor()
	{
		return transform.Find("Square").GetComponent<SpriteRenderer>().color;
	}

}
