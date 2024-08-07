using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tray : MonoBehaviour
{
    private bool isFull = false;
    public List<GameObject> containedObjects = new List<GameObject>(); // the objects that the tray is holding

    private void OnCollisionEnter(Collision other)
    {
        if (!containedObjects.Contains(other.gameObject))
        {
            containedObjects.Add(other.gameObject);
            Debug.Log("Added " + other.gameObject.name + " to tray");
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (containedObjects.Contains(other.gameObject))
        {
            containedObjects.Remove(other.gameObject);
            Debug.Log("Removed " + other.gameObject.name + " from tray");
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, transform.localScale);
    }
}
