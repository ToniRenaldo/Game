using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class RPG_InventoryController : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform placeholder;
    public GameObject itemPrefab;
    public GameData.Item currentItem;
    public List<RPGItem> items;
    public void Initate(AvatarController.Stats stats)
    {
        var groupByID = stats.items.GroupBy(x=> x.itemId);

        foreach(var group in groupByID)
        {
            GameObject item = Instantiate(itemPrefab , placeholder);
            Debug.Log(group.Key);
            GameData.Item currentItem = GameData.instance.globalItem.Find(x => x.itemId == group.Key);
            item.GetComponent<RPGItem>().Initiate(stats,currentItem);
            items.Add(item.GetComponent<RPGItem>());
        }

    }

    public void ChooseObject(GameData.Item choosenItem)
    {
        currentItem = choosenItem;
    }
    public void UseObject()
    {
        if (currentItem == null)
            return;
        FindObjectOfType<TurnBasedRPG>().SetItem(currentItem);
        FindObjectOfType<TurnBasedRPG>().SetAction(TurnBasedRPG.ActionType.USEITEM);
    }
    public void UpdateItem(GameData.Item item)
    {
        items.Find(x => x.item.itemId == item.itemId).UpdateAmmount();   
    }
}
