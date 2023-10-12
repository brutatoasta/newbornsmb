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
    public static Vector3[] marioStartingPositions = {
        new Vector3(-10.03f, -5.4f, 0.0f), //main menu
        Vector3.zero, //loading screen
        new Vector3(-19.0f, 1.5f, 0.0f), // world 1-1
        
        new Vector3(2.91f, 1.5f, 0.0f), // world 1-2

    };
    public static string[] sceneNames = {
        "MainMenu",
        "LoadingScreen",
        "world_1-1",
        "world_1-2",
    };
    public static string[] sceneDisplayNames = {
        "MainMenu",
        "LoadingScreen",
        "WORLD 1-1",
        "WORLD 1-2",
    };

    public enum ArrayIndex
    {
        main_menu = 0,
        loading_screen = 1,
        world_1_1 = 2,
        world_1_2 = 3
    }

    // Goomba's movement
    float goombaPatrolTime = 2.0f;
    float goombaMaxOffset = 5.0f;
}
