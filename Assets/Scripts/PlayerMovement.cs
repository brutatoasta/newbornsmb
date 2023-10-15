using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PlayerMovement : MonoBehaviour
{
    public GameConstants gameConstants;
    float speed;
    float maxSpeed;
    float upSpeed;
    float deathImpulse;
    private bool onGroundState = true;

    private Rigidbody2D marioBody;
    private SpriteRenderer marioSprite;
    private bool faceRightState = true;

    // Animation
    public Animator marioAnimator;

    // Audio
    public AudioSource marioAudio;
    public AudioSource marioDeathAudio;

    // State
    [System.NonSerialized]
    public bool alive = true;
    private bool moving = false;
    private bool jumpedState = false;
    public bool isSuper = false;
    // Collision
    int collisionLayerMask = (1 << 3) | (1 << 6) | (1 << 7);


    public void PlayJumpSound()
    {
        // play jump sound
        marioAudio.PlayOneShot(marioAudio.clip);
    }
    public void PlayDeathImpulse()
    {
        marioBody.AddForce(Vector2.up * deathImpulse, ForceMode2D.Impulse);
    }

    // Start is called before the first frame update
    void Start()
    {
        speed = gameConstants.speed;
        maxSpeed = gameConstants.maxSpeed;
        deathImpulse = gameConstants.deathImpulse;
        upSpeed = gameConstants.upSpeed;
        // Set to be 30 FPS
        Application.targetFrameRate = 30;
        marioBody = GetComponent<Rigidbody2D>();
        marioSprite = GetComponent<SpriteRenderer>();
        // update animator state
        marioAnimator.SetBool("onGround", onGroundState);
    }


    void Awake()
    {
        // other instructions
        // subscribe to Game Restart event
        GameManager.instance.gameRestart.AddListener(GameRestart);
    }


    // Update is called once per frame
    void Update()
    {
        marioAnimator.SetFloat("xSpeed", Mathf.Abs(marioBody.velocity.x));
    }
    void FlipMarioSprite(int value)
    {
        if (value == -1 && faceRightState)
        {
            faceRightState = false;
            marioSprite.flipX = true;
            if (marioBody.velocity.x > 0.05f)
                marioAnimator.SetTrigger("onSkid");

        }

        else if (value == 1 && !faceRightState)
        {
            faceRightState = true;
            marioSprite.flipX = false;
            if (marioBody.velocity.x < -0.05f)
                marioAnimator.SetTrigger("onSkid");
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {   // use tags
        if ((col.gameObject.CompareTag("Ground") || col.gameObject.CompareTag("Enemies") || col.gameObject.CompareTag("Obstacles")) && !onGroundState)


            // use layer mask
            if (((collisionLayerMask & (1 << col.transform.gameObject.layer)) > 0) & !onGroundState)
            {
                onGroundState = true;
                // update animator state
                marioAnimator.SetBool("onGround", onGroundState);
            }
    }

    // FixedUpdate is called 50 times a second
    void FixedUpdate()
    {
        if (alive && moving && !GameManager.instance.isPaused)
        {
            Move(faceRightState == true ? 1 : -1);
        }
    }
    void Move(int value)
    {

        Vector2 movement = new Vector2(value, 0);
        // check if it doesn't go beyond maxSpeed
        if (marioBody.velocity.magnitude < maxSpeed)
            marioBody.AddForce(movement * speed);
    }

    public void MoveCheck(int value)
    {
        if (value == 0)
        {
            moving = false;
        }
        else
        {
            FlipMarioSprite(value);
            moving = true;
            Move(value);
        }
    }

    public void Jump()
    {
        if (alive && onGroundState && !GameManager.instance.isPaused)
        {
            // jump
            marioBody.AddForce(Vector2.up * upSpeed, ForceMode2D.Impulse);
            onGroundState = false;
            jumpedState = true;
            // update animator state
            marioAnimator.SetBool("onGround", onGroundState);

        }
    }

    public void JumpHold()
    {
        if (alive && jumpedState && !GameManager.instance.isPaused)
        {
            // jump higher
            marioBody.AddForce(Vector2.up * upSpeed * 30, ForceMode2D.Force);
            jumpedState = false;

        }
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        // Death
        if (other.gameObject.CompareTag("Enemies") && alive)
        {
            Debug.Log("Collided with goomba!");
        }

    }
    public void Die()
    {
        // play death animation


        if (alive)
        {
            marioAnimator.Play("mario-die");

            alive = false;
            marioDeathAudio.PlayOneShot(marioDeathAudio.clip);
            GameManager.instance.GameOver();
            StartCoroutine(PlayDeathImpulseThenStop());

        }

        // marioDeathAudio.PlayOneShot(marioDeathAudio.clip);

    }
    private IEnumerator PlayDeathImpulseThenStop()
    {
        PlayDeathImpulse();
        yield return new WaitForSeconds(1);
        GameManager.instance.GameOver();


    }
    public void RestartButtonCallback(int input)
    {
        Debug.Log("Restart!");
        // reset everything
        GameRestart();
        // resume time
        Time.timeScale = 1.0f;
    }

    public void GameRestart()
    {
        // reset position
        marioBody.transform.position = GameConstants.marioStartingPositions[GameManager.instance.currentScene];
        marioBody.velocity = new Vector2(0, 0);
        // reset sprite direction
        faceRightState = true;
        marioSprite.flipX = false;

        // reset animation
        marioAnimator.SetTrigger("gameRestart");
        alive = true;
    }

    public void SetStartingPosition(Scene current, Scene next)
    {
        if (next.name == "world_1-2")
        {
            // change the position accordingly in your World-1-2 case
            this.transform.position = new Vector3(2.91f, 1.5f, 0.0f);
        }
        if (next.name == "world_1-1")
        {
            // change the position accordingly in your World-1-2 case
            this.transform.position = new Vector3(-19f, 1.5f, 0.0f);
        }
    }
}