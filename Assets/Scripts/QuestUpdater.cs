using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestUpdater : MonoBehaviour
{
    public string targetQuestId;
    public int progress;

    public void AddProgress()
    {
        SaveFileController.instance.AddQuestProgress(progress, SaveFileController.instance.quests.Find(x=>x.questId == targetQuestId));
    }
}
