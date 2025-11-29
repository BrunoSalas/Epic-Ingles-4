using SpatialSys.UnitySDK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlatformRiddles : MonoBehaviour
{
    [HideInInspector] public ManagerPlatformRiddles Manag_riddle;
    public AudioSource audioSource;
    public AudioClip audio_Riddle;
    public UnityEvent complete;
    public UnityEvent incorrect;
    public List<ResponsePlatform> responses;

    public SpatialInteractable interactable;
    public bool completed;

    private void Awake()
    {
        interactable = GetComponent<SpatialInteractable>();
    }
    private void Start()
    {
        interactable.onInteractEvent.unityEvent.AddListener(AudioInit);
    }
    public void AudioInit()
    {
        if (audio_Riddle != null)
        {
            AudiosResponse(audio_Riddle);
        }

        Manag_riddle.RiddlesUpdate(this);

    }

    public void AudiosResponse(AudioClip clip)
    {
        Manag_riddle.StopsAudio();
        audioSource.clip = clip;
        audioSource.Play();
    }
}
