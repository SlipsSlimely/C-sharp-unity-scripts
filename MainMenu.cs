using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject leaderBoard;
    public GameObject credits;
    public GameObject gameGuide;

    public void PlayGame() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LeaderBoard() 
    {
        mainMenu.SetActive(false);
        leaderBoard.SetActive(true);
        credits.SetActive(false);
        gameGuide.SetActive(false);
    }

    public void Credits()
    {
        mainMenu.SetActive(false);
        leaderBoard.SetActive(false);
        credits.SetActive(true);
        gameGuide.SetActive(false);
    }

    public void Controls()
    {
        mainMenu.SetActive(false);
        leaderBoard.SetActive(false);
        credits.SetActive(false);
        gameGuide.SetActive(true);
    }

    public void Return() 
    {
        mainMenu.SetActive(true);
        leaderBoard.SetActive(false);
        credits.SetActive(false);
        gameGuide.SetActive(false);
    }

    public void ExitGame()
    {
        // May only work in actual builds not in editor, Debug for testing in editor
        Debug.Log("Exit Game");
        Application.Quit();
    }
}
