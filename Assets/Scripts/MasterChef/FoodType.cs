using SpatialSys.UnitySDK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodType : MonoBehaviour
{
    public enum Type
    {
        Apple,
        Avocado,
        Banana,
        Basil,
        Bellpepper,
        Blueberries,
        Butter,
        Cheese,
        Chicken,
        Cilantro,
        Egg,
        Garlic,
        Grapes,
        Ham,
        Honey,
        Lettuce,
        Lime,
        Milk,
        Mint,
        Mushroom,
        Oliveoil,
        Onion,
        Orange,
        Oregano,
        Parmesan,
        Pepper,
        Salt,
        SourCream,
        Spaghetti,
        Strawberry,
        Tacoshell,
        Tomato,
        Watermelon,
        Yogurt,
        None
    }
    public Type type;
    [HideInInspector]public SpatialInteractable interactable;

    private void Awake()
    {
        interactable = GetComponent<SpatialInteractable>();
        interactable.interactText = gameObject.name;
    }

    private void Start()
    {
        interactable.onInteractEvent.unityEvent.AddListener(CallPick);
    }

    public void CallPick()
    {
        PickIngredients.instance.PickUp(type);
    }
}
