using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathHandler : MonoBehaviour
{
    [SerializeField] Canvas gameOverCanvas;
    [SerializeField] AudioSource deathAudio;
    [SerializeField] GameObject[] weapons;
    AudioSource[] allAudioSources;

    void Start()
    {
        gameOverCanvas.enabled = false;
    }

    public void HandleDeath()
    {
        allAudioSources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
        gameOverCanvas.enabled = true;
        Time.timeScale = 0;
        foreach(GameObject weapon in weapons)
        {
            weapon.GetComponent<Weapon>().enabled = false;
        }
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        foreach(AudioSource audio in allAudioSources)
        {
            if(audio != null)
            audio.Stop();
        }
        deathAudio.Play();
    }
}
