using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	////////// attributes

	# region references
	public GameObject target;
	# endregion

	# region camera movement
	private Vector3 targetPosition;
	[SerializeField] private float zOffset = -10f;
	[SerializeField] private float smoothTime = 1f;
	[SerializeField] private float maxVelocity = 1f;
	private Vector3 currentVelocity;
	# endregion

	////////// methods

    // Start is called before the first frame update
    void Start()
    {}

    // Update is called once per frame
    void LateUpdate()
    {
		if (target != null)
		{
			targetPosition = target.transform.position;
			targetPosition.z = zOffset;
			transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, smoothTime, maxVelocity);
		}
    }

	// Set the target of the camera
	public void SetTarget(GameObject newTarget)
	{
		target = newTarget;
	}
}
