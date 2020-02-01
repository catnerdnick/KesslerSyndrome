﻿using UnityEngine;
using System.Collections;


public class ScrollerTest : MonoBehaviour
{
    public string[] intro;
    public float off;
    public float speed = 100;


 public void OnGUI()
 {
     off += Time.deltaTime * speed;
     for (int i = 0; i &lt; intro.Length; i++)
     {
         float roff = (intro.Length*-20) + (i*20 + off);
         float alph = Mathf.Sin((roff/Screen.height)*180*Mathf.Deg2Rad);
         GUI.color = new Color(1,1,1, alph);
         GUI.Label(new Rect(0,roff,Screen.width, 20),intro[i]);
         GUI.color = new Color(1,1,1,1);
     }
 }

}