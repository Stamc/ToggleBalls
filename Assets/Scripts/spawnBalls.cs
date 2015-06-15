using UnityEngine;
using System.Collections;

public class spawnBalls : MonoBehaviour {
    public int numberOfBalls;
    public GameObject[] ballsPrefabs = new GameObject[4];

    public float delay;
    
    float gameTime;
	// Use this for initialization
	void Start () {
        gameTime = 0.0f;
        GameObject.FindWithTag("cubes").SendMessage("changeNumberOfBalls", numberOfBalls);
	}
	
    
	// Update is called once per frame
	void Update () {
	    gameTime += Time.deltaTime;

		if (gameTime >= delay) {
			gameTime = 0;
			Instantiate(ballsPrefabs[Random.Range(0,numberOfBalls)], transform.position, Quaternion.identity);
		}
        //TODO: send number of balls
	}
}
