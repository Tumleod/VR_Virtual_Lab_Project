using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.ParticleSystemJobs;

public class SpawnSplatter : MonoBehaviour
{
    [SerializeField] GameObject splatterPrefab; // Prefab to instantiate when particles trigger
    [SerializeField] ParticleSystem pourParticleSystem; // Particle system to monitor for triggers

    [SerializeField] List<ParticleSystem.Particle> enter = new List<ParticleSystem.Particle>(); // List to store particles that enter the trigger
    private List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>(); // List to store collision events

    // Method to spawn a splatter at a given position
    public void SpawnSplatterTrigger(Vector3 position)
    {
        GameObject splatter = Instantiate(splatterPrefab, position, Quaternion.identity);
        Destroy(splatter, 1f); // Destroy the splatter after 5 seconds
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
            ParticleCollisionEvent collision = collisionEvents[i];
            Vector3 position = collision.intersection;
            
            SpawnSplatterTrigger(position); // Spawn splatter at collision position
        }
    }
}