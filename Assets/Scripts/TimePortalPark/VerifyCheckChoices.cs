using SpatialSys.UnitySDK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerifyCheckChoices : MonoBehaviour
{
    public List<CheckChoices> checkChoices = new List<CheckChoices>();
    public SpatialQuest quest;
    public int indexTask;
    private void Awake()
    {
        quest = FindObjectOfType<SpatialQuest>();
    }

    public void Verify()
    {
        foreach(var checkChoice in checkChoices)
        {
            if (!checkChoice.completed)
            {
                return;
            }
        }
        quest.tasks[indexTask].CompleteTask();

    }
}
