using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dice : MonoBehaviour
{
    public Text hiScore;


    private void Start()
    {
        hiScore.text = PlayerPrefs.GetInt("HighScore").ToString();
    }
}
