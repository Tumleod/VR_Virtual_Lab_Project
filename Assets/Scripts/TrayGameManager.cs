using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrayGameManager : MonoBehaviour
{
    // List of correct objects that should be on the tray
    public List<LabObjects> correctLabObjects = new List<LabObjects>();

    // List of objects that are currently on the tray
    public List<LabObjects> currentLabObjectsInTrays = new List<LabObjects>();

    // Reference to the tray GameObject
    public List<GameObject> trays = new List<GameObject>();

    // Boolean to check if the objects on the tray are correct
    [SerializeField]
    private bool isCorrect = true;

    [ContextMenu("Call My Function")]
    public void CheckObjectsOnTray()
    {
        foreach (var tray in trays)
        {
            currentLabObjectsInTrays.AddRange(tray.GetComponent<Tray>().GetObjectsOnTray());
        }

        // Check if the objects on the tray are correct
        foreach (var item in currentLabObjectsInTrays)
        {
            if (!correctLabObjects.Contains(item))
            {
                Debug.Log("Incorrect object on tray: " + item);
                isCorrect = false;
            }
            else
            {
                Debug.Log("Correct object on tray: " + item);
            }
        }

        if (isCorrect)
        {
            Debug.Log("All objects on tray are correct");
        }
    }
}
