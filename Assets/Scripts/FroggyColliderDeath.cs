using System.Collections;
using UnityEngine;

public class FroggyColliderDeath : MonoBehaviour
{
    [SerializeField] private GameObject froggy;
    public Animator animator;
    public float delayBeforeDestroy = 0.5f;
    static public bool isFroggyAlive = true;
    public static bool tutorialOpen = true;
    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstable"))
        {
            animator.SetBool("isImmune", false);
            animator.SetBool("isBonus", false);
            audioManager.PlaySFX(audioManager.death);
            // Trigger the death animation
            animator.SetBool("isDead", true);


            // Call the DelayedDestroy coroutine to destroy the froggy after a delay
            StartCoroutine(DelayedDestroy(delayBeforeDestroy));
        }
    }

    // Coroutine to destroy the froggy after a delay
    private IEnumerator DelayedDestroy(float delay)
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delay);

        // Destroy the froggy GameObject
        Destroy(froggy);
        isFroggyAlive = false;
        tutorialOpen = false;

        /*     pauseMenu.reTry();*/
    }
}
