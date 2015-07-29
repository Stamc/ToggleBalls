using UnityEngine;
using System.Collections;

public class Manager : MonoBehaviour {
    public int numberOfBalls;

    public GameObject[] ballsPrefabs = new GameObject[5];

    string[,] ballsColors = new string[2, 5] { { "FFAC00", "E83900", "FF00C6", "3900E8", "009CFF" }, 
                                               { "BF3EFF", "385CE8", "4BFFF5", "38E83E", "FFFA3C" } };

    public float ballMoveSpeed;
    public float ballSpeedIncrease;
    public int ballsToSpeedIncrease;
    public float maxBallSpeed;

    public int ballsToNextStage;
    public float delay;
    int counter = 0;
    float gameTime;

    public GameObject[] cubes = new GameObject[5];
    bool[] middleCube = { false, false, false, false, false };

	// Use this for initialization
	void Start () {
        gameTime = 0.0f;
        changeNumberOfBalls(numberOfBalls);
	}
	
	// Update is called once per frame
	void Update () {
        SpawnBalls();
        ChangeColors();
        PlayerInput();
	}

    void SpawnBalls()
    {
        gameTime += Time.deltaTime;
        if (counter > ballsToNextStage && numberOfBalls != 5)
        {
            numberOfBalls = 5;
            changeNumberOfBalls(numberOfBalls);
        }

        GameObject ball;
        if (gameTime >= delay)
        {
            counter++;
            gameTime = 0;
            ball = (GameObject)Instantiate(ballsPrefabs[Random.Range(0, numberOfBalls)], transform.position, Quaternion.identity);
            ball.transform.localScale *= (float)Screen.width / Screen.height;
            if (counter % ballsToSpeedIncrease == 0 && ballMoveSpeed <= maxBallSpeed)
            {
                ballMoveSpeed += ballSpeedIncrease;
            }
            ball.transform.SendMessage("ChangeSpeed", ballMoveSpeed);
        }

    }

    void ChangeColors()
    {
        int set = 0;
        if (counter == 0)
            set = 0;
        else if (counter == ballsToNextStage)
            set = 1;
        else
            return;
        for (int i = 0; i < 5; i++)
        {
            Renderer rend = ballsPrefabs[i].GetComponent<Renderer>();
            rend.sharedMaterial.SetColor("_Color", HexToRGB(ballsColors[set, i]));
        }
    }

    private Color HexToRGB(string hex)
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

    void PlayerInput()
	{
		if (Input.GetMouseButtonDown(0) && Time.timeScale != 0.0f)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.IsChildOf(gameObject.transform))
                {
                    Vector3 hitPosition = hit.transform.position;
                    //hit cubes goes to position of middle cube    
                    hit.transform.position = cubes[findMiddleCube()].transform.position;
                    //middle cube goes to the position of hit cube
                    cubes[findMiddleCube()].transform.position = hitPosition;
                    //we assign the middle cube a false
                    middleCube[findMiddleCube()] = false;

                    //we find the hit cube and set its middle flag to true
                    for (int i = 0; i < numberOfBalls; i++)
                    {
                        //Debug.Log("hit cube:" + hit.transform.tag + " i: " + i + " cubes[i]: " + cubes[i].transform.tag);
                        if (cubes[i].transform.tag == hit.transform.tag)
                        {
                            middleCube[i] = true;
                            break;
                        }
                    }
                }
            }
        }
	}

    private int findMiddleCube()
    {
        //Debug.Log(numberOfBalls);
        for (int i = 0; i < numberOfBalls; i++)
        {
            if (middleCube[i] == true)
                return (i);
        }
        Debug.LogWarning("Returned -1 in findMiddleCube");
        Debug.Break();
        return (-1);
    }

    public void changeNumberOfBalls(int number)
    {
        float ratio = (float)Screen.width / (float)Screen.height;
        //Debug.Log("Ratio: " + ratio);
        if (number == 3)
        {
            numberOfBalls = number;
            cubes[0].transform.position = new Vector3(0, -Camera.main.orthographicSize + (8.0f / 5.0f) * Camera.main.orthographicSize / 10, 0);
            cubes[0].transform.localScale = new Vector3(cubes[0].transform.localScale.x * ratio * 1.2f, cubes[0].transform.localScale.y, cubes[0].transform.localScale.z);
            middleCube[0] = true;
            cubes[1].transform.position = new Vector3(2.0f * Camera.main.orthographicSize * ratio / 6.0f + Camera.main.orthographicSize / 30.0f, -Camera.main.orthographicSize + Camera.main.orthographicSize / 10, 0);
            cubes[1].transform.localScale = new Vector3(cubes[1].transform.localScale.x * ratio * 1.2f, cubes[1].transform.localScale.y, cubes[1].transform.localScale.z);
            middleCube[1] = false;
            cubes[2].transform.position = new Vector3(-2.0f * Camera.main.orthographicSize * ratio / 6.0f - Camera.main.orthographicSize / 30.0f, -Camera.main.orthographicSize + Camera.main.orthographicSize / 10, 0);
            cubes[2].transform.localScale = new Vector3(cubes[2].transform.localScale.x * ratio * 1.2f, cubes[2].transform.localScale.y, cubes[2].transform.localScale.z);
            middleCube[2] = false;
        }
        else if (number == 5)
        {
            numberOfBalls = number;
            cubes[3].SetActive(true);
            cubes[3].transform.position = new Vector3(2.0f * 2.0f * Camera.main.orthographicSize * ratio / 6.0f + Camera.main.orthographicSize / 30.0f, -Camera.main.orthographicSize + Camera.main.orthographicSize / 10, 0);
            cubes[3].transform.localScale = new Vector3(cubes[3].transform.localScale.x * ratio * 1.2f, cubes[3].transform.localScale.y, cubes[3].transform.localScale.z);
            middleCube[3] = false;
            cubes[4].SetActive(true);
            cubes[4].transform.position = new Vector3(-2.0f * 2.0f * Camera.main.orthographicSize * ratio / 6.0f - Camera.main.orthographicSize / 30.0f, -Camera.main.orthographicSize + Camera.main.orthographicSize / 10, 0);
            cubes[4].transform.localScale = new Vector3(cubes[4].transform.localScale.x * ratio * 1.2f, cubes[4].transform.localScale.y, cubes[4].transform.localScale.z);
            middleCube[4] = false;
        }
    }
}
