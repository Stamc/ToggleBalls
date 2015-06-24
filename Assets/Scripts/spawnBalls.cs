using UnityEngine;
using UnityEditor;
using System.Collections;

public class spawnBalls : MonoBehaviour {
    public int numberOfBalls;
    public GameObject[] ballsPrefabs = new GameObject[5];
    string[,] ballsColors = new string[2, 5] { { "FFAC00", "E83900", "FF00C6", "3900E8", "009CFF" }, { "BF3EFF", "385CE8", "4BFFF5", "38E83E", "FFFA3C" } };
    public float ballMoveSpeed;
    public int ballsToNextStage;
    public float ballSpeedIncrease;
    public int ballsToSpeedIncrease;
    public float maxBallSpeed;

    public float delay;
    int counter = 0;
    float gameTime;
	// Use this for initialization
	void Start () {
        gameTime = 0.0f;
        GameObject.FindWithTag("cubes").SendMessage("changeNumberOfBalls", numberOfBalls);
	}
	
    
	// Update is called once per frame
	void Update () {
	    gameTime += Time.deltaTime;
        if (counter > ballsToNextStage && numberOfBalls != 5)
        {
            numberOfBalls = 5;
            GameObject.FindWithTag("cubes").SendMessage("changeNumberOfBalls", numberOfBalls);
        }
        if (counter == 0)
            changeColors(0);
        else if (counter == 10)
            changeColors(1);

        GameObject ball;
		if (gameTime >= delay) {
            counter++;
            gameTime = 0;
            ball = (GameObject)Instantiate(ballsPrefabs[Random.Range(0,numberOfBalls)], transform.position, Quaternion.identity);
            ball.transform.localScale *= (float)Screen.width / Screen.height;
            if (counter % ballsToSpeedIncrease == 0 && ballMoveSpeed <= maxBallSpeed)
            {
                ballMoveSpeed += ballSpeedIncrease;
            }
            ball.transform.SendMessage("ChangeSpeed", ballMoveSpeed);
		}
        
	}

    void changeColors(int set)
    {
        for (int i = 0; i < 5; i++)
        {
            Renderer rend = ballsPrefabs[i].GetComponent<Renderer>();
            rend.sharedMaterial.SetColor("_Color", HexToRGB(ballsColors[set,i]));
        }

    }

    Color HexToRGB (string hex)
    {
        Color color = new Color();

        string red = hex.Substring(0, 2);
        color.r = (float)int.Parse(red, System.Globalization.NumberStyles.HexNumber) / 255.0f;

        string green = hex.Substring(2, 2);
        color.g = (float)int.Parse(green, System.Globalization.NumberStyles.HexNumber) / 255.0f;

        string blue = hex.Substring(4, 2);
        color.b = (float)int.Parse(blue, System.Globalization.NumberStyles.HexNumber) / 255.0f;

        color.a = 1.0f;
        return color;
    }
}
