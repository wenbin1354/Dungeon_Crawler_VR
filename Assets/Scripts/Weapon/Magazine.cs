using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magazine : MonoBehaviour
{
    [SerializeField] private int ammoCount;

    public Magazine(int _intialAmmoCount)
    {
        ammoCount = _intialAmmoCount;
    }

    public void AddAmmo(int _ammoCount)
    {
        ammoCount += _ammoCount;
    }

    public void RemoveAmmo(int _ammoCount)
    {
        ammoCount -= _ammoCount;
    }

    public int GetAmmoCount()
    {
        return ammoCount;
    }
}
