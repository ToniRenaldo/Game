using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Events;

public class QuestGiver : MonoBehaviour
{
    // Start is called before the first frame update
    public SaveFileController.Quest quest;
    

    [Header("Icon")]
    public Transform icon;
    public LookAtConstraint lookAtConstraint;
    public bool questSettedUp;
    private void Start()
    {
        if(quest != null)
        {
            var currentQuest = SaveFileController.instance.quests.Find(x => x.questId == quest.questId);
            if (currentQuest != null )
            {
                
            }
            else
            {
                HaveQuest();

            }
        }
        questSettedUp = true;
    }

    public void SetupQuest()
    {
        var currentQuest = SaveFileController.instance.quests.Find(x => x.questId == quest.questId);
        currentQuest.progressActions = quest.progressActions;

        if (currentQuest.isDone)
        {
            for (int i = 0; i <= currentQuest.progress; i++)
            {
                currentQuest.progressActions.Find(x => x.progress == i)?.OnProgressUpdated.Invoke();

            }
            if (GetComponent<NpcController>() != null)
            {
                GetComponent<NpcController>().interacted = true;
            }
            quest = null;
        }
        else
        {
            if (GetComponent<NpcController>() != null)
            {
                GetComponent<NpcController>().OnDialogueDone.Invoke();
                for (int i = 0; i <= currentQuest.progress; i++)
                {
                    currentQuest.progressActions.Find(x => x.progress == i)?.OnProgressUpdated.Invoke();
                }
                GetComponent<NpcController>().interacted = true;
            }
        }
    }
    public void HaveQuest()
    {
        icon.gameObject.SetActive(true);
        lookAtConstraint.enabled = true;

        ConstraintSource cameraSource = new ConstraintSource();
        cameraSource.sourceTransform = Camera.main.transform;
        cameraSource.weight = 1;

        lookAtConstraint.AddSource(cameraSource);
    }


    private void OnDisable()
    {
        icon.gameObject.SetActive(false);
        lookAtConstraint.enabled = false;
    }
    public void SendQuest()
    {
        
        if (quest == null)
            return;
        if (SaveFileController.instance.quests.Find(x => x.questId == quest.questId) !=null)
        {
            return;
        }
        SaveFileController.instance.AddQuest(quest);
        SaveFileController.instance.AddQuestProgress(0, SaveFileController.instance.quests.Find(x => x.questId == quest.questId));

        if(icon != null)
        icon.gameObject.SetActive(false);
        lookAtConstraint.enabled = false;

    }
}
