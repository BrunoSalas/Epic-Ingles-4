using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ControllerAudio : MonoBehaviour
{
    public List<NPC> npcs = new List<NPC>();
    public List<AudioSource> sources;
    public Coroutine coroutine;

    public UnityEvent onComplete;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StopAudios()
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }
        for (int i = 0; i < sources.Count; i++)
        {
            sources[i].Stop();
            npcs[i].startDialogue = false;
            
        }
    }

    public void CheckComplete()
    {
        foreach (var item in npcs)
        {
            if (item.completed == false)
            {
                return;
            }
        }

        onComplete.Invoke();
    }
}
