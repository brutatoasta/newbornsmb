using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerCombat : MonoBehaviour
{
    public EnemyMovement enemyMovement;
    public PlayerMovement playerMovement;

    public AudioSource audioSource;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemies") && playerMovement.alive && enemyMovement.alive)
        {
            Debug.Log("Collided with goomba!");
            enemyMovement.Die();
            audioSource.PlayOneShot(audioSource.clip);

        }

    }
}