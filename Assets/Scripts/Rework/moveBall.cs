using UnityEngine;
using System.Collections;

public class moveBall : MonoBehaviour {
    public float speed;
    public float duration = 4.0F;
    private float startTime;
    void Start()
    {
        startTime = Time.time;
    }

	// Update is called once per frame
	void Update () {
        float t = (Time.time - startTime) / (10/speed);
        transform.position = new Vector3(0, Mathf.SmoothStep(6, -4, t), 0);
	}

    public void ChangeSpeed(float newSpeed)
    {
        speed = newSpeed;
    }
}
