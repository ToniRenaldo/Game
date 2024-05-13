using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    #endregion

    #region Battle


    #endregion
}
