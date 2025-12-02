using System.Collections;
using System.Collections.Generic;
using SpatialSys.UnitySDK;
using UnityEngine;

public class MissionRiddle : MonoBehaviour
{
    public SpatialQuest quest;


    public void CompleteTask (int i)
    {
        quest.tasks[i].CompleteTask();
    }
}
