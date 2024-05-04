using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    private int scoreVal;
    private int highScoreVal;
    
    public GameObject score;
    public GameObject highScore;
    private TextMeshProUGUI scoreText;
    private TextMeshProUGUI highScoreText;
    void Start()
    {
        scoreText = score.GetComponent<TextMeshProUGUI>();
        highScoreText = highScore.GetComponent<TextMeshProUGUI>();
        scoreVal = 0;
        highScoreVal = PlayerPrefs.GetInt("highscore", 0);
        scoreText.text = "Score: " + scoreVal.ToString();
        highScoreText.text = "Highscore: " + highScoreVal.ToString();
    }

    public void ScorePoints(int i){
        scoreVal += i;
        scoreText.text = "Score: " + scoreVal.ToString();
        if(highScoreVal < scoreVal){
            PlayerPrefs.SetInt("highscore", scoreVal);
            highScoreVal = scoreVal;
            highScoreText.text = "Highscore: " + highScoreVal.ToString();
        }
    }
}
