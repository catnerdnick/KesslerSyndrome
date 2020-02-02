using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Airlock : MonoBehaviour
{
    
    private BoxCollider2D[] boxCollider;
    private BoxCollider2D interiorBC, exteriorBC;
    private Animator interiorAnimator, exteriorAnimator;
    private BoxCollider2D interiorBC2, exteriorBC2;
    private Animator interiorAnimator2, exteriorAnimator2;
    private Rigidbody2D rb2D;
    public AudioClip airlockQuick;
    public GameObject interiorAirlock;
    public GameObject exteriorAirlock;
    public GameObject interiorAirlock2;
    public GameObject exteriorAirlock2;
    // Start is called before the first frame update
    void Start()
    {
        
        boxCollider = GetComponents<BoxCollider2D>();
        interiorBC = interiorAirlock.GetComponent<BoxCollider2D>();
        exteriorBC = exteriorAirlock.GetComponent<BoxCollider2D>();
        interiorAnimator = interiorAirlock.GetComponent<Animator>();
        exteriorAnimator = exteriorAirlock.GetComponent<Animator>();
        interiorBC.enabled = false;
        exteriorBC.enabled = true;
        interiorAnimator.SetBool("open", !interiorBC.enabled);
        exteriorAnimator.SetBool("open", !exteriorBC.enabled);
        interiorBC2 = interiorAirlock2.GetComponent<BoxCollider2D>();
        exteriorBC2 = exteriorAirlock2.GetComponent<BoxCollider2D>();
        interiorAnimator2 = interiorAirlock2.GetComponent<Animator>();
        exteriorAnimator2 = exteriorAirlock2.GetComponent<Animator>();
        interiorBC2.enabled = false;
        exteriorBC2.enabled = true;
        interiorAnimator2.SetBool("open", !interiorBC.enabled);
        exteriorAnimator2.SetBool("open", !exteriorBC.enabled);
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
        interiorBC.enabled = !obj.indoors;
        exteriorBC.enabled = obj.indoors;
        interiorAnimator.SetBool("open", !interiorBC.enabled);
        exteriorAnimator.SetBool("open", !exteriorBC.enabled);
        interiorBC2.enabled = !obj.indoors;
        exteriorBC2.enabled = obj.indoors;
        interiorAnimator2.SetBool("open", !interiorBC.enabled);
        exteriorAnimator2.SetBool("open", !exteriorBC.enabled);
        }
    }
}
