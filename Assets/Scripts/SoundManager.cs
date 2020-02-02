using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource efxSource;
    public AudioSource musicSource;
    public AudioClip airlockQuick;
    public AudioSource playerMovement;
    public AudioClip playerWalk;
    public AudioClip playerLadder;
    public AudioClip playerJump;
    public AudioClip itemGetClip;
    public AudioClip damageClip;
    public AudioClip drillClip;

    public AudioClip song1;
    public AudioClip song2;
    public AudioClip song3;
    public AudioClip[] songs;

    public string lastMove = "stop";
    public static SoundManager instance = null;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        songs = new AudioClip[] { song1, song2, song3};
        musicSource.clip = songs[Random.Range(0, songs.Length)];

        musicSource.loop = true;

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        musicSource.Play();
    }


    public void PlaySingle(AudioClip clip)
    {
        efxSource.clip = clip;
        efxSource.Play();
    }

    public void ChangeMusic(AudioClip clip)
    {
        musicSource.Stop();
        musicSource.clip = clip;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void PlayAirlockQuick()
    {
        PlaySingle(airlockQuick);
    }

    public void MightAsWellJump()
    {
        if(!efxSource.isPlaying)
            PlaySingle(playerJump);
    }
    public void ItemGet()
    {
        PlaySingle(itemGetClip);
    }

    public void HitClip()
    {
        PlaySingle(damageClip);
    }

    public void FixClip()
    {
        PlaySingle(drillClip);
    }

    public void PlayerMovement(string moveType)
    {
        if(moveType == lastMove)
        {
            return;
        }
        //print("movetype " + moveType + " lastmove " + lastMove);
        if (moveType == "stop")
        {
            playerMovement.Stop();// = false; //hopefully, this will stop after the last sound. Otherwise, change this to "Stop"
            lastMove = moveType;
            return;
        }
        else if(moveType == "ladder")
        {
            playerMovement.clip = playerLadder;
        }
        else if(moveType == "walk")
        {
            playerMovement.clip = playerWalk;
        }
        lastMove = moveType;
        playerMovement.loop = true;
        if(!playerMovement.isPlaying)
        playerMovement.Play();
    }
}
