using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Animations;

public class ShopController : MonoBehaviour
{
    public List<GameData.Item> items;
    public List<GameData.Weapon> weapons;
    public List<GameData.Armor> armors;

    public System.DateTime lastVisit;
    public float restockMinutes;
    [Header("UI")]
    public GameObject canvasShop;
    [SerializeField] private GameObject weaponPrefab;
    [SerializeField] private Transform weaponTabContent;
    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private Transform itemTabContent;
    [SerializeField] private GameObject armorPrefab;
    [SerializeField] private Transform armorContainer;
    [SerializeField] private TMP_Text gold;
    [SerializeField] GameObject modalConfirm;
    public List<RPGItem> storeItems;

    [Header("Icon")]
    public Transform icon;
    public LookAtConstraint lookAtConstraint;

    private void Start()
    {
        ConstraintSource cameraSource = new ConstraintSource();
        cameraSource.sourceTransform = Camera.main.transform;
        cameraSource.weight = 1;

        lookAtConstraint.AddSource(cameraSource);
    }
    public void OpenInventory()
    {
        if(lastVisit == null)
        {
            Restock();
        }
       
        System.TimeSpan timespan = System.DateTime.Now - lastVisit;
        
        if(timespan.TotalMinutes > restockMinutes)
        {
            Restock();
        }


        canvasShop.SetActive(true);
        foreach (Transform t in armorContainer)
        {
            Destroy(t.gameObject);
        }
        foreach (Transform t in itemTabContent)
        {
            Destroy(t.gameObject);
        }
        foreach (Transform t in weaponTabContent)
        {
            Destroy(t.gameObject);
        }

        foreach (var panel in weapons)
        {
            GameObject item = Instantiate(weaponPrefab, weaponTabContent);
            RPGItem rpgItem = item.GetComponent<RPGItem>();
            rpgItem.InventorySetup(panel);
            rpgItem.buyButton.onClick.AddListener(() => RequestBuy(panel.id, rpgItem));
            storeItems.Add(rpgItem);
        }

        foreach (var panel in items)
        {
            GameObject item = Instantiate(itemPrefab, itemTabContent);
            RPGItem rpgItem = item.GetComponent<RPGItem>();
            rpgItem.InventorySetup(panel,true);
            rpgItem.buyButton.onClick.AddListener(() => RequestBuy(panel.id, rpgItem));
            storeItems.Add(rpgItem);

        }

        foreach (var panel in armors)
        {
            GameObject item = Instantiate(armorPrefab, armorContainer);
            RPGItem rpgItem = item.GetComponent<RPGItem>();
            rpgItem.InventorySetup(panel);
            rpgItem.buyButton.onClick.AddListener(() => RequestBuy(panel.id, rpgItem));
            storeItems.Add(rpgItem);
        }
        UpdatePrice();
        gameObject.SetActive(true);
    }

    private void Restock()
    {
        lastVisit = System.DateTime.Now;

        items.Clear();
        int itemAmmount = Random.Range(5, 15);
        for(int i = 0; i < itemAmmount;i++)
        {
            GameData.Item item = new GameData.Item() { id = GameData.instance.globalItem[Random.Range(0, GameData.instance.globalItem.Count)].id };
            items.Add(item);
        }

        items.Sort((x, y) => string.Compare(x.id, y.id));
    }
    void UpdatePrice()
    {
        foreach(var item in storeItems)
        {
            if(item.commonItem.price > GlobalInventory.instance.gold)
            {
                item.buyButton.interactable = false;
            }
        }
    }

    public RPGItem requestinBuyItem;
    public void RequestBuy(string id , RPGItem item)
    {
        modalConfirm.gameObject.SetActive(true);
        requestinBuyItem = item;
    }

    public void YesBuy()
    {
        modalConfirm.SetActive(false);
        GlobalInventory.instance.gold -= requestinBuyItem.commonItem.price;
        GlobalInventory.instance.AddItem(requestinBuyItem);

        if (requestinBuyItem.itemType == RPGItem.Type.Weapon)
        {
            weapons.Remove(requestinBuyItem.weapon);
        }
        else if (requestinBuyItem.itemType == RPGItem.Type.Armor)
        {
            armors.Remove(requestinBuyItem.armor);
        }
        else if (requestinBuyItem.itemType == RPGItem.Type.Item)
        {
            items.Remove(requestinBuyItem.item);
        }

        Destroy(requestinBuyItem.gameObject);
        UpdatePrice();
    }
    public void NoBuy()
    {
        modalConfirm.SetActive(false);
    }
}
