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
    void Start()
    {
        nextDamage = Random.Range(1,1);
    }

    // Update is called once per frame
    void Update()
    {
        nextDamage -=Time.deltaTime;
        if(nextDamage <0) {
            nextDamage = Random.Range(1,1);
            Vector3 newPosition = new Vector3(
                Random.Range(
                    ship.GetComponent<SpriteRenderer>().bounds.min.x,
                    ship.GetComponent<SpriteRenderer>().bounds.max.x),
                Random.Range(
                    ship.GetComponent<SpriteRenderer>().bounds.min.y,
                    ship.GetComponent<SpriteRenderer>().bounds.max.y),
                    4);
            Instantiate(damageLine, newPosition,Quaternion.Euler(0, 0, Random.Range(0,180)));
            Debug.Log(newPosition);
            
        }
    }
}
