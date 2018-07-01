using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent (typeof (NavMeshAgent))]
public class Enemy : LivingEntity {

	enum State {

		Ilde, 
		Chasing,
		Attacking

	};

	State currentState;

	NavMeshAgent pathfinder;
	Transform target;
	LivingEntity targetEntity;
	Material shinMaterial;

	Color originalColour;

	float attackDistanceThreshold = .5f;
	float timeBetweenAttacks = 1;
	float damage = 1;

	float nextAttackTime;

	float collisionRadius;
	float targetCollisionRadius;

	bool hasTarget;

	protected override void Start () {
		base.Start ();
		pathfinder = GetComponent<NavMeshAgent> ();
		shinMaterial = GetComponent<Renderer> ().material;

		originalColour = shinMaterial.color;

		if (GameObject.FindGameObjectWithTag ("Player") != null) {
			currentState = State.Chasing;
			hasTarget = true;

			target = GameObject.FindGameObjectWithTag ("Player").transform;
			targetEntity = target.GetComponent<LivingEntity> ();
			targetEntity.OnDeath += OnTargetDeath;
		

			collisionRadius = GetComponent<CapsuleCollider> ().radius;
			targetCollisionRadius = target.GetComponent<CapsuleCollider> ().radius;

			StartCoroutine (UpdatePath ());
		}
	}

	void Update () {

		if (hasTarget) {
			if (Time.time > nextAttackTime) {
				float sqrDstToTarget = (target.position - transform.position).sqrMagnitude;

				if (sqrDstToTarget < Mathf.Pow (attackDistanceThreshold + collisionRadius + targetCollisionRadius, 2)) {
					nextAttackTime = Time.time + timeBetweenAttacks;
					StartCoroutine (Attack ());
				}
			}
		}
	}

	void OnTargetDeath () {

		hasTarget = false;
		currentState = State.Ilde;
	}

	IEnumerator Attack () {
		currentState = State.Attacking;
		pathfinder.enabled = false;

		Vector3 originalPosition = transform.position;
		Vector3 dirToTarget = (target.position - transform.position).normalized;
		Vector3 attackPosition = target.position - dirToTarget * (collisionRadius);

		float attackSpeed = 3;
		float percent = 0;

		shinMaterial.color = Color.red;
		bool hasAppliedDamage = false;

		while (percent <= 1) {

			if (percent >= .5f && !hasAppliedDamage) {
				hasAppliedDamage = true;
				targetEntity.TakeDamage (damage);
			}
				
			percent += Time.deltaTime * attackSpeed;

			float interpolation = (-Mathf.Pow (percent, 2) + percent) * 4;
			transform.position = Vector3.Lerp (originalPosition, attackPosition, interpolation);

			yield return null;
		}

		shinMaterial.color = originalColour;
		currentState = State.Chasing;
		pathfinder.enabled = true;
	}

	IEnumerator UpdatePath () {

		float refreshRate = .25f;

		while (hasTarget) {
			if (currentState == State.Chasing) {
				Vector3 dirToTarget = (target.position - transform.position).normalized;
				Vector3 targetPosition = target.position - dirToTarget * (collisionRadius + targetCollisionRadius + (targetCollisionRadius/2));
				if (!dead) {
					pathfinder.SetDestination (targetPosition);
				}
			}
			yield return new WaitForSeconds (refreshRate);
		}

	}

}
