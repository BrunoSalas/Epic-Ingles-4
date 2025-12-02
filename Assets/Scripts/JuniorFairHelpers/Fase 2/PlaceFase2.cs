using System;
using System.Collections;
using System.Collections.Generic;
using SpatialSys.UnitySDK;
using UnityEngine;

public class PlaceFase2 : MonoBehaviour
{
    public TypeFase2.Type objectType;
    public PickFase2 pick;
    public float moveSpeed = 3f;
    [HideInInspector] public bool completed;
    public List<TransformList> positions = new List<TransformList>();
    public SpatialInteractable interactable;
    public void TryPlaceObject()
    {
        if (pick == null)
            return;
        if (pick.currentObject == null) return;

        if (pick.currentObject != null && pick.currentType == objectType)
        {
            StartCoroutine(MoveToPosition(pick.currentObject));
            pick.typeFase2.objects.SetActive(true);
            pick.currentObject.SetActive(false);
            pick.audioSource.Play();
            pick.Release();
        }
        else if (pick.currentObject != null)
        {
            pick.typeFase2.Back();
            pick.Release();
        }
    }
    public void TransformOccuped()
    {
        bool todosOcupados = true;
        for (int i = 0; i < positions.Count; i++)
        {
            if (!positions[i].occuped)   // Si uno NO está ocupado
            {
                todosOcupados = false;
                break;                   // Ya no hace falta seguir revisando
            }
        }

        if (todosOcupados)
        {
            completed = true;
            gameObject.SetActive(false);
            FinalFase2.instance.AreAllReady();
        }
    }
    private IEnumerator MoveToPosition(GameObject obj)
    {
        Transform target = transform;

        for (int i = 0; 0 < positions.Count; i++)
        {
            if (!positions[i].occuped)
            {
                positions[i].occuped = true;
                break;
            }
        }
        TransformOccuped();
        yield return null;
    }
}
[Serializable]
public class TransformList
{
    public Transform transform;
    public bool occuped;
}

