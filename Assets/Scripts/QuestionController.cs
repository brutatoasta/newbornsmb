using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionController : MonoBehaviour
{
    public Animator questionAnimator;
    public AudioSource questionAudio;
    public AudioClip hitQuestionClip;
    void Start()
    {
        questionAnimator = GetComponent<Animator>();
        // coinBody = GetComponent<Rigidbody2D>();
        // coinSprite = GetComponent<SpriteRenderer>();
        // coinAnimator.SetBool("enabled", true);

    }
    void FixedUpdate()
    {

    }

    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // self is question, other is mario
        // run animation
        // questionAnimator.Play("question-die");
        // disable box
        Debug.Log("Hit question");
        questionAnimator.SetTrigger("dieTrigger");
        questionAudio.PlayOneShot(hitQuestionClip);
        // questionAnimator
        // change box sprite

        // spawn coin
        // play sound effect

    }
    void StopAnimation()
    {
        // questionSR.sprite = endSprite;
        questionAnimator.enabled = false;

    }

}