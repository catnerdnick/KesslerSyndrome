using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    private float nextDamage;
    private float minTime = 10;
    private float maxTime = 15;
    public GameObject ship;
    public GameObject damageLine;
    public GameObject damageSprite;
    public Fire fire;
    public HoleInShip holeSprite;
    public Text timer;
    public GameObject[] rooms;
    public HealthBar bar;
    private float time = 0f;
    void Start()
    {
        nextDamage = Random.Range(1,1);
     //    SceneManager.UnloadSceneAsync("Menu");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        nextDamage -=Time.fixedDeltaTime;
        if(Time.timeScale >0)timer.text = time.ToString();
        time+=Time.fixedDeltaTime;
        //Debug.Log(timer.text);
        if(nextDamage <0) {
            nextDamage = Random.Range(minTime,maxTime);
            
            if(minTime>1)minTime -=1; if(maxTime>2)maxTime-=1;
            Vector3 newPosition = new Vector3(
                Random.Range(
                    ship.GetComponent<SpriteRenderer>().bounds.min.x,
                    ship.GetComponent<SpriteRenderer>().bounds.max.x),
                Random.Range(
                    ship.GetComponent<SpriteRenderer>().bounds.min.y,
                    ship.GetComponent<SpriteRenderer>().bounds.max.y),
                    4);
            RaycastHit2D[] hits;
            float angle = Random.Range(0,360);
            Quaternion direction = Quaternion.Euler(0, 0, angle+90);
            GameObject spacejunk = Instantiate(damageLine, newPosition, direction);
            Bounds junkBounds = spacejunk.GetComponent<SpriteRenderer>().bounds;
            Vector2 startingPoint = new Vector2(newPosition.x+Mathf.Cos(Mathf.Deg2Rad*angle)*1000, newPosition.y+Mathf.Sin(Mathf.Deg2Rad*angle)*1000);
            Vector2 dir = new Vector2(newPosition.x, newPosition.y) - startingPoint;
            hits = Physics2D.RaycastAll(
                startingPoint,
                dir,
                10000000);
            SoundManager.instance.HitClip();
            ArrayList damages = new ArrayList();
            foreach(RaycastHit2D hite in hits){
                if(hite.collider.tag == "Exterior"||hite.collider.tag == "Floor") {damages.Add(hite);}
            }
            RaycastHit2D hit = (RaycastHit2D)damages[Random.Range(0,damages.Count)];
            if(hit.collider.tag == "Exterior") {damages.Add(hit);
                HoleInShip hole = Instantiate(holeSprite, new Vector3(hit.point.x, hit.point.y, 5), Quaternion.identity);
                GameObject go = Instantiate(damageSprite, new Vector3(hit.point.x, hit.point.y, 5), Quaternion.identity);
                hole.bar = bar;
                go.GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Cos(Mathf.Deg2Rad*angle)/3,Mathf.Sin(Mathf.Deg2Rad*angle));
                bar.loseHealth();
            }if(hit.collider.tag == "Floor") {
                Fire hole = Instantiate(fire, new Vector3(hit.point.x, hit.point.y+.5f, 5), Quaternion.identity);
                hole.bar = bar;
                bar.loseHealth();
            }
        }
    }
}
