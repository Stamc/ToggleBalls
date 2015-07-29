using UnityEngine;
using System.Collections;

public class moveLife : MonoBehaviour {
    public float speed;
    //public float duration = 4.0F;
    private float startTime;

    Vector3 target;
    
    void Start()
    {
        startTime = Time.time;
        target = transform.position;
        target.y = -5.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale != 0)
        {
            float t = (Time.time - startTime) / (10 / speed);
            //transform.position = new Vector3(0, Mathf.SmoothStep(6, -5, t), 0);
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
            //transform.localScale *= 1.0f - t; TODO: En mode s takim načinom premikanja žogice.

        }
    }
}
