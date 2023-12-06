using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject spawnObject;
    public Transform spawnPoint;
    public Transform spawnPoint2;
    public Transform spawnPoint3;
    // bool is enemy, is is enemy add 2 more spawnpoint
    // if is enemy, add 2 more spawnpoint


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnSingleObject()
    {
        Instantiate(spawnObject, spawnPoint.position, spawnPoint.rotation);
    }

    public void SpawnMultipleObject()
    {
        GameObject one = Instantiate(spawnObject, spawnPoint.position, spawnPoint.rotation);
        GameObject two = Instantiate(spawnObject, spawnPoint2.position, spawnPoint2.rotation);
        GameObject three =Instantiate(spawnObject, spawnPoint3.position, spawnPoint3.rotation);
    }
}
