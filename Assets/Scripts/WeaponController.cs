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

    [SerializeField] private Recoil recoil;

    void Start()
    {
        bulletsInMagazine = magazineCapacity;
    }

    public void Shoot()
    {
        if (hasAmmo)
        {
            if (canShoot)
            {
                StartCoroutine(FireRateDelay());
                Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
                bulletsInMagazine--;
                recoil.RecoilFire();
                if (bulletsInMagazine <= 0)
                {
                    hasAmmo = false;
                    if (automaticReload)
                    {
                        Reload();
                    }
                }
                Debug.Log("Magazine: " + bulletsInMagazine);
            }
        }

    }

    public void Reload()
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
