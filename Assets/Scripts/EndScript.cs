using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndScript : MonoBehaviour
{

public Text timeText;
private float rawtime=0;
private int seconds;
    // Start is called before the first frame update
    void Start()
    {
        rawtime = SoundManager.instance.getScore(); //PlayerPrefs.GetFloat("Time");
        int time = (int) rawtime;
        //Debug.Log("raw" + time);
        int mins = time / 60;
        //Debug.Log("maw" + mins);
        time -= 60 * mins;
    //Debug.Log("raw" + time.ToString());
        timeText.text = (mins.ToString("D2") + ":" + time.ToString("D2"));

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("SparkTest");
    }
}
