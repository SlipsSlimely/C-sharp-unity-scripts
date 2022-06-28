using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public Text restartText;
    //public Text coinCounter;
    public Text timeText;
    public Image scoreText;
    public InputField nameInput;
    public Button submitButton;


    private bool runTimer;
    private bool restart;
    private bool gameOver;

    private float currentTime;

    // Variables that will store the player and the player controller script, which will be used for things such as finding the player's current health.
    private GameObject player;
    private PlayerController playerController;

    // Variables for calculating the final score
    private int coinCount;
    private int timeScore;
    private int finalScore;

    // Get the canvas to access the pause script there.
    public GameObject canvas;
    private bool isPaused;

    // Stops all audio on death
    public GameObject camera;
    private AudioSource musicSource;

    // Valariable containing the name InputField
    public InputField inputField;

    // Variables for getting a new score and adding it to the array of scores
    private static int newScore = 0;
    private static string newName = "";

    // Start is called before the first frame update
    void Start()
    {
        AudioListener.pause = false;
        restartText.gameObject.SetActive(false);
        scoreText.gameObject.SetActive(false);
        nameInput.gameObject.SetActive(false);
        submitButton.gameObject.SetActive(false);

        //coinCounter.text = "";

        coinCount = 0;
        currentTime = 0;
        runTimer = true;
        restart = false;
        gameOver = false;

        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<PlayerController>();

        musicSource = camera.GetComponent<AudioSource>();
    }

    void Update()
    {
        isPaused = canvas.GetComponent<PauseMenu>().paused;

        if (restart && !isPaused)
        {
            restartText.gameObject.SetActive(true);
            scoreText.gameObject.SetActive(true);
            nameInput.gameObject.SetActive(false);
            submitButton.gameObject.SetActive(false);

            if (Input.GetKeyDown(KeyCode.R) && !isPaused)
            {
                //this line of text no longer works but I have an override in so it should
                Application.LoadLevel(Application.loadedLevel);
                Time.timeScale = 1;

            }
        }
        else if (!isPaused && gameOver) 
        {
            scoreText.gameObject.SetActive(true);
            nameInput.gameObject.SetActive(true);
            submitButton.gameObject.SetActive(true);
        }
        else 
        {
            restartText.gameObject.SetActive(false);
            scoreText.gameObject.SetActive(false);
            nameInput.gameObject.SetActive(false);
            submitButton.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (playerController.PlayerHealth <= 0) 
        {
            StartCoroutine(destroyPlayer(2));
        }

        if (runTimer) 
        {
            currentTime += Time.deltaTime;
            DisplayTime(currentTime);
        }


    }

    public void GameOver()
    {
        runTimer = false;

        player.GetComponent<Collider>().enabled = false;

        AudioListener.pause = true;

        //Gonna have it so the final score also posts in the restart box for right now
        timeScore = Mathf.FloorToInt(currentTime / 10);
        restartText.text = "Game Over! (Press R to Restart)";
        gameOver = true;

        // Calculate and set final score
        coinCount = playerController.coinCount;
        finalScore = (timeScore + coinCount);
        scoreText.GetComponentInChildren<Text>().text = "Score: " + finalScore;

        
    }

    void DisplayTime(float time) 
    {
        
        int hours = Mathf.FloorToInt(time / 3600);
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        int milliSeconds = (int)((time * 1000) % 1000);
        timeText.text = string.Format("{0:00}:{1:00}:{2:00}:{3:000}", hours, minutes, seconds, milliSeconds);
    }

    // function for getting the players score and adding it to the leaderboard
    public void getScore()
    {
        newName = inputField.text;
        newScore = finalScore;

        string oldName;
        int oldScore;

        for (int i = 0; i < 7; i++)
        {
            oldName = PlayerPrefs.GetString("Save_" + i + "_Name");
            oldScore = PlayerPrefs.GetInt("Save_" + i + "_Score");

            if (newScore > oldScore)
            {
                PlayerPrefs.SetString("Save_" + i + "_Name", newName);
                PlayerPrefs.SetInt("Save_" + i + "_Score", newScore);
                PlayerPrefs.Save();

                Debug.Log(PlayerPrefs.GetString("Save_" + i + "_Name"));
                Debug.Log(PlayerPrefs.GetInt("Save_" + i + "_Score"));

                newName = oldName;
                newScore = oldScore;
            }
        }

        restart = true;
    }

    IEnumerator destroyPlayer(int seconds)
    {
        GameOver();
        yield return new WaitForSeconds(seconds);
        Destroy(player);
        Time.timeScale = 0;

    }
}
