using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Tray : MonoBehaviour
{
    public List<GameObject> objectsOnTray = new List<GameObject>();

    /*
        public GameObject SocketObject;
        public GameObject areaObject;
        public int numberOfPrefabs;
    
        private BoxCollider areacollider;
    */
    private void Start() { }

    public void AddObjectToTray(GameObject obj)
    {
        if (obj == null)
        {
            Debug.LogError("Object is null");
            return;
        }
        else if (objectsOnTray.Contains(obj))
        {
            Debug.LogError("Object is already on the tray");
            return;
        }
        else
        {
            objectsOnTray.Add(obj);
        }
    }

    public void RemoveObjectFromTray(GameObject obj)
    {
        if (obj == null)
        {
            Debug.LogError("Object is null");
            return;
        }
        else if (!objectsOnTray.Contains(obj))
        {
            Debug.LogError("Object is not on the tray");
            return;
        }
        else
        {
            objectsOnTray.Remove(obj);
        }
    }

    public List<LabObjects> GetObjectsOnTray()
    {
        List<LabObjects> labObjects = new List<LabObjects>();
        foreach (var item in objectsOnTray)
        {
            LabItem labItem = item.gameObject.GetComponent<LabItem>();
            if (labItem != null)
            {
                labObjects.Add(labItem.labObject);
            }
        }
        return labObjects;
    }
}
