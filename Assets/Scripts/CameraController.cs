using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform player; // Mario's Transform
    public Transform endLimit; // GameObject that indicates end of map
    private float offsetX; // initial x-offset between camera and Mario
    private float startX; // smallest x-coordinate of the Camera
    private float endX; // largest x-coordinate of the camera
    private float offsetY; // initial y-offset between camera and Mario
    private float startY; // smallest y-coordinate of the Camera
    private float endY; // largest y-coordinate of the camera
    private float viewportHalfWidth;
    private float viewportHalfHeight;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        // get coordinate of the bottomleft of the viewport
        // z doesn't matter since the camera is orthographic
        Vector3 bottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
        viewportHalfWidth = Mathf.Abs(bottomLeft.x - transform.position.x);
        viewportHalfHeight = Mathf.Abs(bottomLeft.y - transform.position.y);
        offsetX = 0;//transform.position.x - player.position.x;
        startX = transform.position.x;
        endX = endLimit.transform.position.x - viewportHalfWidth;
        offsetY = 0;//transform.position.y - player.position.y;
        startY = transform.position.y;
        endY = endLimit.transform.position.y - viewportHalfHeight;

    }

    void Update()
    {
        float desiredX = player.position.x + offsetX;
        float desiredY = player.position.y + offsetY;
        // check if desiredX is within startX and endX
        if (!(desiredX > startX && desiredX < endX))
            desiredX = transform.position.x;
        // check if desiredY is within startY and endY
        if (!(desiredY > startY && desiredY < endY))
            desiredY = transform.position.y;

        transform.position = new Vector3(desiredX, desiredY, transform.position.z);
    }
    public void GameRestart()
    {
        transform.position = new Vector3(-7.569899f, 4.774807f, -10);
    }
}
