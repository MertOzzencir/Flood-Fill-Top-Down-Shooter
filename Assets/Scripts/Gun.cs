using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{

    public Transform bulletHolder;

    public Projectile projectile;
    public float projectileSpeed = 35f;
    float nextShootTime;
    public float msBetweenShoots = 100;


    public void Shoot()
    {

        if(Time.time > nextShootTime)
        {
            nextShootTime = Time.time + msBetweenShoots/1000;
            Projectile bullet = Instantiate(projectile, bulletHolder.position, bulletHolder.rotation) as Projectile;
            projectile.SetSpeed(projectileSpeed);
        }
       


    }
}
