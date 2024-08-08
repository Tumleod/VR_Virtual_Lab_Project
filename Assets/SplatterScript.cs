using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SplatterScript : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Splatter")
        {
            if (gameObject.GetInstanceID() < collision.gameObject.GetInstanceID())
            {
                Destroy(gameObject);
            }
            else
            {
                transform.localScale = new Vector3(
                    transform.localScale.x * 1.01f,
                    transform.localScale.y * 1.01f,
                    transform.localScale.z * 1.01f
                );

                float randomAngle = Random.Range(-5f, 5f);
                transform.RotateAround(transform.position, transform.up, randomAngle);
            }
        }
    }

    public void IncreaseSize()
    {
        transform.localScale = new Vector3(
            transform.localScale.x * 1.01f,
            transform.localScale.y * 1.01f,
            transform.localScale.z * 1.01f
        );

        float randomAngle = Random.Range(-5f, 5f);
        transform.RotateAround(transform.position, transform.up, randomAngle);
    }
}
