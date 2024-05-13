using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlobalInventory : MonoBehaviour
{
    public List<GameData.Item> items;
    public List<GameData.Weapon> weapons;
    public List<GameData.Armor> armors;
    public int gold;
    public static GlobalInventory instance;


    private void Awake()
    {
        instance = this;
    }

}
