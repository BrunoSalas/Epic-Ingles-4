using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIMessageController : MonoBehaviour
{
    public TMP_Text text;
    public float duration = 3f;
    private Coroutine currentRoutine;

    public void WriteMessage(string message)
    {
        if (currentRoutine != null)
            StopCoroutine(currentRoutine);

        currentRoutine = StartCoroutine(ShowMessageRoutine(message, duration));
    }

    private IEnumerator ShowMessageRoutine(string message, float duration)
    {
        text.text = message;
        yield return new WaitForSeconds(duration);
        text.text = "";
        currentRoutine = null;
    }
}
