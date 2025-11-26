using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CountDownTimer : MonoBehaviour
{
    public TMP_Text timerText;       
    public float startTime = 600f;   

    private float currentTime;
    private bool isRunning = true;

    public GameObject pots;

    void Start()
    {
        currentTime = startTime;
        UpdateTimerUI();
    }

    void Update()
    {
        if (!isRunning) return;

        currentTime -= Time.deltaTime;

        if (currentTime <= 0)
        {
            currentTime = 0;
            isRunning = false;
            pots.SetActive(false);
        }

        UpdateTimerUI();
    }

    void UpdateTimerUI()
    {
        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);

        timerText.text = $"{minutes:00}:{seconds:00}";
    }

    // Si necesitas reiniciar desde otro script:
    public void ResetTimer()
    {
        currentTime = startTime;
        isRunning = true;
    }

    public void StopTimer()
    {
        isRunning = false;
    }
}
