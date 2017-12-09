using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float walkspeed = 2;
	public float runspeed = 6;

	public float turnSmoothTime = 0.2f;
	float turnSmoothVelocity;

	public float speedSmoothTime = 0.1f;
	float speedSmoothVelocity;
	float currentSpeed;

	Animator animator; 

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 input = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));
		Vector2 inputDirection = input.normalized;

		if (inputDirection != Vector2.zero) {
			float targetRotation = Mathf.Atan2 (inputDirection.x, inputDirection.y) * Mathf.Rad2Deg;
			transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle (transform.eulerAngles.y, 
									targetRotation, ref turnSmoothVelocity, turnSmoothTime);
		}

		bool running = Input.GetKey (KeyCode.LeftShift);
		float speed = ((running) ? runspeed : walkspeed) * inputDirection.magnitude;

		transform.Translate (transform.forward * speed * Time.deltaTime, Space.World);

		float animationSpeedPercent = ((running) ? 1 : .5f) * inputDirection.magnitude;
		animator.SetFloat ("SpeedPercent", animationSpeedPercent);
	}
}
