using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreHandler : MonoBehaviour {
    private int lives = 3;
    public GameObject livesText;
    public GameObject livesSubtext;
    private int highscore;
    public GameObject highscoreText;
    public GameObject highscoreSubtext;
    private int score = 0;
    public GameObject scoreText;
    public GameObject scoreSubtext;

    public GameObject retryButton;
    

    // Use this for initialization
    void Start()
    {
        GetStartingHighscore();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void GetStartingHighscore()
    {
        if (PlayerPrefs.HasKey("Highscore"))
        {
            highscore = PlayerPrefs.GetInt("Highscore");
            Debug.Log("Zacetni highscore: " + highscore);
        }
        else
        {
            PlayerPrefs.SetInt("Highscore", 0);
            PlayerPrefs.Save();
        }
        UpdateHighscore();
    }

    private void UpdateHighscore()
    {
        if (score > highscore)
        {
            highscore = score;
            PlayerPrefs.SetInt("Highscore", highscore);
            PlayerPrefs.Save();
            Debug.Log("Score: " + score + " Highscore: " + highscore);
        }
        
        highscoreText.GetComponent<Text>().text = highscore.ToString();
    }

    private void UpdateLives(int change, Transform ball)
    {
        lives -= change;
        livesText.GetComponent<Text>().text = lives.ToString();
        if(lives == 0)
            ShowGameOverLay();
        else
            Destroy(ball.gameObject);
    }


    private void UpdateScore(int change)
    {
        score += change;
        scoreText.GetComponent<Text>().text = score.ToString();
        UpdateHighscore();
    }


    public void ReceiveInfo(Transform ball, GameObject pillar)
    {
        if (ball.tag == pillar.tag)
        {
            UpdateScore(1);
            Destroy(ball.gameObject);
        }
        else
        {
            UpdateLives(1, ball);
            
        }
    }

    public bool ShowGameOverLay()
    {
        if (lives != 0)
            return false;
        else
        {
            retryButton.SetActive(true);
            livesSubtext.SetActive(true);
            highscoreSubtext.SetActive(true);
            scoreSubtext.SetActive(true);
            Time.timeScale = 0;
            return true;
        }
    }
}
