using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private Rigidbody2D rb2D;
    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        rb2D.velocity = new Vector2(-.3f, -.3f);
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x<-40) transform.position = new Vector2(transform.position.x+80, transform.position.y);
        if(transform.position.y<-20) transform.position = new Vector2(transform.position.x, transform.position.y+40);
    }
}
