using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TrashcanManager : MonoBehaviour
{
    public Dictionary<KemikalieEnum, bool> trashcanStatus;

    private void Start()
    {
        trashcanStatus = new Dictionary<KemikalieEnum, bool>();
        foreach (KemikalieEnum type in System.Enum.GetValues(typeof(KemikalieEnum)))
        {
            trashcanStatus[type] = false;
        }
    }

    public void SetTrashcanCompleted(KemikalieEnum type)
    {
        if (trashcanStatus.ContainsKey(type))
        {
            trashcanStatus[type] = true;
            Debug.Log("Trashcan for " + type + " is completed!");
            CheckAllTrashcansCompleted();
        }
    }

    private void CheckAllTrashcansCompleted()
    {
        foreach (bool status in trashcanStatus.Values)
        {
            if (!status)
            {
                return; // If any trashcan is not completed, return
            }
        }

        // If all trashcans are completed, perform the desired action
        Debug.Log("All trashcans are completed!");
        // Add your code here for what should happen when all trashcans are completed
    }
}
