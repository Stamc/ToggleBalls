using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class highscoreText : MonoBehaviour {
    int highscore;
	// Use this for initialization
	void Start () {
        
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
        canvasHighscore = GetComponent<Text>();
        canvasHighscore.text = highscore.ToString();
	}
    Text canvasHighscore;
	
	// Update is called once per frame
	void CheckHighscore (int score) {
        if (score > highscore)
        {
            highscore = score;
            PlayerPrefs.SetInt("Highscore", highscore);
            PlayerPrefs.Save();
            Debug.Log("Score: " + score + " Highscore: " + highscore);
            canvasHighscore.text = highscore.ToString();
        }
        
	}
}
