using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {

    public GameObject MenuUI;
    public GameObject InGameUI;
    public GameObject DeathUI;

    public Button playBtn;
    public Button restartBtn;
    //public Button soundBtn;

    public Text highScoreText;
    public Text gameScoreText;
    public Text deathScoreText;

    public static UIManager instance;
    private AudioManager audioManager;

    bool isSoundOn = true;

    void Start () {
        //PlayerPrefs.DeleteAll();
        StartScreen();
        instance = this;

        playBtn.onClick.AddListener(GameScreen);
        restartBtn.onClick.AddListener(Restart);
        //soundBtn.onClick.AddListener(SoundFunc);

        audioManager = FindObjectOfType<AudioManager>();
	}
	
	void Update () {
        gameScoreText.text = GameManager.score.ToString();
        deathScoreText.text = gameScoreText.text;
        if (GameManager.playerDead)
        {
            DeathScreen();
        }
	}

    void SoundFunc()
    {
        if (isSoundOn)
        {
            isSoundOn = false;
            AudioListener.volume = 0f;
        }
        else
        {
            isSoundOn = true;
            AudioListener.volume = 1f;
        }
    }

    void GameScreen()
    {
        audioManager.Play("BtnTu");
        GameManager.inGame = true;
        MenuUI.SetActive(false);
        InGameUI.SetActive(true);
    }

    void StartScreen()
    {
        MenuUI.SetActive(true);
        InGameUI.SetActive(false);
        DeathUI.SetActive(false);
        highScoreText.text = "High Score: " + PlayerPrefs.GetInt("highScore", 0).ToString();
    }

    public void DeathScreen()
    {
        MenuUI.SetActive(false);
        InGameUI.SetActive(false);
        DeathUI.SetActive(true);
        if(GameManager.score >= PlayerPrefs.GetInt("highScore", 0))
        {
            PlayerPrefs.SetInt("highScore", GameManager.score);
            deathScoreText.text = "Congrats!!\nNew High Score: " + GameManager.score;
        }
        else
        {
            deathScoreText.text = "Score: " + GameManager.score;
        }
    }

    public void Restart()
    {
        GameManager.playerDead = false;
        audioManager.Play("BtnTu");
        SceneManager.LoadScene(0);
    }


}
