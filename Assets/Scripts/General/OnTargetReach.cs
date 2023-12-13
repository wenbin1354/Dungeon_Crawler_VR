using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnTargetReach : MonoBehaviour
{
    [SerializeField] private float threshold = 0.2f;
    [SerializeField] private Transform target;
    [SerializeField] private UnityEvent OnReached;
    [SerializeField] private bool wasReached = false;

    private void FixedUpdate() {
        float distance = Vector3.Distance(transform.position, target.position);
        if (distance < threshold && !wasReached) {
            OnReached.Invoke();
            wasReached = true;
        }
        else if (distance > threshold){
            wasReached = false;
        }
    }
}
