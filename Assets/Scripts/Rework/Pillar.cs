using UnityEngine;
using System.Collections;

public class Pillar : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter(Collision collision)
    {
        GameObject.FindWithTag("ScoreHandler").transform.GetComponent<ScoreHandler>().ReceiveInfo(collision.transform, gameObject);
    }
}
