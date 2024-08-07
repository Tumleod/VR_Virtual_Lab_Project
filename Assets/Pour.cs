using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pour : MonoBehaviour
{
    [SerializeField] float containerAmount;
    [SerializeField] float startAmount;
    [SerializeField] float endAmount;
    [SerializeField] ParticleSystem pourParticleSystem;
    [SerializeField] Liquid liquid;

    public bool isEmpty = false;
        private Coroutine pourCoroutine;

    // Update is called once per frame
   void Update()
    {
        if (isEmpty)
        {
            if (pourParticleSystem.isPlaying)
                pourParticleSystem.Stop();
            return;
        }

        if (Vector3.Angle(Vector3.down, transform.forward) <= 90f)
        {
            if (!pourParticleSystem.isPlaying)
                pourParticleSystem.Play();

            if (pourCoroutine == null)
                pourCoroutine = StartCoroutine(PourContainer());
        }
        else
        {
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
        float elapsedTime = 0f;
        while (elapsedTime < 2f)
        {
            containerAmount = Mathf.Lerp(startAmount, endAmount, elapsedTime / 2f);
            liquid.fillAmount = containerAmount;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        containerAmount = endAmount;
        isEmpty = true;
    }
}
