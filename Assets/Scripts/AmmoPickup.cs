using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AmmoPickup : MonoBehaviour
{
    [SerializeField] int ammoAmount = 5;
    [SerializeField] AmmoType ammoType;
    AudioSource pickup;
    public AudioClip audioClip;

    void Start()
    {
        pickup = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.tag == "Player")
        {
            FindObjectOfType<Ammo>().IncreaseCurrentAmmo(ammoType, ammoAmount);
            pickup.Play();
            SphereCollider collider = GetComponent<SphereCollider>();
            collider.enabled = false;
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
