using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour {

	public float walkspeed = 2;
	public float runspeed = 6;

	public float turnSmoothTime = 0.2f;
	float turnSmoothVelocity;

	public float speedSmoothTime = 0.1f;
	float speedSmoothVelocity;
	float currentSpeed;
	Vector3 lastPosition;

	Animator animator;

	IEnumerator Start() {
		animator = GetComponent<Animator> ();
		lastPosition = transform.position;

		while(true) {
			yield return new WaitForSeconds(0.3f);
			setAnimation();
		}
	}

	void setAnimation () {
		if (transform.position == lastPosition) {
			this.idle ();
		} else {
			this.moving ();
		}
		lastPosition = transform.position;
	}

	public void idle () {
		animator.SetFloat ("SpeedPercent", 0);
	}

	public void moving () {
		animator.SetFloat ("SpeedPercent", 1);
	}
}
