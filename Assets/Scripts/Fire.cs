using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
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
        if(obj && obj.extinguisher) {
            Destroy(this.gameObject);
            SoundManager.instance.ExtinguishClip();
            bar.gainHealth();
        }
    }
}
