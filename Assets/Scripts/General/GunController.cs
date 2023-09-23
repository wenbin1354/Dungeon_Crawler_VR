using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GunController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private Transform bulletSpawnPoint;
    [SerializeField]
    private GameObject holdingHand;
    private PlayerController playerController;

    [Header("Gun Settings")]
    [SerializeField]
    private float bulletSpeed = 500f;
    [SerializeField]
    private float shootingRate = 0.5f; // Controls the shooting rate in bullets per second
    private float nextShootTime; // Used to control the shooting rate
    [SerializeField]
    private bool enableContinuouslyShooting = true;

    private XRGrabInteractable grabInteractable;
    private void Awake()
    {
        playerController = holdingHand.GetComponent<PlayerController>();
        grabInteractable = GetComponent<XRGrabInteractable>();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (playerController != null && playerController.isShooting)
        {
            if (enableContinuouslyShooting && Time.time >= nextShootTime)
            {
                Shoot();
                nextShootTime = Time.time + 1f / shootingRate; // Calculate the next allowed shoot time
            }
            
        }

    }

    void Shoot()
    {
        if (grabInteractable.isSelected)
        {
            var interactor = grabInteractable.firstInteractorSelecting as XRBaseInteractor;
            if (interactor != null)
            {
                var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
                bullet.GetComponent<Rigidbody>().AddForce(bulletSpawnPoint.forward * bulletSpeed);
                Destroy(bullet, 2f);
            }
        }
    }
}
