using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasTutorialController : MonoBehaviour
{
    // Start is called before the first frame update
    public bool debug;

    private void Start()
    {
        if (debug)
            return;
        if (PlayerPrefs.HasKey("TutorialDone"))
        {
            if(PlayerPrefs.GetInt("TutorialDone") == 1)
            {
                CloseTutorial();
            }
        }
    }

    public void CloseTutorial()
    {
        if (!debug)
        {
            PlayerPrefs.SetInt("TutorialDone", 1);
        }
        gameObject.SetActive(false);
    }
}
