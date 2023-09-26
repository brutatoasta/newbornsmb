using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinController : MonoBehaviour
{

    public Animator coinAnimator;
    public AudioSource coinAudio;
    public AudioClip coinClip;

    public void StopCoinAnimation()
    {
        coinAudio.PlayOneShot(coinClip);
        coinAnimator.enabled = false;
        
    }
}