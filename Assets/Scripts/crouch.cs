using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crouch : MonoBehaviour
{
    private Vector2 normalHeight;
    private bool isColldingWithPlatform = false;
    private bool isCrouch = true;
    public float height;
    public float gravityScale;
    public Rigidbody2D frogsBody;
    public BoxCollider2D froggyCollider;
    [SerializeField] private Animator froggyCrouching;
    [SerializeField] private Animation animationComponent; 
    [SerializeField] private Sprite BonusSprite; 

    private Vector2 originalSize;
    private Vector2 originalOffset;
    private float originalGravityScale;

    AudioManager audioManager;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    // Start is called before the first frame update
    void Start()
    {
        originalSize = froggyCollider.size;
        originalOffset = froggyCollider.offset;
        originalGravityScale = frogsBody.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            // Check each touch
            foreach (Touch touch in Input.touches)
            {
                // Check if the touch began
                if (touch.phase == TouchPhase.Began)
                {
                    if (touch.position.x >= Screen.width / 2)
                    {
                        if (isColldingWithPlatform && isCrouch)
                        {
                            ScaleCollider(height);
                            accelGravityWhileCrouching(gravityScale);
                            audioManager.PlaySFX(audioManager.crouch);
                        }
                    }
                }
                // Check if the touch is moved or stationary
                else if ((touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary) && isCrouch)
                {
                    // Check if the touch is still in the specified area
                    if (touch.position.x >= Screen.width / 2)
                    {
                        //   audioManager.PlaySFX(audioManager.crouch);
                        ScaleCollider(height);                  
                        if (PlayerScript.isBonus && !PlayerScript.isImmune)
                        {
                            froggyCrouching.SetBool("isBonus", true);
                            froggyCrouching.SetBool("isImmune", false);
                            froggyCrouching.SetBool("isCrouching", true);
                            froggyCrouching.SetBool("isJumping", false);
                        }
                        else if (PlayerScript.isImmune && !PlayerScript.isBonus)
                        {
                            froggyCrouching.SetBool("isBonus", false);
                            froggyCrouching.SetBool("isImmune", true);
                            froggyCrouching.SetBool("isCrouching", true);
                            froggyCrouching.SetBool("isJumping", false);
                        }
                        else
                        {
                         
                            froggyCrouching.SetBool("isBonus", false);
                            froggyCrouching.SetBool("isCrouching", true);
                            froggyCrouching.SetBool("isJumping", false);
                        }
                        
                    }
                    else
                    {
                        froggyCollider.size = originalSize;
                        froggyCollider.offset = originalOffset;
                    }
                }
                // Check if the touch ended or canceled
                else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
                {
                    froggyCollider.size = originalSize;
                    froggyCollider.offset = originalOffset;
                    accelGravityWhileCrouching(originalGravityScale);
                    froggyCrouching.SetBool("isCrouching", false);
                }
            }
        }
    }

    void accelGravityWhileCrouching(float gravityScale) { 
        frogsBody.gravityScale = gravityScale;
    }

    void ScaleCollider(float height)
    {
        Vector2 newSize = new Vector2(originalSize.x, height);
        Vector2 sizeDif = newSize - originalSize;
        Vector2 offsetAdjustment = new Vector2(0f, sizeDif.y / 2f);

  /*      Debug.Log(originalOffset + offsetAdjustment);*/
        froggyCollider.size = newSize;
        froggyCollider.offset = originalOffset - offsetAdjustment;

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("actualFloor"))
        {
            isColldingWithPlatform = true;
        }
     /*   Debug.Log(isColldingWithPlatform);*/

    }

}
