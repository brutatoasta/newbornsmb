using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionController : MonoBehaviour
{
    public Animator questionAnimator;
    public Animator coinAnimator;
    public AudioSource questionAudio;
    public AudioClip hitQuestionClip;
    public Sprite endSprite;
    void Start()
    {
        questionAnimator = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // self is question, other is mario
        if (questionAnimator.enabled)
        {
            Debug.Log("Hit question");
            questionAnimator.SetTrigger("dieTrigger");
            questionAudio.PlayOneShot(hitQuestionClip);

            // spawn coin
            coinAnimator.SetTrigger("jumpTrigger");

        }
    }
    public void StopQuestionAnimation()
    {
        questionAnimator.gameObject.GetComponent<SpriteRenderer>().sprite = endSprite;
        questionAnimator.enabled = false;
    }
    public void StopCoinAnimation()
    {
        coinAnimator.enabled = false;
    }
}