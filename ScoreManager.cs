using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public int score = 0;
    public int targetScore = 50;
    public Text scoreText;
    public Text timeText;
    public int timePerLevel = 60;
    public GameObject youWon;
    public GameObject gameOver;
    public Animator minerAnimator;


    private float clockSpeed = 1f;


    void Awake()
    {
        scoreText.text = ("Score: " + score + "/" + targetScore);
        InvokeRepeating("Clock", 0, clockSpeed);
    }

    private void Update()
    {
        if (score == targetScore)
        {
            CheckGameOver();
        }
    }

    void Clock()
    {
        timePerLevel--;
        timeText.text = ("Time: " + timePerLevel);
        if (timePerLevel == 0)
        {
            CheckGameOver();
        }
    }
    public void AddPoints(int pointScored)
    {
        score += pointScored;
        scoreText.text = ("Score: " + score + "/" + targetScore);
    }
 

    void CheckGameOver()
    {
        if (score >= targetScore)
        {
            Time.timeScale = 0;
            youWon.SetActive(true);
            minerAnimator.speed = 0;
        }
        else
        {
            Time.timeScale = 0;
            minerAnimator.speed = 0;
            gameOver.SetActive(true);
        }
    }
}
