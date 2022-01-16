using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public Text scoreText;
    public Text hiScoreText;

    public void Start()
    {
        this.scoreText.text = "Your score: " + Manager.endPoints;
        this.hiScoreText.text = "High score: " + PlayerPrefs.GetInt("HighScore");

    }
    public void GameMenu()
    {
        SceneManager.LoadScene(0);
    }

}
