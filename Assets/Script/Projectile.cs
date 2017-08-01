using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

	public LayerMask collisionMask;
	protected float speed = 10;
	protected float damage = 1;

	float lifetime = 3;
	float skinWidth = .1f;

	public void SetSpeed (float newSpeed) {
		speed = newSpeed;
	}

	void Start () {

		Destroy (gameObject, lifetime);

		Collider[] initialCollision = Physics.OverlapSphere (transform.position, .1f, collisionMask);
		if (initialCollision.Length > 0) {

			OnHitObject (initialCollision[0]);
		}
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

		if (Physics.Raycast (ray, out hit, moveDistance + skinWidth, collisionMask, QueryTriggerInteraction.Collide)) {
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

	void OnHitObject (Collider c) {

		IDamageable damageableObject = c.GetComponent<Collider>().GetComponent<IDamageable> ();
		if (damageableObject != null) {
			damageableObject.TakeDamage (damage);
		}
		GameObject.Destroy (gameObject);

	}
}
