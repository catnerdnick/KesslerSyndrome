// https://answers.unity.com/questions/43490/scroll-intro-text.html

using UnityEngine;
using System.Collections;

public class Intro : MonoBehaviour
{
    public string[] intro;
    public float off = Screen.height;
    public float speed = 10000;
    public GUIStyle guiStyle = new GUIStyle(); //create a new variable

    void Start()
    {
        off = Screen.height;
        intro = new string[] {
            "The MSS thrusters are offline waiting for",
            "repair parts on the supply shuttle. Our",
            "astronaut Major Naeta is on the MSS alone",
            "waiting for the replacement crew.",
            " ",
            "A defunct Russian satellite has collided",
            "with and destroyed a functioning U.S.",
            "Iridium commercial satellite. The collision",
            "added more than 2,000 pieces of trackable",
            "debris to the inventory of space junk and",
            "is headed towards the MSS.",
            " ",
            "Your mission is to repair the MSS damage",
            "from the floating debris and keep the MSS",
            "operational until the supply ship arrives.",
        };
    }

    public void OnGUI()
    {
        guiStyle.fontSize = 40; //change the font size
        guiStyle.alignment = TextAnchor.MiddleCenter;

        print("off = " + off);
        off -= Time.deltaTime * speed * 25;
  
        print("off = " + off);
        for (int i = 0; i < intro.Length; i++)
        {
 //           float roff = (intro.Length * -40) + ((i + 1) * 40 + off);
            float roff =  ((i + 1) * 40 + off);
            float alph = Mathf.Sin((roff / Screen.height) * 180 * Mathf.Deg2Rad);

            guiStyle.fontSize = (int)(roff / 10); //change the font size

            GUI.color = new Color(1, 1, 1, alph);
            print("roff = " + roff);
            GUI.Label(new Rect(0, roff, Screen.width, 20), intro[i], guiStyle);
            GUI.color = new Color(1, 1, 1, 1);
        }
    }
}
