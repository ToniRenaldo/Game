using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public int framerate;
    void Start()
    {
        Application.targetFrameRate = framerate;    
    }

    #region Inventory

    public void OpenInventory()
    {
        FindObjectOfType<InventoryController>(true).OpenInventory();
    }
    public void OpenQuests()
    {
        FindObjectOfType<QuestCanvasController>(true).OpenQuestPanel();

    }
    #endregion

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

    #region Battle


    #endregion
}
