using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.ParticleSystemJobs;

public class SpawnSplatter : MonoBehaviour
{
    [SerializeField]
    Pour pour;

    public KemikalieEnum kemikalieEnum;

    [SerializeField]
    GameObject splatterPrefab;

    [SerializeField]
    ParticleSystem splatterParticleSystem;

    [SerializeField]
    ParticleSystem pourParticleSystem; // Particle system to monitor for triggers

    [SerializeField] GameObject liquidObject;

    [SerializeField]
    List<ParticleSystem.Particle> enter = new List<ParticleSystem.Particle>(); // List to store particles that enter the trigger
    private List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>(); 
    private Color color;// List to store collision events

    private void Start() {
        color = liquidObject.GetComponent<Renderer>().sharedMaterial.GetColor("_BottomColor");
    }

    // Method to spawn a splatter at a given position
    public void SpawnSplatterTrigger(Vector3 position)
    {
        GameObject splatter = Instantiate(splatterPrefab, position, Quaternion.identity);
        Renderer splatterRenderer = splatter.GetComponent<Renderer>();
        splatterRenderer.material.SetColor("_BottomColor", color);
        splatterRenderer.material.SetColor("_Rim_Color", color);
        splatterRenderer.material.SetColor("_TopColor", color);
        //Destroy(splatter, 1f); // Destroy the splatter after 5 seconds
    }

    // Called when particles collide with a GameObject
    void OnParticleCollision(GameObject other)
    {
        // Ensure collisionEvents list is initialized
        if (collisionEvents == null)
        {
            collisionEvents = new List<ParticleCollisionEvent>();
        }

        // Get collision events
        ParticlePhysicsExtensions.GetCollisionEvents(pourParticleSystem, other, collisionEvents);

        // Loop through all collision events
        for (int i = 0; i < collisionEvents.Count; i++)
        {
            Debug.Log(other.gameObject.name);
            ParticleCollisionEvent collision = collisionEvents[i];
            Vector3 position = collision.intersection;
            if (other.gameObject.CompareTag("Splatter"))
            {
                Instantiate(splatterParticleSystem, position, Quaternion.LookRotation(Vector3.up));
                other.gameObject.GetComponent<SplatterScript>().IncreaseSize();
                pour.ResetPour();
            }
            else if (other.gameObject.CompareTag("Trashcan"))
            {
                if (kemikalieEnum != other.gameObject.GetComponent<TrashcanForChemicals>().kemikalieEnum)
                {
                    pour.ResetPour();
                }
            }
            else
            {
                pour.ResetPour();
                SpawnSplatterTrigger(position); // Spawn splatter at collision position
            }
        }
    }
}
