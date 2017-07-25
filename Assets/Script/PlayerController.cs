using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
public class PlayerController : MonoBehaviour {

	protected Rigidbody rb;
	protected Vector3 velocity;

	void Start () {

		rb = GetComponent<Rigidbody> ();
	}
	

	public void Move (Vector3 velocity) {

		this.velocity = velocity;

	}

	protected void FixedUpdate () {

		rb.MovePosition (rb.position + velocity * Time.fixedDeltaTime);
	}

	public void LookAt (Vector3 lookPoint) {

		Vector3 heightCorrectedPoint = new Vector3 (lookPoint.x, transform.position.y, lookPoint.z);

		transform.LookAt (heightCorrectedPoint);

	}
}
