using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HighScore;
using TMPro;


public class ScoreManager : MonoBehaviour
{
    public TMP_InputField inputPlayerName;
    public TMP_Text scoreText;
    string scoreKey = "Score";
    int score;

    public void Start()
    {
      score = PlayerPrefs.GetInt(scoreKey);
      scoreText.text = "Score: " + score + "";
    }

    public void InitGame() {
    // Read the comment for this HS.Init method .
    // This method call should be put into the Start method of some script that runs
    // in the title menu. Call it once when the game launches and you don't have to
    // call it again. If you want to launch the game from another scene, you can 
    // call this method again in the Start method of any script in that scene, since
    // calling this method multiple times doesn't hurt anything.
    HS.Init(this, "TEAM 5"); // you can hard code your game's name
  }

    public void SubmitScore()
    {
        Debug.Log("Submitting Score");
        InitGame();
        HS.SubmitHighScore(this, inputPlayerName.text, score);
    }

}
