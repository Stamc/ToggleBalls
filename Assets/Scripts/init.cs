using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;

public class init : MonoBehaviour {
    int GamesTillNow = 0;
    int GamesPerAd = 2;
	// Use this for initialization
	void Start () {
        if (GameObject.Find("Thing") != null)
            Destroy(GameObject.Find("Thing"));
        else
            transform.name = "Not A Thing";

        DontDestroyOnLoad(transform.gameObject);
        Advertisement.Initialize("48764");
        showAd();
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.timeSinceLevelLoad == 0)
            GamesTillNow++;
        Debug.Log(GamesTillNow);
        if (GamesTillNow % 2 == 0)
            showAd();
	}

    void showAd()
    {
        if (Advertisement.isReady()) 
        { 
            Advertisement.Show(); 
        }
    }
}
