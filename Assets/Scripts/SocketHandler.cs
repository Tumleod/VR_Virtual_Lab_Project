using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SocketHandler : MonoBehaviour
{
    private XRSocketInteractor socketInteractor;
    public GameObject tray;

    void Awake()
    {
        socketInteractor = GetComponent<XRSocketInteractor>();
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

        GameObject interactableGameObject = (args.interactableObject as MonoBehaviour)?.gameObject;
        if (interactableGameObject != null)
        {
            tray.GetComponent<Tray>().addObjectToTray(interactableGameObject);
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
                tray.GetComponent<Tray>().removeObjectFromTray(interactableGameObject);
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
