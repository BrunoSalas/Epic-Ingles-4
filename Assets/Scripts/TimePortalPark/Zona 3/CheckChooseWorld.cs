using SpatialSys.UnitySDK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckChooseWorld : MonoBehaviour
{
    public static CheckChooseWorld instance;
    public List<ChooseWorld> chooseWorlds = new List<ChooseWorld>();
    public SpatialQuest quest;
    public int indexQuest;

    private void Awake()
    {
        instance = this;
    }
    public void Check()
    {
        foreach (var item in chooseWorlds)
        {
            if (!item.completed)
            {
                return;
            }
        }
        quest.tasks[indexQuest].CompleteTask();
    }
}
