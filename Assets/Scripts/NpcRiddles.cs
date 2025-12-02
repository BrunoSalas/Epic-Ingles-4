using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NpcRiddles : MonoBehaviour
{
    [HideInInspector] public ManagerNpcRiddles Manag_riddle;
    public AudioSource audioSource;
    public AudioClip audio_Riddle;
    public UnityEvent complete;
    public UnityEvent incorrect;
    public List<Response> responses;

    public void AudioInit()
    {
        if(audio_Riddle != null) 
        AudiosResponse(audio_Riddle);
        Manag_riddle.RiddlesUpdate(this);
    }

    public void AudiosResponse(AudioClip clip)
    {
        Manag_riddle.StopsAudio();
        audioSource.clip = clip;
        audioSource.Play();
    }
}
