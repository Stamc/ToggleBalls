using UnityEngine;
using System.Collections;

public class adjustCubes : MonoBehaviour {
    public GameObject[] cubes = new GameObject[4];
    int numberOfBalls;
    bool[] middleCube = new bool[4];
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
                Vector3 position = hit.transform.position;
                    
                hit.transform.position = cubes[findMiddleCube()].transform.position;
                cubes[findMiddleCube()].transform.position = position;
                middleCube[findMiddleCube()] = false;
                
                for (int i = 0; i < numberOfBalls; i++)
                {
                    if (cubes[i].transform == hit.transform)
                    {
                        middleCube[i] = true;
                        break;
                    }
                }
            }
        }

	}

    private int findMiddleCube()
    {
        for (int i = 0; i < numberOfBalls; i++)
        {
            if (middleCube[i] == true)
                return (i);
        }
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
            cubes[3].SetActive(true);
            cubes[3].transform.position = new Vector3(4, -Camera.main.orthographicSize + Camera.main.orthographicSize * 2 / 10, 0);
            middleCube[3] = false;
            cubes[4].SetActive(true);
            cubes[4].transform.position = new Vector3(-4, -Camera.main.orthographicSize + Camera.main.orthographicSize * 2 / 10, 0);
            middleCube[4] = false;
        }
    }

}
