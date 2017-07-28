using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

	public LayerMask collisionMask;
	protected float speed = 10;
	protected float damage = 1;

	public void SetSpeed (float newSpeed) {
		speed = newSpeed;
	}


	protected void Update () {
		float moveDistance = speed * Time.deltaTime;
		CheckCollisions (moveDistance);
		transform.Translate (Vector3.forward * Time.deltaTime * speed);

	}

	/// <summary>
	/// Q: Why use raycasts instead of OnTriggerEnter to detect projectile collisions?
	/// A: At very high projectile speeds OnTriggerEnter might not be called (since the 
	/// projectile would be in front of enemy one frame, and through it the next). Raycasting 
	/// just makes sure that collisions will work no matter the projectile speed.
	/// </summary>
	/// <param name="moveDistance">Move distance.</param>
	void CheckCollisions (float moveDistance) {

		Ray ray = new Ray (transform.position, transform.forward);
		RaycastHit hit;

		if (Physics.Raycast (ray, out hit, moveDistance, collisionMask, QueryTriggerInteraction.Collide)) {
			OnHitObject (hit);
		}

	}

	void OnHitObject (RaycastHit hit) {

		IDamageable damageableObject = hit.collider.GetComponent<IDamageable> ();
		if (damageableObject != null) {
			damageableObject.TakeHit (damage, hit);
		}
		GameObject.Destroy (gameObject);

	}
}
