using SpatialSys.UnitySDK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CorrectChoice : MonoBehaviour
{
    private SpatialInteractable interactable;
    public bool completed;
    public UnityEvent onCompleted;

    private void Awake()
    {
        interactable = GetComponent<SpatialInteractable>();
    }

    private void Start()
    {
        interactable.onInteractEvent.unityEvent.AddListener(Complete);
    }
    public void Complete()
    {
        completed = true;
        onCompleted.Invoke();
    }
}
