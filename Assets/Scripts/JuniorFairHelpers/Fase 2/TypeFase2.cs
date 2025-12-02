using System.Collections;
using System.Collections.Generic;
using SpatialSys.UnitySDK;
using UnityEngine;

public class TypeFase2 : MonoBehaviour
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

    public Type type;
    [HideInInspector] public SpatialInteractable interactable;
    public GameObject objects;

    private Vector3 initialPosition;

    private void Start()
    {
        interactable = GetComponent<SpatialInteractable>();
        initialPosition = transform.localPosition;
    }
    public void Back()
    {
        interactable.enabled = true;
        transform.localPosition = initialPosition;
    }
}
