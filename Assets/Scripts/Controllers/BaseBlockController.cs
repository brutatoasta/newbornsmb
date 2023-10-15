using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class BaseBlockController : MonoBehaviour, IPowerupController
{
    [System.NonSerialized]
    public Animator blockAnimator;
    [System.NonSerialized]
    public Animator powerupAnimator;
    public Sprite disabledBlock;
    public GameObject powerupPrefab;
    public virtual void Awake()
    {
        // other instructions
        GameManager.instance.gameRestart.AddListener(GameRestart);
        blockAnimator = GetComponent<Animator>();
    }
    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        blockAnimator.SetTrigger("jumpTrigger");
        // if there is a powerup
        if (transform.childCount > 0)
        {
            // spawn powerup
            powerupAnimator = transform.GetChild(0).gameObject.GetComponent<Animator>();
            switch (transform.GetChild(0).gameObject.GetComponent<BasePowerup>().type)
            {
                case PowerupType.Coin:
                    powerupAnimator.SetTrigger("coinTrigger");
                    break;
                case PowerupType.MagicMushroom:
                    powerupAnimator.SetTrigger("magicTrigger");
                    break;
                case PowerupType.OneUpMushroom:
                    powerupAnimator.SetTrigger("oneupTrigger");
                    break;
                case PowerupType.StarMan:
                    powerupAnimator.SetTrigger("starTrigger");
                    break;
                default:
                    Debug.Log("something's wrong i can feel it");
                    break;

            }
        }

        // disable trigger
        GetComponent<EdgeCollider2D>().enabled = false;

    }
    public virtual void Disable()
    {
        GetComponent<SpriteRenderer>().sprite = disabledBlock;
        blockAnimator.enabled = false;
    }
    public virtual void GameRestart()
    {
        // reset own animation
        blockAnimator.enabled = true;
        blockAnimator.SetTrigger("defaultTrigger");
        GetComponent<EdgeCollider2D>().enabled = true;
        // reset child
        if (gameObject.transform.childCount == 0)
        {
            SpawnPowerup();
        }
    }
    public virtual void SpawnPowerup()
    {
        var child = Instantiate(powerupPrefab, Vector3.zero, Quaternion.identity);
        child.transform.SetParent(transform);
    }
}