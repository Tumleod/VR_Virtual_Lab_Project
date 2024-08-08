using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HandAnimatorController : MonoBehaviour
{
    // Reference to the trigger action
    [SerializeField]
    private InputActionProperty triggerAction;

    // Reference to the grip action
    [SerializeField]
    private InputActionProperty gripAction;

    // Reference to the Animator component
    private Animator animator;

    private void Start()
    {
        // Get the Animator component
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // Get the trigger and grip values
        float triggerValue = triggerAction.action.ReadValue<float>();
        float gripValue = gripAction.action.ReadValue<float>();

        // Set the trigger and grip values to the Animator
        animator.SetFloat("Trigger", triggerValue);
        animator.SetFloat("Grip", gripValue);
    }
}
