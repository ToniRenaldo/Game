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

    private void Start()
    {
       
    }
    private void Awake()
    {
        instance = this;
    }

    public void AddGold(int ammount)
    {
        gold += ammount;
    }
    public void AddItem(RPGItem item)
    {
        if(item.itemType == RPGItem.Type.Weapon)
        {
            weapons.Add(new GameData.Weapon() { id = item.commonItem.id });
        }
        else if (item.itemType == RPGItem.Type.Armor)
        {
            armors.Add(new GameData.Armor() { id = item.commonItem.id });
        }
        else if (item.itemType == RPGItem.Type.Item)
        {
            items.Add(new GameData.Item() { id = item.commonItem.id });
        }

    }

}
