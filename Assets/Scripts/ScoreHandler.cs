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


    float ratio = (float)Screen.width / (float)Screen.height;
    float[] lanes = new float[4];
    public GameObject[] powerups = new GameObject[5];
    public int ballsPerPowerup;
    private int ballsSinceLastPowerup = 0;
    // Use this for initialization
    void Start()
    {
        GetStartingHighscore();

        lanes[0] = 2.0f * Camera.main.orthographicSize * ratio / 6.0f + Camera.main.orthographicSize / 30.0f;
        lanes[1] = -2.0f * Camera.main.orthographicSize * ratio / 6.0f - Camera.main.orthographicSize / 30.0f;
        lanes[2] = 2.0f * 2.0f * Camera.main.orthographicSize * ratio / 6.0f + Camera.main.orthographicSize / 30.0f;
        lanes[3] = -2.0f * 2.0f * Camera.main.orthographicSize * ratio / 6.0f - Camera.main.orthographicSize / 30.0f;
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
        if (lives == 0)
        {
            MoveLastBall(ball);
            ShowGameOverLay();
        }
        else
            Destroy(ball.gameObject);

        if (lives < 3 && ballsSinceLastPowerup >= ballsPerPowerup)
        {
            SpawnLife();
            ballsSinceLastPowerup = 0;
        }
    }

    private void SpawnLife()
    {
        Instantiate(powerups[Random.Range(0, 4)], new Vector3(lanes[Random.Range(0, 4)], 6, 0), Quaternion.identity);
    }

    private void MoveLastBall(Transform ball)
    {
        Vector3 location = new Vector3(0,0, ball.transform.position.z);
        Debug.Log("MoveLastBall: " + location + " trenutna: " + ball.transform.position + ball.name);
        ball.transform.Translate(location);
    }


    private void UpdateScore(int change)
    {
        score += change;
        scoreText.GetComponent<Text>().text = score.ToString();
        UpdateHighscore();
    }


    public void ReceiveInfo(Transform ball, GameObject pillar)
    {
        ballsSinceLastPowerup++;
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
