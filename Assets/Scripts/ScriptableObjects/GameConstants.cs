using UnityEngine;

[CreateAssetMenu(fileName = "GameConstants", menuName = "ScriptableObjects/GameConstants", order = 1)]
public class GameConstants : ScriptableObject
{
    // lives
    int maxLives;

    // Mario's movement
    public float speed = 20;
    public float maxSpeed;
    public float upSpeed;
    public float deathImpulse;
    Vector3 marioStartingPosition;

    // Goomba's movement
    float goombaPatrolTime;
    float goombaMaxOffset;
}
