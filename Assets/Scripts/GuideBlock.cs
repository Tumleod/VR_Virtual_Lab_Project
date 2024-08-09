using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GuideBlock : MonoBehaviour
{
    public GameObject canvas;

    [SerializeField]
    private List<VisualTreeAsset> pages = new List<VisualTreeAsset>();

    private int currentPage = 1;

    // Start is called before the first frame update

    [ContextMenu("Turn Page")]
    public void ActionPressed()
    {
        if (pages == null || pages.Count == 0)
        {
            Debug.LogWarning("No pages available to turn.");
            return;
        }

        // Assign the current page's VisualTreeAsset to the UIDocument
        canvas.GetComponent<UIDocument>().visualTreeAsset = pages[currentPage];

        // Increment currentPage, and wrap around if it exceeds the last page
        currentPage = (currentPage + 1) % pages.Count;
    }
}
