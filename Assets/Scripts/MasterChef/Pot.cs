using SpatialSys.UnitySDK;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Pot : MonoBehaviour
{
    [Header("Configuración")]
    [Tooltip("Ingredientes permitidos que la olla acepta.")]
    public List<FoodType.Type> allowedIngredients = new List<FoodType.Type>();

    [Tooltip("Cantidad máxima de ingredientes a aceptar (ej: 10).")]
    public int capacity = 10;

    [Header("UI / Feedback (opcionales)")]
    [Tooltip("Texto que mostrará el estado (puede estar vacío).")]
    public TMP_Text statusText;

    [Tooltip("Reproducir audio al añadir (opcional).")]
    public AudioSource addSound;

    [Tooltip("Reproducir audio cuando la receta/olla esté completa (opcional).")]
    public AudioSource completeSound;

    [Header("Eventos")]
    [Tooltip("Evento que se dispara cuando se han añadido 'capacity' ingredientes correctos.")]
    public UnityEvent onComplete;

    // Estado interno
    private List<FoodType.Type> addedIngredients = new List<FoodType.Type>();
    private SpatialInteractable interactable;

    public GameObject prefabComplete;
    public Transform pos;
    public SpatialQuest quest;
    public CountDownTimer countDownTimer;

    private void Awake()
    {
        interactable = GetComponent<SpatialInteractable>();
        pos = transform.GetChild(0);
        quest = FindAnyObjectByType<SpatialQuest>();
    }

    private void Start()
    {
        // Cuando alguien interactúe con la olla, llamamos a TryAddIngredient
        interactable.onInteractEvent.unityEvent.AddListener(TryAddIngredient);

        UpdateStatusText();
        
    }

    private void TryAddIngredient()
    {
        if (PickIngredients.instance == null)
        {
            Debug.LogWarning("PickIngredients.instance es null.");
            return;
        }

        FoodType.Type held = PickIngredients.instance.currentType;

        if (held == FoodType.Type.None)
        {
            SetStatus("No hay ingrediente en la mano.");
            return;
        }

        // Olla llena
        if (addedIngredients.Count >= capacity)
        {
            return;
        }

        // No permitido
        if (!allowedIngredients.Contains(held))
        {
            SetStatus("Ingredient not permitted.");
            ResetPot();
            return;
        }

        // ❌ DUPLICADO → reset
        if (addedIngredients.Contains(held))
        {
            SetStatus("Ingredient already used, try again");
            ResetPot();
            return;
        }

        // Añadir
        addedIngredients.Add(held);
        SetStatus("Added: " + held.ToString() + " (" + addedIngredients.Count + "/" + capacity + ")");

        PickIngredients.instance.Release();

        if (addSound != null) addSound.Play();
        CheckIfComplete();

    }

    private void SetStatus(string msg)
    {
        if (statusText != null)
            statusText.text = msg;
        else
            Debug.Log("[Pot] " + msg);
    }

    private void UpdateStatusText()
    {
        if (statusText != null)
        {
            statusText.text = $"Progress: {addedIngredients.Count}/{capacity}";
        }
    }

    public void ResetPot()
    {
        addedIngredients.Clear();
        SetStatus("Ingrediente Incorrecto, intentalo de nuevo");
    }
    public List<FoodType.Type> GetAddedIngredients()
    {
        // devolver copia para seguridad
        return new List<FoodType.Type>(addedIngredients);
    }

    public bool RemoveLastIngredient()
    {
        if (addedIngredients.Count == 0) return false;
        addedIngredients.RemoveAt(addedIngredients.Count - 1);
        SetStatus("Ingrediente removido. " + addedIngredients.Count + "/" + capacity);
        return true;
    }

    public void CheckIfComplete()
    {
        // Cantidad insuficiente
        if (addedIngredients.Count < capacity)
        {
            return;
        }

        // Validar uno por uno
        HashSet<FoodType.Type> seen = new HashSet<FoodType.Type>();

        foreach (var ing in addedIngredients)
        {
            // ingrediente no permitido
            if (!allowedIngredients.Contains(ing))
            {
                return;
            }

            // duplicado
            if (seen.Contains(ing))
            {
                return;
            }

            seen.Add(ing);
        }

        SetStatus("Complete and correct recipe!");
        if (completeSound != null) completeSound.Play();
        onComplete?.Invoke();
        Instantiate(prefabComplete, pos.position, Quaternion.identity);
        quest.tasks[0].CompleteTask();
        countDownTimer.StopTimer();
    }

    private void OnValidate()
    {
        if (capacity < 1) capacity = 1;
    }
}
