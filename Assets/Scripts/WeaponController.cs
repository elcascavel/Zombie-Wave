using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{

    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform bulletSpawnPoint;

    [SerializeField] int magazineCapacity = 30;
    int bulletsInMagazine;

    [SerializeField] float reloadTime = 2f;

    [SerializeField] float fireRateDelay = 0.1f;

    bool canShoot = true;

    bool hasAmmo = true;

    [SerializeField] bool automaticReload = false;

    [SerializeField] bool automaticWeapon = false;


    void Start()
    {
        bulletsInMagazine = magazineCapacity;
    }


    void Update()
    {
        if(Input.GetMouseButton(0) && automaticWeapon)
        {
            if(hasAmmo)
            {
                if(canShoot)
                {
                    Shoot();
                    Debug.Log("Magazine: " + bulletsInMagazine);
                }
            }
           
        } else if(Input.GetMouseButtonDown(0) && !automaticWeapon)
            {
                if(hasAmmo)
                {
                    if(canShoot)
                    {
                        Shoot();
                    }
                }
            }
        
        if(Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }
        
    }

    void Shoot()
    {
        StartCoroutine(FireRateDelay());
        Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        bulletsInMagazine--;
        if(bulletsInMagazine <= 0)
        {
            hasAmmo = false;
            if(automaticReload)
            {
                Reload();
            }
        }
    }

    void Reload()
    {
        StartCoroutine(ReloadDelay());
    }

    IEnumerator FireRateDelay()
    {
        canShoot = false;
        yield return new WaitForSeconds(fireRateDelay);
        canShoot = true;
    }

    IEnumerator ReloadDelay()
    {
        canShoot = false;
        yield return new WaitForSeconds(reloadTime);
        bulletsInMagazine = magazineCapacity;
        hasAmmo = true;
        canShoot = true;
    }
}
