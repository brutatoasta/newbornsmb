using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class PlayerCombat : Singleton<PlayerCombat>
{
    public EnemyMovement enemyMovement;
    public PlayerMovement playerMovement;

    public AudioSource audioSource;
    void OnTriggerEnter2D(Collider2D other)
    {
        enemyMovement = other.gameObject.GetComponent<EnemyMovement>();
        if (other.gameObject.CompareTag("Enemies") && playerMovement.alive && enemyMovement.alive)
        {
            Debug.Log("Collided with goomba!");

            enemyMovement.Die();
            audioSource.PlayOneShot(audioSource.clip);

        }

    }
}