using Cinemachine;
using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public class NelayanController : MonoBehaviour
{
    public int targetGold;
    public List<string> oldDialogue;
    public List<string> newDialogue;
    public UnityEvent OnDialogueDoneOld;
    public UnityEvent OnDialogueDoneAfterOld;

    public UnityEvent OnDialogueDone;
    public bool targetAchieved;

    public GameObject pulau1;
    public Transform TravelPointPulau1;
    public GameObject pulau2;
    public Transform TravelPointPulau2;

    [Header("Pulau Nikah")]
    public GameObject pulauNikah;
    public Transform TravelPointPulauNikah;
    private void Start()
    {
        oldDialogue.AddRange( GetComponent<NpcController>().dialogues);
        OnDialogueDoneAfterOld = GetComponent<NpcController>().OnDialogueAfterDone;
        OnDialogueDoneOld = GetComponent<NpcController>().OnDialogueDone;
    }
    private void Update()
    {
        if (!targetAchieved)
        {
            if(GlobalInventory.instance.gold >= targetGold)
            {
                GetComponent<NpcController>().dialogues = newDialogue;
                GetComponent<NpcController>().dialogueAfter = newDialogue;

                GetComponent<NpcController>().OnDialogueDone = OnDialogueDone;
                GetComponent<NpcController>().OnDialogueAfterDone = OnDialogueDone;
                targetAchieved = true;
            }
        }
        else
        {
            if (GlobalInventory.instance.gold < targetGold)
            {
                GetComponent<NpcController>().dialogues = oldDialogue;
                GetComponent<NpcController>().dialogueAfter = oldDialogue;
                GetComponent<NpcController>().OnDialogueDone = OnDialogueDoneOld;
                GetComponent<NpcController>().OnDialogueAfterDone = OnDialogueDoneAfterOld;

                targetAchieved = false;
            }
        }
   
    }


    public void TravelToPulauNikah()
    {
        GlobalInventory.instance.gold -= targetGold;
        if (SaveFileController.instance.quests.Find(x => x.questId == "dermaga0")?.isDone == false)
        {
            SaveFileController.instance.AddQuestProgress(1, SaveFileController.instance.quests.Find(x => x.questId == "dermaga0"));
        }
        OnDialogueDone.RemoveAllListeners();
        OnDialogueDone.AddListener(TravelToPulau2);
        TransitionTravelToPulauNikah();
    }


    public void TravelToPulau2()
    {
        GlobalInventory.instance.gold -= targetGold;
        if(SaveFileController.instance.quests.Find(x=>x.questId == "dermaga1")?.isDone == false)
        {
            SaveFileController.instance.AddQuestProgress(1, SaveFileController.instance.quests.Find(x => x.questId == "dermaga1"));
        }
        Travel(true);
    }

    public async void TransitionTravelToPulauNikah()
    {
        pulauNikah.gameObject.SetActive(true);
        await FadeCanvasController.instance.FadeOut();
        Transform tpPoint = TravelPointPulauNikah;
        FindObjectOfType<ThirdPersonController>().transform.SetPositionAndRotation(tpPoint.position, tpPoint.rotation);
        await Task.Delay(2000);
        await FadeCanvasController.instance.FadeIn();
        SaveFileController.instance.SendNotification("Anda telah tiba di Pulau Majapahit");
    }

    public async void Travel(bool flagPulau2)
    {
        await FadeCanvasController.instance.FadeOut();
        Transform tpPoint = flagPulau2 ? TravelPointPulau2 : TravelPointPulau1;
        pulau2.SetActive(flagPulau2);
        pulau1.SetActive(!flagPulau2);
        FindObjectOfType<ThirdPersonController>().transform.SetPositionAndRotation(tpPoint.position, tpPoint.rotation);
        await Task.Delay(2000);
        await FadeCanvasController.instance.FadeIn();
        SaveFileController.instance.SendNotification("Anda telah tiba area baru");
    }

}
