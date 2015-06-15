using UnityEngine;
using System.Collections;

public class deathBall : MonoBehaviour {

	// Use this for initialization
	void Start () {

	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == transform.tag)
        {
            GameObject.FindWithTag("ScoreText").SendMessage("ChangeScore", 1);
            
        }
        else
            GameObject.FindWithTag("HealthText").SendMessage("LooseHealth", 1);
        if (!GameObject.FindWithTag("HealthText").transform.GetComponent<healthText>().isDead())
            Destroy(gameObject);
    }

}
