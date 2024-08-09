using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private InputActionProperty triggerActionRight,
        triggerActionLeft;

    [SerializeField]
    private GameObject holdingGameObjectLeft;

    [SerializeField]
    private GameObject holdingGameObjectRight;

    public bool isHoldingObject = false;

    private void Awake()
    {
        triggerActionRight.action.performed += OnActionPressed;
        triggerActionLeft.action.performed += OnActionPressed;
    }

    private void OnDestroy()
    {
        triggerActionRight.action.started -= OnActionPressed;
        triggerActionLeft.action.started -= OnActionPressed;
    }

    void OnActionPressed(InputAction.CallbackContext context)
    {
        if (context.control.device.device.name == "OculusTouchControllerOpenXR")
        {
            if (holdingGameObjectLeft == null)
                return;

            if (holdingGameObjectLeft.CompareTag("GuideBlock"))
            {
                // Perform the task action here
                holdingGameObjectLeft.GetComponent<GuideBlock>().ActionPressed();
            }
        }
        else
        {
            if (holdingGameObjectRight == null)
                return;

            // If only the right hand is holding an object, perform the action for the right hand
            if (holdingGameObjectRight.CompareTag("GuideBlock"))
            {
                // Perform the task action here
                holdingGameObjectRight.GetComponent<GuideBlock>().ActionPressed();
            }
        }
    }

    public void OnHoldObject(SelectEnterEventArgs args)
    {
        if (args == null)
        {
            return;
        }

        GameObject heldObject = args.interactableObject.transform.gameObject;

        if (args.interactorObject is XRDirectInteractor directInteractor)
        {
            if (directInteractor.gameObject.name == "Left Direct Interactor")
            {
                // Object is held in the left hand
                holdingGameObjectLeft = heldObject;
            }
            else if (directInteractor.gameObject.name == "Right Direct Interactor")
            {
                // Object is held in the right hand
                holdingGameObjectRight = heldObject;
            }
        }
        isHoldingObject = true;
    }

    public void OnReleaseObject()
    {
        // Reset the holding object for the specific hand that is releasing it
        if (holdingGameObjectLeft != null)
        {
            holdingGameObjectLeft = null;
        }
        else if (holdingGameObjectRight != null)
        {
            holdingGameObjectRight = null;
        }

        isHoldingObject = false;
    }
}
