using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Room : MonoBehaviour
{
    private BoxCollider2D boxCollider;
    private SpriteRenderer renderer;
    public float oxygenLevel = 100;
//    public List<Tool> toolList;
//    public List<Damage> damageList;
    public List<Spark> sparkList;
    public bool isOnFire;
    //something like public List<Transform> sparkLocations; //keeps track of where the sparks will go when they're generated in this room-- to be set in Inspector

    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        renderer = GetComponent<SpriteRenderer>();
        oxygenLevel = 100;
        isOnFire = false;
        sparkList = new List<Spark>();
    }

    // Update is called once per frame
    void Update()
    {
        //check whether the room catches fire
        if (sparkList.Count > 0)
        {
            foreach (Spark spark in sparkList)
            {
                if (Random.Range(0, 100) < spark.fireChance) //if we rolled lower than the firechance DC
                {
                    isOnFire = true;
                    //TODO: instantiate the fire

                    //now that there's fire, destroy all the sparks
                    foreach (Spark sparky in sparkList)
                    {
                        Destroy(sparky);
                        sparkList.Clear();
                    }
                    break;
                }
            }
        }


        if(isOnFire)
        {
            //think this would be a pretty quick drain per frame
            oxygenLevel -= 0.05f;
        }



        //see if the oxygen level is <=0 and set accordingly
        if (oxygenLevel <= 0)
        {
            oxygenLevel = 0;
            isOnFire = false;
        }

    }

    private void OnTriggerEnter2D(Collider2D player)
    {
        //do something like-- 
        //display something about the room, maybe? set some kind of room display active to show tools, damage, etc
    }

    //add a spark object to the room, up to 2
    public void addSpark(Spark spark)
    {
        if(sparkList.Count < 2)
            sparkList.Add(spark);
    }
}
