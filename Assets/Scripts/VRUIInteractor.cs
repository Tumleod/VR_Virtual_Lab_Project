using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.XR.Interaction.Toolkit;

public class VRUIInteractor : MonoBehaviour
{
    public XRRayInteractor rayInteractor;

    [SerializeField]
    private UIDocument uiDocument;

    void Start()
    {
        // Find the UIDocument in the scene (assumes there's only one)
        uiDocument = FindObjectOfType<UIDocument>();
    }

    void Update()
    {
        if (rayInteractor.TryGetCurrentUIRaycastResult(out var result))
        {
            // Convert the world position of the raycast hit to a screen position
            Vector2 screenPosition = Camera.main.WorldToScreenPoint(result.worldPosition);

            // Convert screen position to Panel position
            Vector2 panelPosition = RuntimePanelUtils.ScreenToPanel(
                uiDocument.rootVisualElement.panel,
                screenPosition
            );

            // Use panel.Pick to get the VisualElement at the given panel position
            VisualElement pickedElement = uiDocument.rootVisualElement.panel.Pick(panelPosition);

            if (pickedElement != null)
            {
                // Example: Check if the picked element is part of a ScrollView
                var scrollView = pickedElement.GetFirstAncestorOfType<ScrollView>();
                if (scrollView != null)
                {
                    // Perform scrolling or other interaction
                    if (Input.GetAxis("Vertical") > 0)
                    {
                        scrollView.scrollOffset += new Vector2(0, 10); // Scroll down
                    }
                    else if (Input.GetAxis("Vertical") < 0)
                    {
                        scrollView.scrollOffset -= new Vector2(0, 10); // Scroll up
                    }
                }
            }
        }
    }
}
