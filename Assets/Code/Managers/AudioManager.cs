using UnityEngine;
using System.Collections;
using System;

public class AudioManager : MonoBehaviour
{
    private AudioSource source;
    public String currentlyPlayingClip { get; private set; }
    public AudioClip idleSound;
    public AudioClip lowRpm;
    public AudioClip mediumRpm;
    public AudioClip highRpm;
    public AudioClip maxRpm;

    public AudioClip lowRpmOff;
    public AudioClip midRpmOff;
    public AudioClip highRpmOff;

    

    void Awake()
    {

        source = Camera.main.GetComponent<AudioSource>();
    }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playIdleSound()
    {
        //if is playing self then dont play
        if (source.isPlaying && this.currentlyPlayingClip == idleSound.name)
        {
            
        } else
        {
            source.PlayOneShot(idleSound, 1);
            this.currentlyPlayingClip = idleSound.name;
        }
        
    }

    public void playLowRpm()
    {
        if(source.isPlaying && this.currentlyPlayingClip == lowRpm.name)
        {
            
        } else
        {
            source.PlayOneShot(lowRpm, 1);
            this.currentlyPlayingClip = lowRpm.name;
        }
        
    }

    public void playMedRpm()
    {
        if (source.isPlaying && this.currentlyPlayingClip == mediumRpm.name)
        {
            
        } else
        {
            source.PlayOneShot(mediumRpm, 1);
            this.currentlyPlayingClip = mediumRpm.name;
        }        
    }

    public void playHighRpm()
    {
        if (source.isPlaying && this.currentlyPlayingClip == highRpm.name)
        {
            
        } else
        {
            source.PlayOneShot(highRpm, 1);
            this.currentlyPlayingClip = highRpm.name;
        }            
    }

    public void playMaxRpm()
    {
        if (source.isPlaying && this.currentlyPlayingClip == maxRpm.name)
        {
            

        } else
        {
            source.PlayOneShot(maxRpm, 1);
            this.currentlyPlayingClip = maxRpm.name;
        }
            
    }

    public void playMaxRpmOff()
    {
        source.PlayOneShot(highRpmOff, 1);
    }

    public void playMidRpmOff()
    {
        source.PlayOneShot(midRpmOff, 1);
    }

    public void playLowOff()
    {
        source.PlayOneShot(lowRpmOff, 1);
    }
}
