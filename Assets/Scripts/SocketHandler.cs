using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SocketHandler : MonoBehaviour
{
    // Reference to the XRSocketInteractor component
    private XRSocketInteractor socketInteractor;

    // Reference to the tray GameObject
    public GameObject tray;

    void Awake()
    {
        // Get the XRSocketInteractor component
        socketInteractor = GetComponent<XRSocketInteractor>();
        // Check if the XRSocketInteractor component is found
        if (socketInteractor == null)
        {
            Debug.LogError("XRSocketInteractor component not found on this GameObject.");
            return;
        }

        // Subscribe to the select entered event
        socketInteractor.selectEntered.AddListener(OnSelectEntered);
        socketInteractor.selectExited.AddListener(OnSelectExited);
    }

    void OnDestroy()
    {
        // Unsubscribe from the select entered event
        socketInteractor.selectEntered.RemoveListener(OnSelectEntered);
    }

    private void OnSelectEntered(SelectEnterEventArgs args)
    {
        // Set the parent of the interactable object to be the socket
        args.interactableObject.transform.SetParent(socketInteractor.transform);

        // Optionally, you can reset the local position, rotation, and scale
        args.interactableObject.transform.localPosition = Vector3.zero;
        args.interactableObject.transform.localRotation = Quaternion.identity;
        args.interactableObject.transform.localScale = Vector3.one;

        // Add the object to the tray
        GameObject interactableGameObject = (args.interactableObject as MonoBehaviour)?.gameObject;
        if (interactableGameObject != null)
        {
            tray.GetComponent<Tray>().AddObjectToTray(interactableGameObject);
        }
        else
        {
            Debug.LogWarning("Interactable does not have a GameObject.");
        }
    }

    private void OnSelectExited(SelectExitEventArgs args)
    {
        // Remove the object from the tray
        if (tray != null)
        {
            GameObject interactableGameObject = (
                args.interactableObject as MonoBehaviour
            )?.gameObject;
            if (interactableGameObject != null)
            {
                tray.GetComponent<Tray>().RemoveObjectFromTray(interactableGameObject);
            }
            else
            {
                Debug.LogWarning("Interactable does not have a GameObject.");
            }
        }
        else
        {
            Debug.LogWarning("Tray GameObject is not assigned.");
        }
    }
}
