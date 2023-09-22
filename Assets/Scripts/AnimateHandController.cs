using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Animator))]
public class AnimateHandController : MonoBehaviour
{
    public InputActionReference gripInputActionReference;
    public InputActionReference triggerInputActionReference;

    private Animator handAnimator;
    private float gripValue;
    private float triggerValue;

    private void Awake()
    {
        handAnimator = GetComponent<Animator>();


    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    
    void Update()
    {
        gripInputActionReference.action.performed += _ => AnimateGrip();
        triggerInputActionReference.action.performed += _ => AnimateTrigger();
    }

    private void AnimateGrip()
    {
        gripValue = gripInputActionReference.action.ReadValue<float>();
        handAnimator.SetFloat("Grip", gripValue);
    }

    private void AnimateTrigger()
    {
        triggerValue = triggerInputActionReference.action.ReadValue<float>();
        handAnimator.SetFloat("Trigger", triggerValue);
    }
}
