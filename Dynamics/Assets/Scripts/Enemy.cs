using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    float startTime;
    int score;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(Random.Range(-8, 8), Random.Range(-4, 4), -3);
        transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
        startTime = Time.time;
        score = 10;
    }

    private float abs(float x)
    {
        if (x < 0)
        {
            return -x;
        }
        return x;
    }
    private float min(float x, float y)
    {
        if (x < y)
        {
            return x;
        }
        return y;
    }
    // Update is called once per frame
    void Update()
    {
        float age = (Time.time - startTime);
        if (age > 1.5)
        {
            Destroy(this.gameObject);
        }
        float size = min(age, 1.25f);
        transform.localScale = new Vector3(size, size, size);
        if (age > 1.1 && age < 1.3)
        {
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            sr.color = new Color(1, 0.843f, 0);
            score = 50;
        } else if (age < 0.9)
        {
            
        } else
        {
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            sr.color = new Color(1, 0.7f, 1);
            score = 30;
        }
    }

    public int getScore()
    {
        return score;
    }
}
