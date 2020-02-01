using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutdoorMovement : MonoBehaviour
{
    private const int MAX_INDOOR_SPEED = 30;
    private BoxCollider2D boxCollider;
    private Rigidbody2D rb2D;
    public bool indoors = true;
    private bool floor = false;
    private bool ladder = false;
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
        Vector3 vel = rb2D.velocity;
        if(!indoors)
        rb2D.velocity = new Vector3(vel.x+horizontal, vel.y+vertical, 0);
        else {
            rb2D.velocity = new Vector3(vel.x+horizontal>MAX_INDOOR_SPEED?MAX_INDOOR_SPEED:vel.x+horizontal<-1*MAX_INDOOR_SPEED?-1*MAX_INDOOR_SPEED:vel.x+horizontal,floor?30*vertical:vel.y,0);
            if(ladder&&vertical!=0) {
                rb2D.velocity = new Vector3(rb2D.velocity.x,0,0);
                rb2D.MovePosition(new Vector2(rb2D.position.x, rb2D.position.y+Input.GetAxisRaw("Vertical")/5));
            }
            if(floor&&vertical>0)floor=false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.tag=="Ladder") {
            ladder = true;
        }
    }
    void OnCollisionStay2D(Collision2D other){
        if(other.collider.tag=="Floor") floor=true;
    }
    void OnCollisionExit2D(Collision2D   other){
      if(other.collider.tag == "Floor"){
          floor = false;
      }
    }
    private void OnTriggerExit2D(Collider2D collision) {
        if(collision.tag=="Ladder") {
            ladder = false;
        }
    }
}
