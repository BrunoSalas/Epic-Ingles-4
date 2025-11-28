using SpatialSys.UnitySDK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCompleteTask : MonoBehaviour
{
    public List<PlacementZone> objects = new List<PlacementZone>();
    public SpatialQuest quest;
    public int indexQuest = 0;

    public void AreAllComplete()
    {
        foreach (PlacementZone obj in objects)
        {
            if (obj == null) continue;

            if (!obj.completed)
                return;
        }
        quest.tasks[indexQuest].CompleteTask();
    }
}
