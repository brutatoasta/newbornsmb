using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyMovement : MonoBehaviour
{

    private float originalX;
    private float maxOffset = 5.0f;
    private float enemyPatroltime = 2.0f;
    private int moveRight = -1;
    private Vector2 velocity;
    public Vector3 startPosition;
    Animator enemyAnimator;
    private Rigidbody2D enemyBody;
    public PlayerMovement playerMovement;
    public bool alive;
    public UnityEvent increaseScore;
    public UnityEvent damagePlayer;
    void Start()
    {
        enemyAnimator = GetComponent<Animator>();
        enemyBody = GetComponent<Rigidbody2D>();
        // get the starting position
        originalX = transform.position.x;
        startPosition = transform.position;
        ComputeVelocity();
        alive = true;
    }
    void ComputeVelocity()
    {
        velocity = new Vector2((moveRight) * maxOffset / enemyPatroltime, 0);
    }
    void Movegoomba()
    {
        enemyBody.MovePosition(enemyBody.position + velocity * Time.fixedDeltaTime);
    }

    void Update()
    {
        if (Mathf.Abs(enemyBody.position.x - originalX) < maxOffset)
        {
            // move goomba
            // todo uncomment movegoomba
            Movegoomba();
        }
        else
        {
            // change direction
            moveRight *= -1;
            ComputeVelocity();
            Movegoomba();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // damage player, we let mario decides if he dies or if hes supermario he gets downgraded
        if (other.gameObject.CompareTag("Player") && alive)
        {
            damagePlayer.Invoke();
        }
    }
    public void Die()
    {
        alive = false;
        enemyAnimator.SetTrigger("dieTrigger");
        
        // GameManager.instance.IncreaseScore(1);
        increaseScore.Invoke();
    }
    public void DieCallback()
    {
        gameObject.SetActive(false);
    }
    public void GameRestart()
    {
        gameObject.SetActive(true);
        alive = true;
        // todo change animator state
        enemyAnimator.Play("goomba-walk");
        transform.position = startPosition;
        originalX = transform.position.x;
        moveRight = -1;
        ComputeVelocity();
    }
}