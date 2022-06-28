using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderBoard : MonoBehaviour
{
    // Array of score slots
    public GameObject[] scoreSlots;

    private GameObject childOne;
    private GameObject childTwo;

    private string scoreName;
    private string scoreValue;


    void Start()
    {
        IntitalizeScores();
    }

    public void IntitalizeScores() 
    {
        // Initials player prefs if there nothing to load, otherwise load player prefs for scoring
        if (!PlayerPrefs.HasKey("Save_0_Name"))
        {
            for (int i = 0; i < 7; i++)
            {
                PlayerPrefs.SetString("Save_" + i + "_Name", "AAAA");
                PlayerPrefs.SetInt("Save_" + i + "_Score", 0);
            }
            PlayerPrefs.Save();

        }

        loadScores();
    }

    public void EraseScores() 
    {
        PlayerPrefs.DeleteAll();
        IntitalizeScores();
    }

    public void loadScores() 
    {
        for (int i = 0; i < 7; i++)
        {
            childOne = scoreSlots[i].transform.GetChild(0).gameObject;
            childTwo = scoreSlots[i].transform.GetChild(1).gameObject;
            scoreName = PlayerPrefs.GetString("Save_" + i + "_Name");
            scoreValue = PlayerPrefs.GetInt("Save_" + i + "_Score").ToString();

            childOne.GetComponent<Text>().text = scoreName;
            childTwo.GetComponent<Text>().text = scoreValue;
        }
    }
}
