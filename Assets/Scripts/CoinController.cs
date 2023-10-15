using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CoinController : BasePowerup
{

    public AudioSource coinAudio;
    public AudioClip coinClip;

    protected override void Start()
    {
        base.Start(); // call base class Start()
        this.type = PowerupType.Coin;
    }
    public override void ApplyPowerup(MonoBehaviour i)
    {
        Debug.Log("collide");
    }

    public override void SpawnPowerup()
    {
        spawned = true;
    }

    public void StopCoinAnimation()
    {

        GameManager.instance.IncreaseScore(1);
        StartCoroutine(PlayAudioThenDestroy());
    }
    private IEnumerator PlayAudioThenDestroy()
    {

        coinAudio.PlayOneShot(coinClip);
        yield return new WaitUntil(() => !coinAudio.isPlaying);
        DestroyPowerup();

    }
}