
using System.Collections;
using System.Collections.Generic;
using SpatialSys.UnitySDK;
using UnityEngine;

public class PickFase2 : MonoBehaviour
{
    public TypeFase2.Type currentType;
    public GameObject currentObject;
    public SpatialInteractable interactable;
    private bool isMoving = false;
    public float moveSpeed = 5f;
    [HideInInspector] public TypeFase2 typeFase2;
    public AudioSource audioSource;

    [Header("Offset")]
    public Vector3 offset = new Vector3(0, 0.5f, 0);



    private void Update()
    {
        if (isMoving)
        {
            if (currentObject == null) return;
            if (SpatialBridge.actorService == null) return;

            Vector3 targetPos = SpatialBridge.actorService.localActor.avatar
                .GetAvatarBoneTransform(HumanBodyBones.Head).position + offset;

            currentObject.transform.position = Vector3.Lerp(
                currentObject.transform.position,
                targetPos,
                Time.deltaTime * moveSpeed
            );



            // Ajuste final si está muy cerca
            if (Vector3.Distance(currentObject.transform.position, targetPos) < 0.05f)
            {
                currentObject.transform.position = targetPos;
                moveSpeed = 20f;
            }
        }
    }
    public TypeFase2 FindType(GameObject obj)
    {
        if (obj.GetComponentInChildren<TypeFase2>())
        {
            return obj.GetComponentInChildren<TypeFase2>();
        }
        return obj.GetComponent<TypeFase2>();
    }
    public void PickUp(GameObject obj)
    {
        if (currentObject != null)
            return;
        currentObject = obj;
        isMoving = true;

        typeFase2 = FindType(obj);
        interactable = typeFase2.interactable;
        interactable.enabled = false;

        if (typeFase2 != null)
        {
            currentType = typeFase2.type;
            Debug.Log($"Agarraste un {currentType}: {obj.name}");
        }
        else
        {
            Debug.LogWarning("El objeto no tiene TypeObject asignado");
        }
    }
    public void Release()
    {
        if (currentObject != null)
        {
            currentObject = null;
            isMoving = false;
        }
    }
}
