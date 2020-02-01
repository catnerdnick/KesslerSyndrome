using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateSpark : MonoBehaviour
{
    public GameObject myspark;
    // Start is called before the first frame update
    void Start()
    {
        myspark = Instantiate(myspark, new Vector2(0,0), Quaternion.identity) as GameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
