using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 20;
    public float maxSpeed = 20;
    public float upSpeed = 10;
    public float rotateSpeed = 0.1f;
    public float deathImpulse = 50;
    private bool onGroundState = true;

    private Rigidbody2D marioBody;
    private SpriteRenderer marioSprite;
    private bool faceRightState = true;

    //UI
    public Button retry;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public Vector3 originalPosScoreText;
    public Vector3 originalPosRetry;
    public CanvasRenderer image;

    // Animation
    public Animator marioAnimator;

    // Enemy
    public JumpOverGoomba jumpOverGoomba;

    public GameObject enemies;

    // Audio
    public AudioSource marioAudio;
    public AudioClip marioDeath;
    // Camera
    public Transform gameCamera;

    // State
    [System.NonSerialized]
    public bool alive = true;
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
        // Set to be 30 FPS
        Application.targetFrameRate = 30;
        marioBody = GetComponent<Rigidbody2D>();
        marioSprite = GetComponent<SpriteRenderer>();

        gameOverText.enabled = false;
        originalPosScoreText = scoreText.transform.position;
        originalPosRetry = retry.transform.position;
        image.SetAlpha(0.0f);
        // update animator state
        marioAnimator.SetBool("onGround", onGroundState);


    }


    // Update is called once per frame
    void Update()
    {
        // toggle state
        if (Input.GetKeyDown("a") && faceRightState)
        {
            faceRightState = false;
            marioSprite.flipX = true;
            if (marioBody.velocity.x > 0.1f)
                marioAnimator.SetTrigger("onSkid");
        }

        if (Input.GetKeyDown("d") && !faceRightState)
        {
            faceRightState = true;
            marioSprite.flipX = false;
            if (marioBody.velocity.x < -0.1f)
                marioAnimator.SetTrigger("onSkid");

        }
        marioAnimator.SetFloat("xSpeed", Mathf.Abs(marioBody.velocity.x));
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if ((col.gameObject.CompareTag("Ground") || col.gameObject.CompareTag("Enemies") || col.gameObject.CompareTag("Obstacles")) && !onGroundState)
        {
            onGroundState = true;
            // update animator state
            marioAnimator.SetBool("onGround", onGroundState);
        }
    }

    // FixedUpdate is called 50 times a second
    void FixedUpdate()
    {
        if (alive)
        {
            float moveHorizontal = Input.GetAxisRaw("Horizontal");

            if (Mathf.Abs(moveHorizontal) > 0)
            {
                Vector2 movement = new Vector2(moveHorizontal, 0);
                // check if it doesn't go beyond maxSpeed
                if (marioBody.velocity.magnitude < maxSpeed)
                    marioBody.AddForce(movement * speed);

            }

            // stop
            if (Input.GetKeyUp("a") || Input.GetKeyUp("d"))
            {
                // stop
                marioBody.velocity = Vector2.zero;
            }

            // jump
            if (Input.GetKeyDown("space") && onGroundState)
            {
                marioBody.AddForce(Vector2.up * upSpeed, ForceMode2D.Impulse);
                onGroundState = false;
                // update animator state
                marioAnimator.SetBool("onGround", onGroundState);
            }
            // rotate clockwise
            if (Input.GetKeyDown(","))
            {
                // marioBody.transform.Rotate(Vector3.back * rotateSpeed);
                marioBody.AddTorque(rotateSpeed, ForceMode2D.Impulse);

            }
            if (Input.GetKeyDown("."))
            {
                // marioBody.transform.Rotate(Vector3.forward * rotateSpeed);
                marioBody.AddTorque(-1 * rotateSpeed, ForceMode2D.Impulse);

            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Death
        if (other.gameObject.CompareTag("Enemies") && alive)
        {
            Debug.Log("Collided with goomba!");

            // play death animation
            marioAnimator.Play("mario-die");
            marioAudio.PlayOneShot(marioDeath);
            alive = false;
        }
    }

    public void GameOverScene()
    {
        Time.timeScale = 0.0f;
        gameOverText.enabled = true;
        scoreText.transform.position = gameOverText.transform.position + new Vector3(0.0f, -90.0f, 0.0f);
        retry.transform.position = gameOverText.transform.position + new Vector3(0.0f, -210.0f, 0.0f);
        image.SetAlpha(10.0f);
    }

    public void RestartButtonCallback(int input)
    {
        Debug.Log("Restart!");
        // reset everything
        ResetGame();
        // resume time
        Time.timeScale = 1.0f;
    }

    public void ResetGame()
    {
        // reset position
        marioBody.transform.position = new Vector3(-19.0f, 0.5f, 0.0f);

        // reset rotation
        marioBody.transform.rotation = Quaternion.identity;

        // reset sprite direction
        faceRightState = true;
        marioSprite.flipX = false;

        // reset animation
        marioAnimator.SetTrigger("gameRestart");
        alive = true;

        // reset UI
        scoreText.text = "Score: 0";
        scoreText.transform.position = originalPosScoreText;
        retry.transform.position = originalPosRetry;
        image.SetAlpha(0.0f);
        jumpOverGoomba.score = 0;
        gameOverText.enabled = false;

        // reset camera position
        gameCamera.position = new Vector3(0, 0, -10);

        // reset Goomba
        foreach (Transform eachChild in enemies.transform)
        {
            eachChild.transform.localPosition = eachChild.GetComponent<EnemyMovement>().startPosition;
        }

    }
}