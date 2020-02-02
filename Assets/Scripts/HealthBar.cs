using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    // Start is called before the first frame update
    private int health =10;
    public Sprite[] sprites;
    private SpriteRenderer renderer;
    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
    }

    public void loseHealth() {
        health--;
        renderer.sprite = sprites[10-health];
    }
    public void gainHealth(){
        
        health++;
        renderer.sprite = sprites[10-health];
    }
    // Update is called once per frame
    void Update()
    {
    }
}
