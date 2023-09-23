using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    public InputActionReference shootInputActionReference;
    public bool isShooting { get; private set; }

    private void OnEnable()
    {
        shootInputActionReference.action.performed += OnFire;
        shootInputActionReference.action.canceled += OnFireRelease;
    }

    private void OnDisable()
    {
        shootInputActionReference.action.performed -= OnFire;
        shootInputActionReference.action.canceled -= OnFireRelease;
    }

    private void OnFire(InputAction.CallbackContext context)
    {
        isShooting = true;
    }

    private void OnFireRelease(InputAction.CallbackContext context)
    {
        isShooting = false;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

}
