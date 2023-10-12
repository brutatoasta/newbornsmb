using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionBoxPowerupController : MonoBehaviour, IPowerupController
{
    public Animator questionAnimator;
    Animator powerupAnimator;
    public Sprite disabledBlock;
    public BasePowerup powerup;
    void OnTriggerEnter2D(Collider2D other)
    {
        questionAnimator.SetTrigger("dieTrigger");
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

        // disable trigger
        gameObject.GetComponent<EdgeCollider2D>().enabled = false;

    }
    public void Disable()
    {
        StopQuestionAnimation();
    }
    public void StopQuestionAnimation()
    {
        questionAnimator.gameObject.GetComponent<SpriteRenderer>().sprite = disabledBlock;
        questionAnimator.enabled = false;
    }
}