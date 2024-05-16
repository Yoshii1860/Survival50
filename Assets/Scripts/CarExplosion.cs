using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarExplosion : MonoBehaviour
{
    [SerializeField] ParticleSystem explosionVFX;
    [SerializeField] AudioSource explosionSFX;

    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.tag == "Player")
        {
            explosionVFX.Play();
            explosionSFX.Play();
            Destroy(gameObject);
        }
    }
}
