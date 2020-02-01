using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spark : Damage
{
    private Vector2 center;
    private Rigidbody2D rb2D;
    private float time = 0f;
    private const float timeToTwitch = .15f;
    // Start is called before the first frame update
    void Start()
    {
        center = GetComponent<Rigidbody2D>().position;
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        time +=Time.deltaTime;
        if(time>timeToTwitch){
            //rb2D.MovePosition(new Vector2(rb2D.position.x+10, rb2D.position.y+10));
            rb2D.MovePosition(new Vector2(center.x+Random.Range(-.1f,.1f), center.y+Random.Range(-.1f,.1f)));
            time -=timeToTwitch;
            Debug.Log(rb2D.position);
        }
    }
}
