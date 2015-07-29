using UnityEngine;
using System.Collections;

public class moveBall : MonoBehaviour {
    public float speed;
    public float duration = 4.0F;
    private float startTime;
    float endScale = (float)Screen.width / Screen.height;
    void Start()
    {
        startTime = Time.time;
    }

	// Update is called once per frame
	void Update () {
        if (Time.timeScale != 0)
        {
            float t = (Time.time - startTime) / (10 / speed);
            //transform.position = new Vector3(0, Mathf.SmoothStep(6, -5, t), 0);
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(0, -5, 0), speed * Time.deltaTime);
            //transform.localScale *= 1.0f - t; TODO: En mode s takim načinom premikanja žogice.
            if (transform.localScale.x > endScale)
                transform.localScale *= 1.0f - t;
        }
    }

    public void ChangeSpeed(float newSpeed)
    {
        speed = newSpeed;
    }


}
