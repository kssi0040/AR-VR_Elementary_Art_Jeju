using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class videoSound : MonoBehaviour
{
    public AudioSource audioSource;

	// Use this for initialization
	void Start ()
    {
        this.GetComponent<VideoPlayer>().audioOutputMode = VideoAudioOutputMode.AudioSource;
        this.GetComponent<VideoPlayer>().SetTargetAudioSource(0, audioSource);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
