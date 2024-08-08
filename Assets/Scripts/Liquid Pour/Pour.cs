using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pour : MonoBehaviour
{
    [SerializeField]
    TrashcanManager trashcanManager;

    [SerializeField]
    SpawnSplatter spawnSplatter;

    [SerializeField]
    float containerAmount;

    [SerializeField]
    float startAmount;

    [SerializeField]
    float endAmount;

    [SerializeField]
    ParticleSystem pourParticleSystem;

    [SerializeField]
    Liquid liquid;

    public bool isEmpty = false;
    private Coroutine pourCoroutine;

    [SerializeField]
    float elapsedTime = 0f;

    // Update is called once per frame
    void Update()
    {
        // Check if the container is empty
        if (isEmpty)
        {
            //If the container is empty, stop the particle system
            if (pourParticleSystem.isPlaying)
                pourParticleSystem.Stop();

            //If the container is empty, do nothing and return
            return;
        }

        //Check the angle of the container, and if it is less than 90 degrees, start the particle system
        if (Vector3.Angle(Vector3.down, transform.forward) <= 90f)
        {
            //If the particle system is not playing, start it to ensure the particle effect only starts once
            if (!pourParticleSystem.isPlaying)
                pourParticleSystem.Play();

            //If the pour coroutine is null, start it
            if (pourCoroutine == null)
                pourCoroutine = StartCoroutine(PourContainer());
        }
        else
        {
            //If the container is not at the correct angle, stop the particle system and the pour coroutine
            if (pourParticleSystem.isPlaying)
                pourParticleSystem.Stop();

            if (pourCoroutine != null)
            {
                StopCoroutine(pourCoroutine);
                pourCoroutine = null;
            }
        }
    }

    private IEnumerator PourContainer()
    {
        while (elapsedTime < 2f)
        {
            containerAmount = Mathf.Lerp(startAmount, endAmount, elapsedTime / 2f);
            liquid.fillAmount = containerAmount;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        containerAmount = endAmount + 1;
        liquid.fillAmount = containerAmount;
        isEmpty = true;
        PourComplete();
    }

    public void ResetPour()
    {
        elapsedTime = 0f;
        containerAmount = startAmount;
        liquid.fillAmount = containerAmount;
        isEmpty = false;
    }

    private void PourComplete()
    {
        trashcanManager.SetTrashcanCompleted(spawnSplatter.kemikalieEnum);
    }
}
