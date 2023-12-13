using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magazine : MonoBehaviour
{
    [SerializeField] private int ammoCount;
    [SerializeField] private GameObject magazineWithAmmo;
    [SerializeField] private GameObject magazineWithNoAmmo;
    [SerializeField] private GameObject bulletPrefab;

    private void Start()
    {

        // Initially, set the representation based on the current ammo count.
        UpdateRepresentation(ammoCount);
    }

    public Magazine(int _intialAmmoCount)
    {
        ammoCount = _intialAmmoCount;
    }

    public void AddAmmo(int _ammoCount)
    {
        ammoCount += _ammoCount;
        UpdateRepresentation(ammoCount);
    }

    public void RemoveAmmo(int _ammoCount)
    {
        ammoCount -= _ammoCount;
        UpdateRepresentation(ammoCount);
    }

    public int GetAmmoCount()
    {
        return ammoCount;
    }

    public void UpdateRepresentation(int _ammoCount)
    {
        if (_ammoCount > 0)
        {
            magazineWithAmmo.SetActive(true);
            magazineWithNoAmmo.SetActive(false);
        }
        else
        {
            magazineWithAmmo.SetActive(false);
            magazineWithNoAmmo.SetActive(true);
        }
    }

    public GameObject GetBulletPrefab()
    {
        return bulletPrefab;
    }
}
