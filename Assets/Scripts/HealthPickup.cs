using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    AudioSource pickup;
    public AudioClip audioClip;

    void Start()
    {
        pickup = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.tag == "Player")
        {
            FindObjectOfType<PlayerHealth>().ChangeHealth();
            pickup.Play();
            SphereCollider collider = GetComponent<SphereCollider>();
            collider.enabled = false;
            BoxCollider boxCollider = GetComponent<BoxCollider>();
            boxCollider.enabled = false;
            GameObject light = transform.GetChild(0).gameObject;
            light.SetActive(false);
            StartCoroutine(WaitForAudioClip(audioClip.length));
        }
    }

    IEnumerator WaitForAudioClip(float clipLength)
    {
        yield return new WaitForSeconds(clipLength);
        Destroy(gameObject);
    }
}
