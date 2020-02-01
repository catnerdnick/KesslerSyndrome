using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutdoorMovement : MonoBehaviour
{
    private BoxCollider2D boxCollider;
    private Rigidbody2D rb2D;
    public bool indoors = true;
    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        int horizontal = 0;
        int vertical = 0;

        horizontal = (int)Input.GetAxisRaw("Horizontal");
        vertical = (int)Input.GetAxisRaw("Vertical");
        if(!indoors)
        rb2D.velocity = new Vector3(rb2D.velocity.x+horizontal, rb2D.velocity.y+vertical, 0);
        else {
            rb2D.velocity = new Vector3(rb2D.velocity.x+horizontal>5?5:rb2D.velocity.x+horizontal<-5?-5:rb2D.velocity.x+horizontal,rb2D.velocity.y<=0&&vertical>-0?10*vertical:0,0);
        }
    }
}
