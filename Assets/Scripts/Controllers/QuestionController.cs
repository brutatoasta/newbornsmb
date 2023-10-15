using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionController : MonoBehaviour
{
    public Animator questionAnimator;
    public Animator coinAnimator;
    public Sprite endSprite;

    void OnTriggerEnter2D(Collider2D other)
    {
        // self is question, other is mario
        if (questionAnimator.enabled)
        {
            Debug.Log("Hit question");
            // spawn coin
            coinAnimator.enabled = true;
            coinAnimator.SetTrigger("jumpTrigger");
            questionAnimator.SetTrigger("dieTrigger");


        }
    }
    public void StopQuestionAnimation()
    {
        questionAnimator.gameObject.GetComponent<SpriteRenderer>().sprite = endSprite;
        questionAnimator.enabled = false;
    }
}