using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    // Start is called before the first frame update
    public const float timeOnScreen = 1.0f;
    private float timeleft;
    private SpriteRenderer renderer;
    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        timeleft = timeOnScreen;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timeleft-=Time.deltaTime;
        renderer.color = new Color(renderer.color.r,renderer.color.g,renderer.color.b,timeleft/timeOnScreen);
        if(timeleft<=0) Destroy(this);
    }
}
