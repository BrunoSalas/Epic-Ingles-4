using System.Collections;
using System.Collections.Generic;
using SpatialSys.UnitySDK;
using UnityEngine;
using UnityEngine.Events;

public class PlaceFase4 : MonoBehaviour
{
    public TypeFase4.Type objectType;
    public PickFase4 pick;
    public float moveSpeed = 3f;
    [HideInInspector] public bool completed;
    public UnityEvent finish;
    public SpatialInteractable interactable;
    public void TryPlaceObject()
    {
        if (pick == null)
            return;
        if (pick.currentObject == null) return;

        if (pick.currentObject != null && pick.currentType == objectType)
        {
            pick.typeFase4.objects.SetActive(true);
            pick.audioSource.Play();
            pick.currentObject.SetActive(false);
            pick.Release();
            finish.Invoke();
            interactable.enabled = false;
        }
        else if (pick.currentObject != null)
        {
            pick.typeFase4.Back();
            pick.Release();
        }
    }
}
