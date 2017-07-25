using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {

	[SerializeField]
	protected Transform muzzle;
	[SerializeField]
	protected Projectile porjectile;
	[SerializeField]
	protected float msBetweenShots = 100;
	[SerializeField]
	protected float muzzleVelocity = 35;

	protected float nextShotTime;

	public void Shoot () {

		if (Time.time > nextShotTime) {

			nextShotTime = Time.time + msBetweenShots / 1000;
			Projectile newProjectile = Instantiate (porjectile, muzzle.position, muzzle.rotation) as Projectile;
			newProjectile.SetSpeed (muzzleVelocity);
		}

	}
}
