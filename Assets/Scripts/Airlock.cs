using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Airlock : MonoBehaviour
{
    
    private BoxCollider2D[] boxCollider;
    private Rigidbody2D rb2D;
    public AudioClip airlockQuick;
    // Start is called before the first frame update
    void Start()
    {
        
        boxCollider = GetComponents<BoxCollider2D>();
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other) {
        Player obj = other.GetComponent<Player>();
        if(obj) {
        Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector3(0,0,0);
        rb.gravityScale = rb.gravityScale>0?0:3;
        obj.indoors = !obj.indoors;
        SoundManager.instance.PlayAirlockQuick();
        }
    }
}
