using ODT.MaG.Gun;
using UnityEngine;

namespace ODT.MaG.Player
{
    public class PlayerWeaponBehaviour : MonoBehaviour
    {
        [SerializeField]
        private Transform playerArm;
        [SerializeField]
        private GameObject InitGunPrefab;

        private PlayerGunBehaviour currentWeapon;

        private void OnEnable()
        {
            GameObject gun = Instantiate(InitGunPrefab);
            currentWeapon = gun.GetComponent<PlayerGunBehaviour>();

            currentWeapon.SetPlayerArm(playerArm);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Weapon")
            {
                SetNewWeapon(other.GetComponent<PlayerGunBehaviour>());
            }
        }

        private void SetNewWeapon(PlayerGunBehaviour newWeapon)
        {
            currentWeapon.Drop();
            currentWeapon = newWeapon;
            currentWeapon.SetPlayerArm(playerArm);
        }

    }
}
