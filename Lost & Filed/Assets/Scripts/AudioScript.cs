using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioScript : MonoBehaviour
{
    public AudioSource boardingAnnouncementSource;
    public AudioSource planeTakingOffSource;
    public AudioSource planeLandingSource;
    void Start()
    {
        Invoke("PlayBoardingAnnouncement",Random.Range(10,30f));
    }

    // Update is called once per frame
    void PlayBoardingAnnouncement() {
        boardingAnnouncementSource.Play();
        Invoke("PlayBoardingAnnouncement",Random.Range(45,90f));
    }

    void PlayPlanetakingOff() {
        planeTakingOffSource.Play();
    }
    void PlayLandingSource() {
        planeLandingSource.Play();
    }
}
