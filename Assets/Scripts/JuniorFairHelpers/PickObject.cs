using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpatialSys.UnitySDK;

public class PickObject : MonoBehaviour
{
    public static PickObject Instance { get; private set; }

    public ObjectType.Type currentType;
    public GameObject currentObject;
    public SpatialInteractable interactable;
    private bool isMoving = false;
    public float moveSpeed = 5f;
    public AudioSource audioSource;

    [Header("Offset")]
    public Vector3 offset = new Vector3(0, 0.5f, 0);

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;
    }

    private void Update()
    {

        if (currentObject == null)
        {
            return;
        }

        Vector3 targetPos = SpatialBridge.actorService.localActor.avatar
            .GetAvatarBoneTransform(HumanBodyBones.Head).position + offset;

        currentObject.transform.position = Vector3.Lerp(
            currentObject.transform.position,
            targetPos,
            Time.deltaTime * moveSpeed
        );

        //if (SpatialBridge.cameraService != null)
        //{
        //    currentObject.transform.LookAt(SpatialBridge.cameraService.position);

        //    currentObject.transform.Rotate(0, 180f, 0);
        //}

        // Ajuste final si está muy cerca
        if (Vector3.Distance(currentObject.transform.position, targetPos) < 0.05f)
        {
            currentObject.transform.position = targetPos;
            moveSpeed = 20f;
        }
    }

    public void PickUp(GameObject obj)
    {
        if (currentObject != null)
        {
            ChangeObject();
        }

        currentObject = obj;
        obj.SetActive(true);
        isMoving = true;

        ObjectType typeObj = obj.GetComponent<ObjectType>();
        interactable = typeObj != null ? typeObj.interactable : null;
        audioSource.clip = typeObj.clip;
        audioSource.Play();
        if (interactable != null) interactable.enabled = false;

        if (typeObj != null)
        {
            currentType = typeObj.type;
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

    public void ChangeObject()
    {
        if (currentObject != null)
        {
            ObjectType typeObj = currentObject.GetComponent<ObjectType>();
            if (typeObj != null)
            {
                typeObj.ResetTransform();
            }

            if (interactable != null)
            {
                interactable.enabled = true;
            }

            currentObject = null;
            isMoving = false;
        }
    }

    public GameObject GetCurrentObject()
    {
        return currentObject;
    }
}
