using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    private AudioListener audioListener;
    private bool isPaused = false;

    void OnEnable()
    {
        Time.timeScale = 1f;
    }

    void Start() 
    {
        audioListener = GameObject.FindObjectOfType<AudioListener>();
        pauseMenuUI.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;

            if (isPaused)
            {
                Time.timeScale = 0;
                pauseMenuUI.SetActive(true);
                AudioListener.pause = true;
            }
            else
            {
                Time.timeScale = 1;
                pauseMenuUI.SetActive(false);
                AudioListener.pause = false;
            }
        }
    }
}
