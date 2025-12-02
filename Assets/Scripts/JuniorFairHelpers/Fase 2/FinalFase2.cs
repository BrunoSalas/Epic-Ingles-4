using System.Collections;
using System.Collections.Generic;
using SpatialSys.UnitySDK;
using UnityEngine;

public class FinalFase2 : MonoBehaviour
{
    public List<PlaceFase2> places = new List<PlaceFase2>();
    public static FinalFase2 instance;
    public SpatialQuest quest;


    private void Start()
    {
        instance = this;
    }

    public void AreAllReady()
    {
        foreach (PlaceFase2 obj in places)
        {
            if (obj == null) continue;


            if (!obj.completed)
                return;
        }
        quest.tasks[1].CompleteTask();
    }
}
