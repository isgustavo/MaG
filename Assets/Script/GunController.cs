using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour {

	[SerializeField]
	protected Transform weaponHold;
	[SerializeField]
	protected Gun startingGun;

	protected Gun equippedGun;

	protected void Start () {

		if (startingGun != null) {
			EquipGun (startingGun);
		}

	}

	public void EquipGun (Gun gunToEquip) {

		if (equippedGun != null) {

			Destroy (equippedGun.gameObject);
		}

		equippedGun = Instantiate (gunToEquip, weaponHold.position, weaponHold.rotation) as Gun;
		equippedGun.transform.SetParent (weaponHold.transform);

	}

	public void Shoot () {

		if (equippedGun != null) {

			equippedGun.Shoot ();
		}
	}
}
