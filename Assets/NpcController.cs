using Cinemachine;
using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class NpcController : MonoBehaviour
{
    // Start is called before the first frame update
    public string npcName;
    public bool isAggresive;
    public List<string> dialogues;
    public List<string> dialogueAfter;

    public UnityEvent OnDialogueDone;
    public UnityEvent OnDialogueAfterDone;


    [Header("Canvas")]
    public GameObject dialogueCanvas;
    public TMP_Text dialogueTMP;
    public TMP_Text dialogueName;
    public Button canvasButton;

    public Animator avaAnimator;
    public Transform cameraPosition;
    public bool interacted;
    public int dialogueCounter = 0;
    public bool typing;
    private List<string> activeDialogue;
    public float dialogueSpeed;
    private void Start()
    {
        canvasButton.onClick.AddListener(NextDialgoue);
    }
    Coroutine CR_Dialgoue;


    public void NextDialgoue()
    {
        

        if(CR_Dialgoue != null)
        {
            StopCoroutine(CR_Dialgoue);
            dialogueTMP.text = activeDialogue[dialogueCounter];
            CR_Dialgoue = null;
        }
        else
        {
            if (dialogueCounter + 1 == activeDialogue.Count)
            {
                FindObjectOfType<CinemachineVirtualCamera>().Follow = FindObjectOfType<ThirdPersonController>().GetComponent<CameraPosition>().cameraPos;
                FindObjectOfType<UICanvasControllerInput>(true).gameObject.SetActive(true);
                dialogueCanvas.SetActive(false);
                if (interacted)
                {
                    OnDialogueAfterDone.Invoke();
                }
                else
                {
                    LocalPlayer.instance.mainAvatar.SetActive(true);
                    OnDialogueDone.Invoke();

                }
                interacted = true;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                return;
            }
            dialogueCounter++;
            

            dialogueTMP.text = "";
            CR_Dialgoue = StartCoroutine(IE_Type());
        }

    }

    public IEnumerator IE_Type()
    {
        int textLength = activeDialogue[dialogueCounter].Length;
        int counter = 0;
        while(counter != textLength)
        {
            dialogueTMP.text += activeDialogue[dialogueCounter][counter];
            yield return new WaitForSeconds(dialogueSpeed);
            counter++;
        }
        CR_Dialgoue = null;
        yield return null;
    }
    public void ShowDialgoue()
    {
        FindObjectOfType<CinemachineVirtualCamera>().Follow = cameraPosition;

        FindObjectOfType<UICanvasControllerInput>(true).gameObject.SetActive(false);
        LocalPlayer.instance.mainAvatar.SetActive(false);

        activeDialogue = interacted ?  dialogueAfter: dialogues;  
        dialogueCounter = 0;
        dialogueName.text = npcName;
        dialogueTMP.text = "";

        CR_Dialgoue = StartCoroutine(IE_Type());

        //dialogueTMP.text = activeDialogue[0];
        dialogueCanvas.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

    }

    public void InitateInteractButton()
    {
        if (isAggresive)
        {
            ShowDialgoue();
        }
        else
        {
            FindObjectOfType<InteractButton>().ActivateButton(ShowDialgoue);
        }
    }
    public void DeactivateInteractButton()
    {
        FindObjectOfType<InteractButton>().DeactivateButton();

    }

    public void DestroyInstance()
    {
        Destroy(gameObject);
    }
}
