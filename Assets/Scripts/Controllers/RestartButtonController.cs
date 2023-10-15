using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RestartButtonController : MonoBehaviour, IInteractiveButton
{
    public UnityEvent gameRestart;
    public void ButtonClick()
    {
        Debug.Log("Onclick restart button");
        gameRestart.Invoke();
    }
}
