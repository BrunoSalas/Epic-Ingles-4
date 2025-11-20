using SpatialSys.UnitySDK;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class OrderedSequence : MonoBehaviour
{
    [Header("Orden correcto (1-based: usa 1..N si prefieres)")]
    public List<int> correctOrder = new List<int>(); // por ejemplo: 1,5,3,4,6,2

    [Header("Renderers de los pilares (el índice corresponde al 0-based en la lista 'pillars')")]
    public List<Renderer> pillars = new List<Renderer>();

    [Header("Colores de emisión por pilar")]
    public List<Color> emissionColors = new List<Color>();

    [Header("Emisión")]
    public float emissionIntensity = 1f;

    [Header("Sonidos")]
    public AudioSource audioSource;
    public AudioClip correctSound;
    public AudioClip failSound;

    [Header("Evento al completar")]
    public UnityEvent OnCompleted;

    private int currentIndex = 0;
    public bool isCompleted;

    public SpatialQuest quest;
    public int indexQuest;

    // internals
    private bool correctOrderNormalized = false;

    void Start()
    {
        NormalizeCorrectOrderIfNeeded();
        TurnOffAllEmissions();
    }

    // Normaliza la lista correctOrder de 1-based a 0-based una sola vez
    void NormalizeCorrectOrderIfNeeded()
    {
        if (correctOrder == null) return;
        if (correctOrderNormalized) return;

        for (int i = 0; i < correctOrder.Count; i++)
        {
            // si ya parece 0-based (ej. contiene un 0) asumimos que está ya normalizado
            if (correctOrder[i] == 0)
            {
                correctOrderNormalized = true;
                return;
            }
        }

        for (int i = 0; i < correctOrder.Count; i++)
        {
            correctOrder[i] = correctOrder[i] - 1; // convierte 1->0, 2->1, etc.
        }

        correctOrderNormalized = true;
    }

    // buttonIndex llega 1-based (según tu intención). Convertimos a 0-based aquí.
    public void RegisterPress(int buttonIndex)
    {
        if (!correctOrderNormalized) NormalizeCorrectOrderIfNeeded();

        int idx = buttonIndex - 1; // convertir a 0-based
        if (idx < 0 || idx >= pillars.Count)
        {
            Debug.LogWarning($"OrderedSequence: buttonIndex {buttonIndex} (convertido a {idx}) fuera de rango (pillars.Count = {pillars.Count}).");
            // reseteamos para evitar desincronía, opcional:
            // ResetSequence();
            return;
        }

        if (currentIndex < 0 || currentIndex >= correctOrder.Count) currentIndex = 0;

        // seguridad: comprobar que correctOrder[currentIndex] esté en rango
        if (correctOrder[currentIndex] < 0 || correctOrder[currentIndex] >= pillars.Count)
        {
            Debug.LogWarning($"OrderedSequence: correctOrder contiene un índice fuera de rango: {correctOrder[currentIndex]} (pillar count {pillars.Count}).");
            ResetSequence();
            return;
        }

        if (idx == correctOrder[currentIndex])
        {
            EnableEmission(pillars[idx], idx);
            currentIndex++;
            PlayCorrectSound();

            if (currentIndex >= correctOrder.Count)
            {
                OnCompleted?.Invoke();
                isCompleted = true;
                if (quest != null)
                {
                    quest.tasks[indexQuest].CompleteTask();
                }
            }
        }
        else
        {
            ResetSequence();
            PlayFailSound();
        }
    }

    void EnableEmission(Renderer rend, int pillarIndex)
    {
        if (rend == null) return;

        Material mat = rend.material;
        mat.EnableKeyword("_EMISSION");

        Color c = Color.white;
        if (pillarIndex >= 0 && pillarIndex < emissionColors.Count)
            c = emissionColors[pillarIndex];

        mat.SetColor("_EmissionColor", c * emissionIntensity);
    }

    void DisableEmission(Renderer rend)
    {
        if (rend == null) return;

        Material mat = rend.material;
        mat.SetColor("_EmissionColor", Color.black);
        mat.DisableKeyword("_EMISSION");
    }

    void TurnOffAllEmissions()
    {
        for (int i = 0; i < pillars.Count; i++)
        {
            DisableEmission(pillars[i]);
        }
    }

    public void ResetSequence()
    {
        currentIndex = 0;
        TurnOffAllEmissions();
    }

    void PlayCorrectSound()
    {
        if (audioSource != null && correctSound != null)
            audioSource.PlayOneShot(correctSound);
    }

    void PlayFailSound()
    {
        if (audioSource != null && failSound != null)
            audioSource.PlayOneShot(failSound);
    }
}
