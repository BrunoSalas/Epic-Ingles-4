using SpatialSys.UnitySDK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectType : MonoBehaviour
{
    public enum Type
    {
        None,
        WelcomeSign,
        Balloons,
        SmallBanner,
        MapBrochure,
        TicketBox,
        DirectionArrow,
        TrashBag,
        InfoBadge,
        SafetyCone,
        DecorativePlant,
        FoodStall,
        GamesStall
    }
    public GameObject originalCopy;
    public Type type = Type.None;
    [HideInInspector] public SpatialInteractable interactable;
    // guarda estado original para ResetTransform
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private Vector3 originalScale;
    public AudioClip clip;

    private void Awake()
    {
        interactable = GetComponent<SpatialInteractable>();
        originalPosition = transform.position;
        originalRotation = transform.rotation;
        originalScale = transform.localScale;
    }
    public void ResetTransform()
    {
        transform.position = originalPosition;
        transform.rotation = originalRotation;
        transform.localScale = originalScale;
        gameObject.SetActive(true);
        if (interactable != null) interactable.enabled = true;
    }

    public void ActivateOriginal()
    {
        originalCopy.SetActive(true);
        gameObject.SetActive(false);
    }
}
