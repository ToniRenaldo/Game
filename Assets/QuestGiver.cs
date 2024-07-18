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
    private void Start()
    {
        if(quest != null)
        {
            var currentQuest = SaveFileController.instance.quests.Find(x => x.questId == quest.questId);
            if (currentQuest != null && currentQuest.isDone)
            {
                quest = null;
            }
            else
            {
                HaveQuest();

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
        SaveFileController.instance.AddQuest(quest);
        icon.gameObject.SetActive(false);
        lookAtConstraint.enabled = false;

    }
}
