using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private const int MAX_INDOOR_SPEED = 6;
    private BoxCollider2D boxCollider;
    private Rigidbody2D rb2D;
    private SpriteRenderer spriteRenderer;
    public bool indoors = true;
    public bool welder = false, extinguisher = false, ductTape = false;
    public AudioClip ladderClip;
    public AudioClip jumpClip;
    public AudioClip itemGetClip;
    public AudioClip footstepClip;

    private bool floor = false;
    private bool ladder = false;
    private Animator animator;
    public GameObject chunk = null;
    public GameObject PopupE;
    public Transform welderO, extinguisherO, ductTapeO;
    public GameObject ship;
    public bool chonk = false;
    public float timeSincePickup = 0f;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        rb2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        int horizontal = 0;
        int vertical = 0;
        timeSincePickup+=Time.fixedDeltaTime;
        horizontal = (int)Input.GetAxisRaw("Horizontal");
        vertical = (int)Input.GetAxisRaw("Vertical");
        Vector3 vel = rb2D.velocity;
        if(!indoors) {
        animator.SetBool("Inside", false);
            SoundManager.instance.PlayerMovement("stop");
            rb2D.velocity = new Vector3(vel.x+Input.GetAxisRaw("Horizontal")/5*2, vel.y+Input.GetAxisRaw("Vertical")/5*2, 0);
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
            }
            else if (floor && vertical > 0)
            {
                floor = false;
                SoundManager.instance.PlayerMovement("stop");
                SoundManager.instance.MightAsWellJump();
            }
            else if (rb2D.velocity.x <-.2) {
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
                    SoundManager.instance.PlayerMovement("stop");
                }
                
            }
        }
            if(Input.GetKey("e") && indoors && timeSincePickup > 1)
        {
            if(extinguisher) 
            {
                extinguisherO.SetParent(ship.transform);
                extinguisher = false;

            }
            if(ductTape) 
            {
                ductTapeO.SetParent(ship.transform);
                ductTape = false;

            }
            if(welder)
            {
                welderO.SetParent(ship.transform);
                welder = false;

            }

            

        }

    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.tag=="Ladder") {
            ladder = true;
           // Debug.Log("entered ladder");
        } if(collision.tag=="ShipChunk" &&!chonk) {
            collision.gameObject.transform.SetParent(this.transform);
            collision.gameObject.transform.localPosition = new Vector3(-0.7f, 0.6f, 0);
            chunk = collision.gameObject;
            chonk = true;
            timeSincePickup = 0f;
        } 

        if(collision.tag=="Welder"&& !welder || collision.tag=="DuctTape"&& !ductTape || collision.tag=="FireExtinguisher"&& !extinguisher)
        {
            //instantiate E sequence
            
            GameObject E = Instantiate(PopupE,new Vector3(rb2D.position.x, rb2D.position.y+1f, 5),Quaternion.identity);
            E.gameObject.transform.SetParent(this.transform);
        }
    }

private void OnTriggerStay2D(Collider2D collision) {
            if(Input.GetKey("e"))
        {
           // Debug.Log("you hit e what do you want a medal");

    if(timeSincePickup>1){if(collision.tag=="Welder") {
            welderO = collision.gameObject.transform;
            welderO.SetParent(this.transform);
            welderO.localPosition = new Vector3(1f, 0.6f, 0);
            if(extinguisherO) extinguisherO.SetParent(ship.transform);
            if(ductTapeO) ductTapeO.SetParent(ship.transform);
            ductTape = extinguisher = false;
            if(!welder)
            {
                SoundManager.instance.ItemGet();
                welder = true;
            }
            timeSincePickup = 0f;
        } if(collision.tag=="DuctTape") {
            ductTapeO = collision.gameObject.transform;
            ductTapeO.SetParent(this.transform);
            ductTapeO.localPosition = new Vector3(0.7f, 0.6f, 0);
            if(welderO)welderO.SetParent(ship.transform);
            if(extinguisherO)extinguisherO.SetParent(ship.transform);
            welder = extinguisher = false;
            if(!ductTape)
            {
                SoundManager.instance.ItemGet();
                ductTape = true;
            }
            timeSincePickup = 0f;
        } if(collision.tag=="FireExtinguisher") {
            extinguisherO = collision.gameObject.transform;
            extinguisherO.SetParent(this.transform);
            extinguisherO.localPosition = new Vector3(0.7f, 0.6f, 0);
            if(welderO)welderO.SetParent(ship.transform);
            if(ductTapeO)ductTapeO.SetParent(ship.transform);
            ductTape = welder = false;
            if(!extinguisher)
            {
                SoundManager.instance.ItemGet();
                extinguisher = true;
            }
            timeSincePickup = 0f;
        }}}
    
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
           // Debug.Log("left ladder");
        }
    }
}
