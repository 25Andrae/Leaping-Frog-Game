using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    AudioManager audioManager;
    public string sceneToLoad = "Start Game";
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    public void PlayGame()
    {
        StartCoroutine(LoadLevel());
    }

    IEnumerator LoadLevel()
    {
        audioManager.PlaySFX(audioManager.play_retry);
        yield return new WaitForSeconds(audioManager.play_retry.length - 2);
        SceneManager.LoadSceneAsync(sceneToLoad);
    }
    public void butts() {
        audioManager.PlaySFX(audioManager.buttons);
    }

    public void Exit()
    {
        audioManager.PlaySFX(audioManager.buttons);
        Application.Quit();
    }
}
