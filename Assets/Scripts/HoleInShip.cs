using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleInShip : MonoBehaviour
{
    GameObject chunk = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.tag=="ShipChunk") {
            collision.gameObject.transform.SetParent(this.transform);
            chunk = collision.gameObject;
            Debug.Log("Chonker!");
        } if(collision.tag=="Welder" && chunk !=null) {
            Destroy(chunk);
            Destroy(this.gameObject);
            Debug.Log("Welder!");
        }
    }
}
