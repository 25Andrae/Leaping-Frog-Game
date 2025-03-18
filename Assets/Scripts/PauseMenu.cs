using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject froggyIsDeathPanel;
    public static bool isPaused;
    public string sceneToLoad = "Main Menu";
    AudioManager audioManager;
    /*private bool tutorialOpen = true;*/
    public GameObject tutorialPanel;
    [SerializeField] Animator transitionAnim;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
        froggyIsDeathPanel.SetActive(false);
        if (!FroggyColliderDeath.tutorialOpen)
        {
            tutorialPanel.SetActive(false);
        }
      
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            tutorialPanel.SetActive(false);
            FroggyColliderDeath.tutorialOpen = false;
        }
    }

    public void PauseGame()
    {
        audioManager.PlaySFX(audioManager.buttons);
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true; 
    }
    public void ResumeGame()
    {
        audioManager.PlaySFX(audioManager.play_retry);
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void GoToMainMenu()
    {
        pauseMenu.SetActive(false);
        StartCoroutine(LoadLevel());
        FroggyColliderDeath.tutorialOpen = true;
        Time.timeScale = 1f;
    }

    IEnumerator LoadLevel()
    {
        transitionAnim.SetTrigger("End");
        audioManager.PlaySFX(audioManager.buttons);   
        yield return new WaitForSeconds(audioManager.buttons.length - 2);
        SceneManager.LoadSceneAsync(sceneToLoad);
        transitionAnim.SetTrigger("Start");
    }
    public void QuitGame()
    {
        audioManager.PlaySFX(audioManager.buttons);
        Application.Quit();
    }
    public void reTry()
    {
        StartCoroutine(LoadGame());
        FroggyColliderDeath.tutorialOpen = false;
        /*Debug.Log("The current setting after pressing the retry for open tutorial panel is " + tutorialOpen);*/
        Time.timeScale = 1f;
        froggyIsDeathPanel.SetActive(false);
        obstacleDespawner.isTheFirstObjectHasBeenDestroyed = false;
       
    }
    IEnumerator LoadGame()
    {
        transitionAnim.SetTrigger("End");
        audioManager.PlaySFX(audioManager.play_retry);
        yield return new WaitForSeconds(audioManager.play_retry.length - 2.8f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        transitionAnim.SetTrigger("Start");
    }

}
