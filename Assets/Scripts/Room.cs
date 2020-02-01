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
    public List<Damage> damageList;
    public List<Spark> sparkList;
    public bool isOnFire;

    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        renderer = GetComponent<SpriteRenderer>();
        oxygenLevel = 100;
        isOnFire = false;
        damageList = new List<Damage>();
        sparkList = new List<Spark>();
    }

    // Update is called once per frame
    void Update()
    {
        //assess current oxygen level after all leaks
        if(damageList.Count > 0)
        {
            foreach(Damage damage in damageList)
            {
                oxygenLevel -= damage.O2Leakage;
            }
        }

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


        ////TODO: set the color of the room based on the oxygen level 
        //if(oxygenLevel == 100)
        //{
        //    renderer.color = Color.green;
        //}
        //else if(oxygenLevel > 80)
        //{
        //    renderer.color = Color.cyan;
        //}
        //else if(oxygenLevel > 40)
        //{
        //    renderer.color = Color.yellow;
        //}
        //else if(oxygenLevel > 5)
        //{
        //    renderer.color = Color.red;
        //}
        //else
        //{
        //    renderer.color = Color.black;
        //}


    }

    private void OnTriggerEnter2D(Collider2D player)
    {
        //do something like-- 
        //set the player's oxygen level to the room level (if the room's level is higher)
        //display something about the room, maybe? set some kind of room display active to show tools, damage, etc
    }

    //add a damage object to the room
    public void addDamage(Damage damage)
    {
        damageList.Add(damage);

    }

    //add a spark object to the room, up to 2
    public void addSpark(Spark spark)
    {
        if(sparkList.Count < 2)
            sparkList.Add(spark);
    }

}
