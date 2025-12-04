using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ManagerNpcRiddles : MonoBehaviour
{
    public List<NpcRiddles> npcs = new List<NpcRiddles>();
    [HideInInspector]public List<AudioSource> audioSource = new List<AudioSource>();

    public GameObject panel;
    private int currentcorrectAnswers=0;
    public UnityEvent allRiddlesComplete;

    private void Awake()
    {
        if(panel!=null)
            panel.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
       foreach (NpcRiddles npc in npcs) {

            npc.Manag_riddle = this;
            audioSource.Add(npc.audioSource);
       }
    }
    public void StopsAudio()
    {
        foreach (AudioSource audioSource in audioSource)
                audioSource.Stop();
    }

    public void RiddlesUpdate(NpcRiddles npc)
    {
        panel.SetActive(false);
        panel.SetActive(true);
        var respon = npc.responses;
        foreach(Response response in respon)
        {
            response.button.onClick.RemoveAllListeners();
            string trimmedString = Regex.Replace(response.text, @"\s+", " ");
            response.button.GetComponentInChildren<TextMeshProUGUI>().text = trimmedString;
            if (response.correct)
            {
                response.button.onClick.AddListener(() =>
                {
                    DisableEmpty();
                    npc.complete.Invoke();
                    currentcorrectAnswers++;
                    if (currentcorrectAnswers >= npcs.Count)
                    {
                        allRiddlesComplete?.Invoke();
                    }
                    if(npc.quest != null)
                        npc.quest.tasks[npc.indexQuest].CompleteTask();
                    if (response.audioClip!=null)
                    {
                        npc.AudiosResponse(response.audioClip); 
                    }
                });
            }
            else
            {
                response.button.onClick.AddListener(() =>
                {
                    DisableEmpty();
                    npc.incorrect.Invoke();
                    if (response.audioClip!=null)
                    {
                        npc.AudiosResponse(response.audioClip); 
                    }
                });
                
            }
        }
    }

    private void OnDisable()
    {
        currentcorrectAnswers = 0;
    }

    private void OnEnable()
    {
        currentcorrectAnswers = 0;
    }

    private void DisableEmpty()
    {
        panel.SetActive(false);
    }
}

[Serializable]

public class Response
{
    public string text;
    public Button button;
    public AudioClip audioClip;
    public bool correct;
}
