using SpatialSys.UnitySDK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlaceObject : MonoBehaviour
{
    public Transform pos;
    public UnityEvent onComplete;
    public SpatialQuest quest;
    public int indexQuest;
    private void Awake()
    {
        quest = FindObjectOfType<SpatialQuest>();
    }
    public void Place()
    {
        if (Pick.instance.currentObject != null)
        {
            GameObject obj = Pick.instance.currentObject;
            Pick.instance.Release();
            obj.transform.position = pos.position;
            quest.tasks[indexQuest].CompleteTask();
            onComplete.Invoke();
        }
        
    }
}
