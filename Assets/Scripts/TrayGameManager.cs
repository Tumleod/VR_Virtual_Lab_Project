using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrayGameManager : MonoBehaviour
{
    public List<LabObjects> correctLabObjects = new List<LabObjects>();
    public List<LabObjects> currentLabObjectsInTray = new List<LabObjects>();
    public GameObject tray;

    [ContextMenu("Call My Function")]
    private void checkObjectsOnTray()
    {
        currentLabObjectsInTray = tray.GetComponent<Tray>().getObjectsOnTray();

        foreach (var item in currentLabObjectsInTray)
        {
            if (!correctLabObjects.Contains(item))
            {
                Debug.Log("Incorrect object on tray: " + item);
            }
            else
            {
                Debug.Log("Correct object on tray: " + item);
            }
        }
        Debug.Log(currentLabObjectsInTray);
    }
}
