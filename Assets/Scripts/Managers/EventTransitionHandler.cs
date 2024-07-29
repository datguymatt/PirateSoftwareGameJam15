using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventTransitionHandler : MonoBehaviour
{
    private MenuManager menuManager;
    private AudioManager audioManager;

    //time
    public float transitionDuration = 7f;

    public GameObject allUIElements;


    // Start is called before the first frame update
    void Start()
    {
        menuManager = FindObjectOfType<MenuManager>();
        audioManager = FindObjectOfType<AudioManager>();
        //subscribe to events
        menuManager.StartGameClicked += StartGameClicked;
    }

    public void StartGameClicked()
    {
        StartCoroutine(StartGameRoutine());
    }

    private IEnumerator StartGameRoutine()
    {
        //start animation??
        allUIElements.SetActive(false);
        //coordinate with audio manager
        audioManager.PlaySFXAudio("gong-start");
        //wait then launch intro level
        yield return new WaitForSeconds(transitionDuration);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
