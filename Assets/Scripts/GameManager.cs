using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    private float nextDamage;
    public GameObject ship;
    public GameObject damageLine;
    public GameObject damageSprite;
    public GameObject holeSprite;
    public GameObject[] rooms;
    void Start()
    {
        nextDamage = Random.Range(1,1);
    }

    // Update is called once per frame
    void Update()
    {
        nextDamage -=Time.deltaTime;
        if(nextDamage <0) {
            nextDamage = Random.Range(10,15);
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
            foreach(RaycastHit2D hit in hits){
                if(hit.collider.tag == "Exterior") {
                    GameObject hole = Instantiate(holeSprite, new Vector3(hit.point.x, hit.point.y, 5), Quaternion.identity);
                    GameObject go = Instantiate(damageSprite, new Vector3(hit.point.x, hit.point.y, 5), Quaternion.identity);
                    go.GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Cos(Mathf.Deg2Rad*angle)/3,Mathf.Sin(Mathf.Deg2Rad*angle));
                }
            }
        }
    }
}
