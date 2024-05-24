using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class SaveFileController : MonoBehaviour
{
    // Start is called before the first frame update
    [System.Serializable]
    public class Progress
    {
        public bool prologue;
        public bool tutorial;
        public bool pengamokAwal;
        public bool prologue2;
    }

    [System.Serializable]
    public class Quest
    {
        public string questId;
        public string title;
        public string description;
        public int target;
        public int progress;
        public bool isDone;
        public bool mainQuest;
        public List<ResultCanvasController.Reward> rewards;
    }
    public List<Quest> quests;
    public static SaveFileController instance; 
    public Transform newQuestCanvas;
    public GameObject questNotificationPrefab;

    [Header("Debug")]
    [SerializeField] public List<Quest> debugQuests;
    [SerializeField] int debugIndex;
    [SerializeField] int addedProgress;

    private void Awake()
    {
        instance = this;
    }



    [ContextMenu("Debug Add New Quest")]
    public void DebugAddQuest()
    {
        foreach(var quest in debugQuests)
        {
            AddQuest(quest);
        }
    }
    public void AddQuest(Quest newQuest)
    {
        SendNotification("Misi baru ditambahkan");
        quests.Add(newQuest);
    }

    public async void SendNotification(string message)
    {
        Debug.Log("Pushing Notification");
        GameObject notification = Instantiate(questNotificationPrefab, newQuestCanvas);
        notification.GetComponentInChildren<TMP_Text>().text = message;
        notification.SetActive(true);
        await Task.Delay(3000);
        Destroy(notification);

    }


    [ContextMenu("Add Progress On Debug Quest")]
    public void DebugAddProgress()
    {
        AddQuestProgress(addedProgress , quests[debugIndex]);
    }
    public void AddQuestProgress(int progress, Quest quest)
    {
        quest.progress += progress;
        if (quest.progress == quest.target)
        {
            QuestDone(quest);
        }
        else
        {
            SendNotification("Progres misi ditambahkan");
        }
    }

    public void QuestDone(Quest correspondingQuest)
    {
        SendNotification("Misi Selesai");
        quests.Find(x=>x.questId ==correspondingQuest.questId).isDone = true;
        
        foreach(var reward in correspondingQuest.rewards)
        {
            if(reward.type == "Gold")
            {
                GlobalInventory.instance.gold += reward.value;
            }
        }


    }
}
