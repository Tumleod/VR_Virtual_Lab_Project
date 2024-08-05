using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HideRayCast : MonoBehaviour
{
    [SerializeField]
    GameObject interactorLineGameObject;

    public void HideRaycast()
    {
        interactorLineGameObject.GetComponent<XRInteractorLineVisual>().lineWidth = 0;
    }

    public void ShowRaycast()
    {
        interactorLineGameObject.GetComponent<XRInteractorLineVisual>().lineWidth = 0.005f;
    }
}
