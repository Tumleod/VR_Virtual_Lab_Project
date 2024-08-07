using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pour : MonoBehaviour
{

    [SerializeField] ParticleSystem pourParticleSystem;

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Angle(Vector3.down, transform.forward) <= 90f){
            if(!pourParticleSystem.isPlaying)
                pourParticleSystem.Play();
        }else{
            if(pourParticleSystem.isPlaying)
                pourParticleSystem.Stop();
        }
    }
}
