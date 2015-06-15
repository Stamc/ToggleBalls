using UnityEngine;
using System.Collections;

public class adjustCubes : MonoBehaviour {
    public GameObject[] cubes = new GameObject[5];
    int numberOfBalls;
    bool[] middleCube = {false, false, false, false, false};
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
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
                        Debug.Log("hit cube:" + hit.transform.tag + " i: " + i + " cubes[i]: " + cubes[i].transform.tag);
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
        if (number == 3)
        {
            numberOfBalls = number;
            cubes[0].transform.position = new Vector3(0, -Camera.main.orthographicSize + Camera.main.orthographicSize * 2 / 10, 0);
            middleCube[0] = true;
            cubes[1].transform.position = new Vector3(2, -Camera.main.orthographicSize + Camera.main.orthographicSize * 2 / 10, 0);
            middleCube[1] = false;
            cubes[2].transform.position = new Vector3(-2, -Camera.main.orthographicSize + Camera.main.orthographicSize * 2 / 10, 0);
            middleCube[2] = false;
        }
        else if (number == 5)
        {
            numberOfBalls = number;
            cubes[3].SetActive(true);
            cubes[3].transform.position = new Vector3(4, -Camera.main.orthographicSize + Camera.main.orthographicSize * 2 / 10, 0);
            middleCube[3] = false;
            cubes[4].SetActive(true);
            cubes[4].transform.position = new Vector3(-4, -Camera.main.orthographicSize + Camera.main.orthographicSize * 2 / 10, 0);
            middleCube[4] = false;
        }
    }

}
