using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrickController : MonoBehaviour, IPowerupController
{
    public Animator brickAnimator;
    Animator powerupAnimator;
    public Sprite disabledBrick;
    public BasePowerup powerup;
    void OnTriggerEnter2D(Collider2D other)
    {
        // self is question, other is mario
        Debug.Log("Hit brick");

        brickAnimator.SetTrigger("jumpTrigger");
        // if there is a powerup
        if (gameObject.transform.childCount > 0)
        {
            // spawn powerup
            powerupAnimator = gameObject.transform.GetChild(0).gameObject.GetComponent<Animator>();
            switch (powerup.type)
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
        // TODO: check if supermario
        bool isSuper = false;
        if (isSuper)
        {
            Disable();
        }
        // disable trigger
        gameObject.GetComponent<EdgeCollider2D>().enabled = false;

    }
    public void Disable()
    {
        // if supermario, change sprite to disabled brick
        gameObject.GetComponent<SpriteRenderer>().sprite = disabledBrick;
    }
}