using UnityEngine;
using System.Collections;


public class Intro : MonoBehaviour
{
    public string[] intro;
 //   public string[] intro = new string[] { "line 1", "line 2" };
    //   public string[] intro = new string[2];
    //   intro[0] = "line 1";

    public float off;
    public float speed = 10;
    public GUIStyle guiStyle = new GUIStyle(); //create a new variable

    void Start()
    {
        intro = new string[] {
            "A defunct Russian satellite has collided",
            "with and destroyed a functioning U.S.",
            "Iridium commercial satellite. The collision",
            "added more thaan 2,000 pieces of trackable",
            "debris to the inventory of space junk and",
            "is headed towards the MSS."
        };
    }

    public void OnGUI()
    {
        guiStyle.fontSize = 40; //change the font size
        guiStyle.alignment = TextAnchor.MiddleCenter;


        off += Time.deltaTime * speed;
         for (int i = 0; i < intro.Length; i++)
         {
             print("Here I am and i = " + i);
             float roff = (intro.Length*-40) + (i*40 + off);
             float alph = Mathf.Sin((roff/Screen.height)*180*Mathf.Deg2Rad);
             GUI.color = new Color(1,1,1, alph);
             GUI.Label(new Rect(0,roff,Screen.width, 20),intro[i], guiStyle);
             GUI.color = new Color(1,1,1,1);
         }
    }
}