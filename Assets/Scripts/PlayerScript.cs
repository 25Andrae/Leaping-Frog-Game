using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    #region
    public float gravity;
    public Vector2 velocity;
    public float maxXVelocity = 100;
    public float maxAcceleration = 10;
    public float acceleration = 10;
    public float distance = 0;
    public float jumpVelocity = 20;
    public float groundHeight = 10;
    public bool isGrounded = false;

    public bool isHoldingJump = false;
    public float maxHoldJumpTime = 0.4f;
    public float holdJumpTimer = 0.0f;

    public float jumpGroundThreshold = 1;

    private float invincibilityDuration = 3.0f;
    private bool invincibleActive = false;

    public static int multiplier = 1;
    private float scoreMultiplierDuration = 5.0f;
    private bool scoreMultiplierActive = false;

    public static bool isBonus;
    public static bool isImmune;
    [SerializeField] private Animator animator;
    [SerializeField] SpriteRenderer froggySprite;
    [SerializeField] Sprite sprite;
    #endregion

    AudioManager audioManager;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    private void Start()
    {
        isBonus = false;
        isImmune = false;
        froggySprite.sprite = sprite;
        animator.SetBool("isImmune", false);
        animator.SetBool("isBonus", false);
    }
    void Update()
    {
        if (invincibleActive&&!scoreMultiplierActive)
        {
            Debug.Log("Invincible Active");
            isImmune = true;
            isBonus = false;    
            scoreMultiplierActive = false;
            animator.SetBool("isImmune", true);
            animator.SetBool("isBonus", false);

            invincibilityDuration -= Time.deltaTime;

            if (invincibilityDuration <= 0)
            {
                isImmune = false;
                invincibleActive = false;
                animator.SetBool("isImmune", false);
                Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), false);  // Disable collisions between player and enemies
                Debug.Log("Invinsible deactive");
            }
        }
        if (scoreMultiplierActive&&!invincibleActive)
        {
            Debug.Log("Score Multiplied");
            multiplier = 5;
            isImmune = false;
            isBonus = true;
            invincibleActive = false;
            animator.SetBool("isImmune", false);
            animator.SetBool("isBonus", true);

            scoreMultiplierDuration -= Time.deltaTime;

            if (scoreMultiplierDuration <= 0)
            {
                isBonus = false;
                scoreMultiplierActive = false;
                animator.SetBool("isBonus", false);
                multiplier = 1; // Reset multiplier to 1 when score multiplier ends
            }

        }
     
    }

    private void FixedUpdate()
    {
        Vector2 pos = transform.position;

        if (!isGrounded)
        {
            if (isHoldingJump)
            {
                holdJumpTimer += Time.fixedDeltaTime;
                if (holdJumpTimer >= maxHoldJumpTime)
                {
                    isHoldingJump = false;
                }
            }


            pos.y += velocity.y * Time.fixedDeltaTime;
            if (!isHoldingJump)
            {
                velocity.y += gravity * Time.fixedDeltaTime;
            }

            if (pos.y <= groundHeight)
            {
                pos.y = groundHeight;
                isGrounded = true;
            }
        }

        distance += velocity.x * Time.fixedDeltaTime;

        if (isGrounded)
        {
            float velocityRatio = velocity.x / maxXVelocity;
            acceleration = maxAcceleration * (1 - velocityRatio);

            velocity.x += acceleration * Time.fixedDeltaTime;
            if (velocity.x >= maxXVelocity)
            {
                velocity.x = maxXVelocity;
            }
        }
        transform.position = pos;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PowerUpInvincible")
        {
            isBonus = false;
            isImmune = true;
            audioManager.PlaySFX(audioManager.invincible);
            InvincibleOn();
            Destroy(collision.gameObject);
        }
        else if (collision.tag == "PowerUpScoreMultiplier")
        {
            isBonus = true;
            isImmune = false;
            audioManager.PlaySFX(audioManager.bonus);
            ScoreMultiplierOn();
            Destroy(collision.gameObject);
        }
    }

    public void InvincibleOn()
    {   
        invincibleActive = true;
        scoreMultiplierActive = false;
        invincibilityDuration = 3.0f; // Reset duration                                    
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), true);  // Disable collisions between player and enemies
        Debug.Log("Invinsible Active");
    }


    public void ScoreMultiplierOn()
    {        
        invincibleActive = false;
        scoreMultiplierActive = true;
        scoreMultiplierDuration = 5.0f; // Reset duration                                    
    }


}


