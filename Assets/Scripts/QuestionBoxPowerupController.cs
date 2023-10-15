using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionBoxPowerupController : MonoBehaviour, IPowerupController
{
    Animator questionAnimator;
    Animator powerupAnimator;
    public Sprite disabledBlock;
    public GameObject powerupPrefab;
    void Awake()
    {
        // other instructions
        GameManager.instance.gameRestart.AddListener(GameRestart);
        questionAnimator = GetComponent<Animator>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        questionAnimator.SetTrigger("dieTrigger");
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
    public void Disable()
    {
        StopQuestionAnimation();
    }
    public void StopQuestionAnimation()
    {
        GetComponent<SpriteRenderer>().sprite = disabledBlock;
        questionAnimator.enabled = false;
    }
    public void GameRestart()
    {
        // reset own animation
        Debug.Log("QBPC restart");
        questionAnimator.enabled = true;
        questionAnimator.Play("question-flash");
        // reset child
        if (gameObject.transform.childCount == 0)
        {
            SpawnPowerup();
        }
    }
    public void SpawnPowerup()
    {
        var child = Instantiate(powerupPrefab, Vector3.zero, Quaternion.identity);
        child.transform.SetParent(transform);
    }
}