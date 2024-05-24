using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestItem : MonoBehaviour
{
    public TMP_Text quizTitle;
    public TMP_Text quizDescription;
    public TMP_Text quizReward;


    public void SetupQuest(SaveFileController.Quest quest)
    {
        quizTitle.text = "";
        if (quest.isDone)
        {
            quizTitle.text = "<s>";
        }
        quizTitle.text += $"{quest.title} ({quest.progress}/{quest.target})";
        quizDescription.text = quest.description;
        quizReward.text = "";
        foreach (var reward in quest.rewards)
        {
            quizReward.text += $"{reward.value} {reward.type}\n";
        }
    }
}
