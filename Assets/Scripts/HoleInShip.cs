using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleInShip : MonoBehaviour
{
    GameObject chunk = null;
    public HealthBar bar;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        Player obj = collision.GetComponent<Player>();
        if(obj && obj.chonk && obj.welder) {
            Destroy(obj.chunk);
            Destroy(this.gameObject);
            SoundManager.instance.FixClip();
            obj.chonk=false;
            bar.gainHealth();
        }
    }
}
