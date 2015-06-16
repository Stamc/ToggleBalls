using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class scoreText : MonoBehaviour {
    int score = 0;
    
	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    Text canvasScore;
    

    void ChangeScore(int change)
    {
        score+= change;
        GameObject.FindWithTag("HighscoreText").SendMessage("CheckHighscore", GameObject.FindWithTag("ScoreText").GetComponent<scoreText>().getScore());
        canvasScore = GetComponent<Text>();
        canvasScore.text = score.ToString();
    }

    public int getScore()
    {
        return (score);
    }
}
