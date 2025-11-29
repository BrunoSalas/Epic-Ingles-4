using SpatialSys.UnitySDK;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class ManagerPlatformRiddles : MonoBehaviour
{
    public List<PlatformRiddles> npcs = new List<PlatformRiddles>();
    [HideInInspector] public List<AudioSource> audioSource = new List<AudioSource>();

    public GameObject panel;
    public SpatialQuest quest;
    public int indexTaskCompleted;

    [Header("Audios")]
    public AudioSource audioS;
    public AudioClip correct;
    public AudioClip error;

    void Start()
    {
        foreach (PlatformRiddles npc in npcs)
        {

            npc.Manag_riddle = this;
            audioSource.Add(npc.audioSource);
        }
    }

    public void StopsAudio()
    {
        foreach (AudioSource audioSource in audioSource)
            audioSource.Stop();
    }

    public void RiddlesUpdate(PlatformRiddles npc)
    {
        panel.SetActive(false);
        panel.SetActive(true);
        var respon = npc.responses;
        foreach (ResponsePlatform response in respon)
        {
            response.button.onClick.RemoveAllListeners();
            response.button.GetComponentInChildren<TextMeshProUGUI>().text = response.text;
            if (response.correct)
            {
                response.button.onClick.AddListener(() =>
                {
                    PlaySound(correct);
                    DisableEmpty();
                    npc.complete.Invoke();
                    npc.completed = true;
                    npc.interactable.enabled = false;
                    CheckComplete();
                    if (response.audioClip != null)
                    {
                        npc.AudiosResponse(response.audioClip);
                    }

                });
            }
            else
            {
                response.button.onClick.AddListener(() =>
                {
                    LifeManager.Instance.RestarPoints();
                    PlaySound(error);
                    DisableEmpty();
                    npc.incorrect.Invoke();
                    if (response.audioClip != null)
                    {
                        npc.AudiosResponse(response.audioClip);
                    }

                });

            }
        }
    }

    private void DisableEmpty()
    {
        panel.SetActive(false);
    }

    public void CheckComplete()
    {
        Debug.LogError("ww");
        foreach (var item in npcs)
        {
            if (!item.completed)
            {
                return;
            }
        }
        quest.tasks[indexTaskCompleted].CompleteTask();
    }

    public void PlaySound(AudioClip clip)
    {
        if (audioSource == null)
            return;
        audioS.clip = clip;
        audioS.Play();
    }
}


[Serializable]

public class ResponsePlatform
{
    public string text;
    public Button button;
    public AudioClip audioClip;
    public bool correct;
}