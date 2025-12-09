using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CheckChoices : MonoBehaviour
{
    public List<CorrectChoice> choices = new List<CorrectChoice>();
    public bool completed;
    public UnityEvent OnCompleted;
    public void Check()
    {
        foreach (var choice in choices)
        {
            if (!choice.completed)
            {
                return;
            }
        }

        completed = true;
        OnCompleted?.Invoke();
    }
}
