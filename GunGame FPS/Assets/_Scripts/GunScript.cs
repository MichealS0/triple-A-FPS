using UnityEngine;
using System.Collections;
using System.Linq;
using TMPro;

public class GunScript : MonoBehaviour
{
    [Header("Refrences")]
    [SerializeField] private Camera fpsCam;
    [SerializeField] private ParticleSystem muzzelFlash;
    [SerializeField] private GameObject impactEffect;
    [SerializeField] private TextMeshProUGUI Ammu;
    [SerializeField] private Transform gun;
    [SerializeField] private Transform ADSpos;
    [SerializeField] private Transform NormalPos;

    [Header("Game Settings")]
    [SerializeField] private float damage = 10f;
    [SerializeField] private float range = 100f;
    [SerializeField] private float knockBackForce;
    [SerializeField] private float shotsShot = 0f;
    [SerializeField] private float MagSize;
    [SerializeField] private float fireRate = 0.1f;
    [SerializeField] private float nextFire = 0f;

    [Header("Booleans")]
    [SerializeField] private bool canShoot;

    void Start()
    {
        canShoot = true;
    }

    void FixedUpdate()
    {
        if (canShoot)
        {
            if(Input.GetButton("Fire1") && Time.time > nextFire - 0.05f)
            {
                nextFire = Time.time + fireRate;
                Shoot(canShoot);
                shotsShot++;

                // Outputting The number of shots fired
                Debug.Log(shotsShot);

                if (shotsShot == MagSize)
                {
                    canShoot = false;
                    StartCoroutine(FiresDone());
                }
            }

            else if (Input.GetKeyDown(KeyCode.R))
            {
                StartCoroutine(Reload());
                canShoot = false;
            }

            else if(Input.GetButtonDown("Fire2"))
            {
                gun.position = ADSpos.position;
            }

            else if(Input.GetButtonUp("Fire2"))
            {
                gun.position = NormalPos.position;
            }

            Ammu.text = shotsShot.ToString();
        }
    }

    IEnumerator FiresDone()
    {
        shotsShot = 0;

        yield return new WaitForSeconds(2);

        canShoot= true;
    }

    IEnumerator Reload()
    {
        shotsShot -= shotsShot;

        yield return new WaitForSeconds(2);

        canShoot = true;
    }

    void Shoot(bool canShoot)
    {
        RaycastHit hit;

        if (canShoot)
        {

            if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
            {
                muzzelFlash.Play();

                TakeDamage target = hit.transform.GetComponent<TakeDamage>();
                if (target != null) {
                    target.DecreaseHealth(damage);
                }

                GameObject bullethole = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                bullethole.transform.position += bullethole.transform.position / 100;
                Destroy(bullethole, 5f);

                if (hit.rigidbody != null)
                {
                    hit.rigidbody.AddForce(-hit.normal * knockBackForce);
                }
            }
        }
    }
    // -- Back At It Again
}
