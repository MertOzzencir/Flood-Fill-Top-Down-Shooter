using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public Transform weaponHolder;
    Gun EquipGun;
    public Gun starterGun;



    private void Start()
    {
        if(starterGun !=null)
        {
            EquippedGun(starterGun);
        }
    }

    void EquippedGun(Gun gun)
    {
        if(EquipGun != null)
        {
            Destroy(EquipGun.gameObject);

        }
        EquipGun = Instantiate(gun,weaponHolder.position,weaponHolder.rotation) as Gun;
        EquipGun.transform.parent = weaponHolder;

    }
    public void Shoot()
    {

        if(EquipGun!=null)
        {
            EquipGun.Shoot();
        }
    }
}
