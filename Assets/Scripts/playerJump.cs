using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class playJump : MonoBehaviour
{
    public Rigidbody2D Myrigidbody2D;
    [SerializeField] private float speedMultiplier = 12.8f;
    public float gravityWhileFalling = 2.5f;
    private bool isColldingWithPlatform = false;
    private bool highestPoint = false;
    private float jumpTimer = 1.5f;
    [SerializeField] private Animator froggyAnimator;
    AudioManager audioManager;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    #region
    // Update is called once per frame
    void Update()
    {
        // Check for touch input
        if (Input.touchCount > 0)
        {
            // Check each touch
            foreach (Touch touch in Input.touches)
            {
                // Check if the touch began
                if (touch.phase == TouchPhase.Began)
                {
                    // Check if the touch position is within a reasonable area (you may need to adjust this)
                    if (touch.position.x < Screen.width / 2)
                    {
                        
                        // Perform the action if the touch is in the lower half of the screen
                        if (isColldingWithPlatform)
                        {
                          
                            Debug.Log("Touch detected in the lower half of the screen and player is colliding with platform");
                            highestPoint = true;
                            Myrigidbody2D.velocity = Vector2.up * speedMultiplier;                           
                            isColldingWithPlatform = false; // State reset
                            audioManager.PlaySFX(audioManager.jump);
                            if (PlayerScript.isBonus && !PlayerScript.isImmune)
                            {
                                froggyAnimator.SetBool("isBonus", true);
                                froggyAnimator.SetBool("isImmune", false);
                                froggyAnimator.SetBool("isJumping", true);
                            }
                            else if (PlayerScript.isImmune && !PlayerScript.isBonus)
                            {
                                froggyAnimator.SetBool("isBonus", false);
                                froggyAnimator.SetBool("isImmune", true);
                                froggyAnimator.SetBool("isJumping", true);
                            }
                            else
                            {
                                froggyAnimator.SetBool("isBonus", false);
                                froggyAnimator.SetBool("isJumping", true);
                            }                           
                            ////////////////////////////////////////////////
                            ///              Debug Section               ///
                            ////////////////////////////////////////////////

                            Debug.Log(Myrigidbody2D.gravityScale);
                            
                        }
                    }
                }
                if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved)
                {
                    
                    // Check if the touch position is within a reasonable area (you may need to adjust this)
                    if (touch.position.x < Screen.width / 2)
                    {
                        // Delay the globalMaxima() method based on the jumpTimer
                        if (jumpTimer > 0)
                        {
                            jumpTimer -= Time.deltaTime;
                           
                        }
                        else
                        {
                            globalMaxima();
                        }
                    }
                }

            }
        }
        globalMaxima();
    }
    #endregion

    void globalMaxima() {
     
        if (highestPoint && Myrigidbody2D.velocity.y < 0f)
        {
            Myrigidbody2D.gravityScale = gravityWhileFalling;
            highestPoint = false;
            jumpTimer = 1.5f;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("actualFloor"))
        {
            isColldingWithPlatform = true;
            Myrigidbody2D.gravityScale = 1;
            froggyAnimator.SetBool("isJumping", false);
        }
    }
}
