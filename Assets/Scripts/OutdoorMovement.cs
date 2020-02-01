using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutdoorMovement : MonoBehaviour
{
    private const int MAX_INDOOR_SPEED = 30;
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
            rb2D.velocity = new Vector3(rb2D.velocity.x+horizontal>MAX_INDOOR_SPEED?MAX_INDOOR_SPEED:rb2D.velocity.x+horizontal<-1*MAX_INDOOR_SPEED?-1*MAX_INDOOR_SPEED:rb2D.velocity.x+horizontal,rb2D.velocity.y<=0&&vertical>0?30*vertical:rb2D.velocity.y,0);
        }
    }
}
