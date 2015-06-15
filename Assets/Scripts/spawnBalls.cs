using UnityEngine;
using System.Collections;

public class spawnBalls : MonoBehaviour {
    public int numberOfBalls;
    public GameObject[] ballsPrefabs = new GameObject[5];
    public float ballMoveSpeed;
    public int ballsToNextStage;
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

        GameObject ball;
		if (gameTime >= delay) {
            counter++;
            gameTime = 0;
            ball = (GameObject)Instantiate(ballsPrefabs[Random.Range(0,numberOfBalls)], transform.position, Quaternion.identity);
            ball.transform.localScale *= (float)Screen.width / Screen.height;
            if (counter % ballsToSpeedIncrease == 0 && ballMoveSpeed <= maxBallSpeed)
            {
                ballMoveSpeed += 0.5f;
            }
            ball.transform.SendMessage("ChangeSpeed", ballMoveSpeed);
		}
        
	}
}
