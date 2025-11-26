using SpatialSys.UnitySDK;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PickIngredients : MonoBehaviour
{
    public static PickIngredients instance;

    public FoodType.Type currentType;

    [Header("UICanvas")]
    public TMP_Text holdingText;

    private void Awake()
    {
        instance = this;
    }
    public void PickUp(FoodType.Type Type)
    {
        currentType = Type;
        holdingText.text = "Holding " + currentType.ToString();
    }
    public void Release()
    {
        currentType  = FoodType.Type.None;
        holdingText.text = "Waiting";
    }
}
