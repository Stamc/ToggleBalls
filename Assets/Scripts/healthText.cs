using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class healthText : MonoBehaviour {
    int health = 3;
    Text canvasHealth;
    public GameObject retryButton;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void LooseHealth(int change)
    {
        health -= change;
        
        canvasHealth = GetComponent<Text>();
        canvasHealth.text = health.ToString();
        

    }

    public bool isDead()
    {
        if (health != 0)
            return false;
        else
        {
            retryButton.SetActive(true);
            Time.timeScale = 0;
            return true;
        }
    }
}
