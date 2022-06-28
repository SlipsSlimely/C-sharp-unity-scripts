using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public bool paused = false;

    public GameObject pauseMenuUI;
    public Text generalText;
    public Text creditsText;
    public GameObject creditsUI;
    //public GameObject creditsUI;

    void Start()
    {
        pauseMenuUI.SetActive(false);
        creditsUI.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            if (paused)
            {

                Resume();
            }
            else
            {
                Pause();
            }
        }        
    }

    public void Pause() 
    {
        //AudioListener.pause();
        AudioListener.pause = true;
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        paused = true;
    }

    public void Credits()
    {
        creditsUI.SetActive(true);
        pauseMenuUI.SetActive(false);
        generalText.text = "Credits";
        creditsText.text = "This game is made by: " +
            "Paul Bahdouchi " +
            "Katie Comadoll and " +
            "Jakob Jaeger ";



    }

    public void Resume() 
    {
        //musicSource.UnPause();
        AudioListener.pause = false;
        pauseMenuUI.SetActive(false);
        creditsUI.SetActive(false);
        Time.timeScale = 1f;
        paused = false;
    }
    public void MainMenu()
    {
        Resume();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void ExitGame()
    {
        // May only work in actual builds not in editor, Debug for testing in editor
        Debug.Log("Exit Game");
        Application.Quit();
    }
}
