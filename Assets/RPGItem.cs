using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RPGItem : MonoBehaviour
{
    // Start is called before the first frame update
    public enum Type
    {
        Item,
        Weapon,
        Armor
    }
    public AvatarController.Stats stats;
    public GameData.Item item;
    public GameData.Weapon weapon;
    public GameData.Armor armor;
    public GameData.CommonItem commonItem;
    public int ammount;
    public bool isWeapon;
    public Type itemType;
    [Header("UI")]
    public Image icon;
    public Image avatar;
    public TMP_Text itemName;
    public TMP_Text itemEffect;
    public TMP_Text itemAmmount;
    public Button buyButton;
    public void Initiate(AvatarController.Stats stats, GameData.Item item)
    {
        this.stats = stats;
        this.item = item;

        var groupByID = stats.items.GroupBy(x => x.id);

        ammount = stats.items.FindAll(x => x.id == item.id).Count;

        itemName.text = item.objectName;

        string effect = "";
        if(item.effect == GameData.ItemEffect.IncreaseHealth)
        {
            effect += "+ " + item.value + "HP";
        }else if(item.effect == GameData.ItemEffect.IncreaseDamage)
        {
            effect += "+ " + item.value + "DMG";
        }
        itemEffect.text = effect;
        itemAmmount.text = "x" + ammount;
        icon.sprite = item.imageSprite;
        GetComponent<Button>().onClick.AddListener(ChooseItem);
    }
    public void UpdateAmmount()
    {
        ammount = stats.items.FindAll(x => x.id == item.id).Count;
        itemAmmount.text = "x" + ammount;
        if (ammount == 0)
            Destroy(gameObject); 
    }

    public void ChooseItem()
    {
        FindObjectOfType<RPG_InventoryController>().ChooseObject(this.item);
    }
     public void UseItem()
    {
        stats.items.Remove(item);
        UpdateAmmount();
    }

    public void InventorySetup(GameData.Weapon obj)
    {
        this.weapon = GameData.instance.GetWeapon(obj);
        
        itemName.text = weapon.objectName;  
        itemEffect.text = weapon.damage + "DMG";
        if(weapon.imageSprite != null)
        {
            icon.sprite = weapon.imageSprite;
        }
        itemAmmount.text = weapon.price.ToString();
        itemType = Type.Weapon;
        commonItem = weapon;
    }
    public void InventorySetup(GameData.Item obj, bool store = false)
    {
        item = GameData.instance.GetItem(obj);
        itemType = Type.Item;


        var groupByID = stats.items.GroupBy(x => x.id);

        ammount = stats.items.FindAll(x => x.id == item.id).Count;

        itemName.text = item.objectName;

        string effect = "";
        if (item.effect == GameData.ItemEffect.IncreaseHealth)
        {
            effect += "+ " + item.value + "HP";
        }
        else if (item.effect == GameData.ItemEffect.IncreaseDamage)
        {
            effect += "+ " + item.value + "DMG";
        }
        itemEffect.text = effect;
        itemAmmount.text = store? item.price.ToString() : "x" + ammount;
        icon.sprite = item.imageSprite;
        commonItem = item;

    }

    public void InventorySetup(GameData.Armor obj, bool store = false)
    {
        this.armor = GameData.instance.GetArmor(obj);
        itemName.text = armor.objectName;
        itemEffect.text = armor.defendChance + "DEF";
        if (armor.imageSprite != null)
        {
            icon.sprite = armor.imageSprite;
        }
        itemAmmount.text = armor.price.ToString();
        itemType = Type.Armor;
        commonItem = armor;


    }


}
