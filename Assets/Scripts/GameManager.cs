using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;
    public static int score;
    public static bool spawnCircle;
    public static bool playerDead;
    public static bool inGame;

    GameObject rightWall;

    void Start()
    {
        MakeSingleton();
        score = 0;
        inGame = false;
        rightWall = GameObject.Find("RightWall");
    }

    void Update()
    {

    }

    void MakeSingleton()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}

