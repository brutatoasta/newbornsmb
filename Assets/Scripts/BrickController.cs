using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrickController : BaseBlockController
{
    public override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
        // TODO: check if supermario
        bool isSuper = other.gameObject.GetComponent<PlayerMovement>().isSuper;
        if (isSuper)
        {
            BreakBlock();
        }

    }
    public void BreakBlock()
    {

    }
}