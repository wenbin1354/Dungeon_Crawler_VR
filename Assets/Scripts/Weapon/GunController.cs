using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SimpleShoot : MonoBehaviour
{
    [Header("Prefab Refrences")]
    public GameObject casingPrefab;
    public GameObject muzzleFlashPrefab;
    public Transform enemyTransform;

    [Header("Location Refrences")]
    [SerializeField] private Animator gunAnimator;
    [SerializeField] private Transform barrelLocation;
    [SerializeField] private Transform casingExitLocation;

    [Header("Settings")]
    [Tooltip("Specify time to destory the casing object")][SerializeField] private float destroyTimer = 2f;
    [SerializeField] private float bulletDamageMultiplier = 1f;
    [Tooltip("Bullet Speed")][SerializeField] private float shotPower = 500f;
    [Tooltip("Casing Ejection Speed")][SerializeField] private float ejectPower = 150f;

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip shootSound;
    [SerializeField] private AudioClip reloadSound;
    [SerializeField] private AudioClip noAmmoSound;

    [Header("Magazine")]
    [SerializeField] private Magazine magazine;
    [SerializeField] private GameObject magazineLocation;
    [SerializeField] private XRBaseInteractor magazineInteractor;

    [Header("Reload Slide")]
    [SerializeField] private bool hasReloadSlide = true;

    void Awake()
    {
        magazineInteractor = magazineLocation.GetComponentInChildren<XRSocketInteractor>();

        if (magazineInteractor == null)
            magazineInteractor = GetComponentInChildren<XRSocketInteractor>();

        magazineInteractor.selectEntered.AddListener(AddMagazine);
        magazineInteractor.selectExited.AddListener(RemoveMagazine);

    }

    void Start()
    {
        if (barrelLocation == null)
            barrelLocation = transform;

        if (gunAnimator == null)
            gunAnimator = GetComponentInChildren<Animator>();

        if (magazine == null)
            magazine = magazineInteractor.GetOldestInteractableSelected().transform.GetComponent<Magazine>();
    }

    public void PullTrigger()
    {
        if (magazine && magazine.GetAmmoCount() > 0 && hasReloadSlide)
        {
            gunAnimator.SetTrigger("Fire");
        }
        else
        {
            audioSource.PlayOneShot(noAmmoSound);
        }
    }


    //This function creates the bullet behavior
    void Shoot()
    {
        GameObject bulletPrefab = magazine.GetBulletPrefab();
        // play shoot sound
        audioSource.PlayOneShot(shootSound);

        // decrease ammo count
        magazine.RemoveAmmo(1);

        if (muzzleFlashPrefab)
        {
            //Create the muzzle flash
            GameObject tempFlash;
            tempFlash = Instantiate(muzzleFlashPrefab, barrelLocation.position, barrelLocation.rotation);

            //Destroy the muzzle flash effect
            Destroy(tempFlash, destroyTimer);
        }

        //cancels if there's no bullet prefeb
        if (!bulletPrefab)
        { return; }

        // Create a bullet and add force on it in direction of the barrel
        GameObject bullet = Instantiate(bulletPrefab, barrelLocation.position, barrelLocation.rotation);
        bullet.GetComponent<Bullet>().bulletDamageMultiplier = bulletDamageMultiplier;
        bullet.GetComponent<Rigidbody>().AddForce(barrelLocation.forward * shotPower);
    }

    //This function creates a casing at the ejection slot
    void CasingRelease()
    {
        //Cancels function if ejection slot hasn't been set or there's no casing
        if (!casingExitLocation || !casingPrefab)
        { return; }

        //Create the casing
        GameObject tempCasing;
        tempCasing = Instantiate(casingPrefab, casingExitLocation.position, casingExitLocation.rotation) as GameObject;
        //Add force on casing to push it out
        tempCasing.GetComponent<Rigidbody>().AddExplosionForce(Random.Range(ejectPower * 0.7f, ejectPower), (casingExitLocation.position - casingExitLocation.right * 0.3f - casingExitLocation.up * 0.6f), 1f);
        //Add torque to make casing spin in random direction
        tempCasing.GetComponent<Rigidbody>().AddTorque(new Vector3(0, Random.Range(100f, 500f), Random.Range(100f, 1000f)), ForceMode.Impulse);

        //Destroy casing after X seconds
        Destroy(tempCasing, destroyTimer);
    }

    void AddMagazine(SelectEnterEventArgs context)
    {
        // magazineInteractor.GetOldestInteractableSelected();
        magazine = magazineInteractor.GetOldestInteractableSelected().transform.GetComponent<Magazine>();
        audioSource.PlayOneShot(reloadSound);
        hasReloadSlide = false;
    }

    void RemoveMagazine(SelectExitEventArgs context)
    {
        magazine = null;
        audioSource.PlayOneShot(reloadSound);
    }

    public void ReloadSlide()
    {
        hasReloadSlide = true;
        audioSource.PlayOneShot(reloadSound);
    }

    public void RevertDamageMultiplierAndSpeed()
    {
        bulletDamageMultiplier = (bulletDamageMultiplier == 1f) ? 1.5f : 1f;
        shotPower = (shotPower == 1000f) ? 1500f : 1000f;
    }

}
