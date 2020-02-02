using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private const int MAX_INDOOR_SPEED = 4;
    private BoxCollider2D boxCollider;
    private Rigidbody2D rb2D;
    private SpriteRenderer spriteRenderer;
    public bool indoors = true;
    public bool toolSound;
    public AudioClip ladderClip;
    public AudioClip jumpClip;
    public AudioClip itemGetClip;
    public AudioClip footstepClip;

    private bool floor = false;
    private bool ladder = false;
    private Animator animator;
    public GameObject chunk = null;
    public bool chonk = false;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        rb2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        toolSound = false;
    }

    // Update is called once per frame
    void Update()
    {
        int horizontal = 0;
        int vertical = 0;

        horizontal = (int)Input.GetAxisRaw("Horizontal");
        vertical = (int)Input.GetAxisRaw("Vertical");
        Vector3 vel = rb2D.velocity;
        if(!indoors) {
        animator.SetBool("Inside", false);
            SoundManager.instance.PlayerMovement("stop");
            rb2D.velocity = new Vector3(vel.x+Input.GetAxisRaw("Horizontal")/5, vel.y+Input.GetAxisRaw("Vertical")/5, 0);
        if (rb2D.velocity.x <-.2) {
            animator.SetBool("Moving", true);
            spriteRenderer.flipX = true;
        }else if (rb2D.velocity.x >.2) {
            animator.SetBool("Moving", true);
            spriteRenderer.flipX = false;
        }
        } else {
            animator.SetBool("Inside", true);
            rb2D.velocity = new Vector3(vel.x+horizontal>MAX_INDOOR_SPEED?MAX_INDOOR_SPEED:vel.x+horizontal<-1*MAX_INDOOR_SPEED?-1*MAX_INDOOR_SPEED:vel.x+horizontal,floor?18*vertical:vel.y,0);
            if(ladder&&vertical!=0) {
                rb2D.velocity = new Vector3(rb2D.velocity.x,0,0);
                rb2D.MovePosition(new Vector2(rb2D.position.x, rb2D.position.y+Input.GetAxisRaw("Vertical")/13));
                SoundManager.instance.PlayerMovement("ladder");
                print("I got a ladder");
            }
            if (floor && vertical > 0)
            {
                floor = false;
                SoundManager.instance.PlayerMovement("stop");
                SoundManager.instance.MightAsWellJump();
            }
            if (rb2D.velocity.x <-.2) {
                animator.SetBool("Moving", true);
                spriteRenderer.flipX = true;
                SoundManager.instance.PlayerMovement("walk");
            }
            else if (rb2D.velocity.x >.2) {
                animator.SetBool("Moving", true);
                spriteRenderer.flipX = false;
                SoundManager.instance.PlayerMovement("walk");
            } else {

                animator.SetBool("Moving", false);
                if(!ladder || vertical == 0)
                {
                    print("I did this thing");
                    SoundManager.instance.PlayerMovement("stop");
                }
                
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.tag=="Ladder") {
            ladder = true;
        } if(collision.tag=="Welder") {
            collision.gameObject.transform.SetParent(this.transform);
            if(!toolSound)
            {
                SoundManager.instance.ItemGet();
                toolSound = true;
            }

        } if(collision.tag=="ShipChunk" &&!chonk) {
            collision.gameObject.transform.SetParent(this.transform);
            chunk = collision.gameObject;
            chonk = true;
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
