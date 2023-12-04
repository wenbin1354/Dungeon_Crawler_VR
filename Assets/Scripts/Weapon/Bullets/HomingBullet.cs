using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingBullet : Bullet
{
    public float turnPower = 300f;
    // homing bulllet implementation to shoot nearest Enemy
    private void Update()
    {
        // find all entities in the scene
        GameObject[] entities = GameObject.FindGameObjectsWithTag("Enemy");
        // Debug.Log(entities.Length);
        // find the closest entity
        GameObject closestEntity = null;
        float closestDistance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject entity in entities)
        {
            Vector3 diff = entity.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < closestDistance)
            {
                closestEntity = entity;
                closestDistance = curDistance;
            }
        }

        // if there is an entity, shoot it
        if (closestEntity != null)
        {
            // get the direction to the entity
            Vector3 direction = closestEntity.transform.position - transform.position;
            direction.Normalize();

            // shoot the bullet, slowly turning it towards the entity
            transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, direction, 5.0f * Time.deltaTime, 0.0f));
            GetComponent<Rigidbody>().AddForce(transform.forward * turnPower);

        }
    }
}
