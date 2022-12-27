using System.Collections;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField]
    private bool addBulletSpread = true;
    [SerializeField]
    private Vector3 bulletSpreadVariance = new Vector3(0.1f, 0.1f, 0.1f);
    [SerializeField]
    private ParticleSystem impactParticleSystem;
    [SerializeField] Transform bulletSpawnPoint;
    [SerializeField] float reloadTime = 2f;
    [SerializeField] float fireRateDelay = 0.5f;
    [SerializeField] bool automaticWeapon = false;
    [SerializeField] private ParticleSystem shootingSystem;
    [SerializeField] private TrailRenderer bulletTrail;
    [SerializeField] private LayerMask Mask;

    [SerializeField]
    private float bulletSpeed = 100;

    private float lastShootTime;

    public void Shoot()
    {
        if (lastShootTime + fireRateDelay < Time.time)
        {
            shootingSystem.Play();
            Vector3 direction = GetDirection();

            if (Physics.Raycast(bulletSpawnPoint.position, direction, out RaycastHit hit, float.MaxValue, Mask))
            {
                TrailRenderer trail = Instantiate(bulletTrail, bulletSpawnPoint.position, Quaternion.identity);
                StartCoroutine(SpawnTrail(trail, hit.point, hit.normal, true));
                lastShootTime = Time.time;
            }
            else
            {
                TrailRenderer trail = Instantiate(bulletTrail, bulletSpawnPoint.position, Quaternion.identity);

                StartCoroutine(SpawnTrail(trail, bulletSpawnPoint.position + GetDirection() * 100, Vector3.zero, false));

                lastShootTime = Time.time;
            }
        }
    }

    private Vector3 GetDirection()
    {
        Vector3 direction = transform.forward;

        if (addBulletSpread)
        {
            direction += new Vector3(
                Random.Range(-bulletSpreadVariance.x, bulletSpreadVariance.x),
                Random.Range(-bulletSpreadVariance.y, bulletSpreadVariance.y),
                Random.Range(-bulletSpreadVariance.z, bulletSpreadVariance.z)
            );

            direction.Normalize();
        }

        return direction;
    }

    private IEnumerator SpawnTrail(TrailRenderer trail, Vector3 hitPoint, Vector3 hitNormal, bool madeImpact)
    {
        Vector3 startPosition = trail.transform.position;
        float distance = Vector3.Distance(trail.transform.position, hitPoint);
        float remainingDistance = distance;

        while (remainingDistance > 0)
        {
            trail.transform.position = Vector3.Lerp(startPosition, hitPoint, 1 - (remainingDistance / distance));
            Debug.Log(trail.transform.position);

            remainingDistance -= bulletSpeed * Time.deltaTime;

            yield return null;
        }
        //Animator.SetBool("IsShooting", false);
        trail.transform.position = hitPoint;
        if (madeImpact)
        {
            Instantiate(impactParticleSystem, hitPoint, Quaternion.LookRotation(hitNormal));
        }

        Destroy(trail.gameObject, trail.time);
    }
}
