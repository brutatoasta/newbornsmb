using UnityEngine;

[CreateAssetMenu(fileName = "GameConstants", menuName = "ScriptableObjects/GameConstants", order = 1)]
public class GameConstants : ScriptableObject
{
    // lives
    int maxLives = 10;

    // Mario's movement
    public float speed = 250;
    public float maxSpeed = 5;
    public float upSpeed = 30;
    public float deathImpulse = 10;
    Vector3 marioStartingPosition;

    // Goomba's movement
    float goombaPatrolTime = 2.0f;
    float goombaMaxOffset = 5.0f;
}
