using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RPGItem : MonoBehaviour
{
    // Start is called before the first frame update
    public AvatarController.Stats stats;
    public GameData.Item item;
    public GameData.Weapon weapon;
    public GameData.Armor armor;

    public int ammount;
    public bool isWeapon;

    [Header("UI")]
    public Image icon;
    public Image avatar;
    public TMP_Text itemName;
    public TMP_Text itemEffect;
    public TMP_Text itemAmmount;
    

    public void Initiate(AvatarController.Stats stats, GameData.Item item)
    {
        this.stats = stats;
        this.item = item;


        var groupByID = stats.items.GroupBy(x => x.itemId);

        ammount = stats.items.FindAll(x => x.itemId == item.itemId).Count;

        itemName.text = item.name;

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
        ammount = stats.items.FindAll(x => x.itemId == item.itemId).Count;
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

        itemName.text = weapon.weaponName;
        itemEffect.text = weapon.damage + "DMG";
        if(weapon.imageSprite != null)
        {
            icon.sprite = weapon.imageSprite;
        }
        itemAmmount.text = weapon.price.ToString();
    }
    public void InventorySetup(GameData.Item obj)
    {
        item = GameData.instance.GetItem(obj);


        var groupByID = stats.items.GroupBy(x => x.itemId);

        ammount = stats.items.FindAll(x => x.itemId == item.itemId).Count;

        itemName.text = item.name;

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
        itemAmmount.text = "x" + ammount;
        icon.sprite = item.imageSprite;
    }

    public void InventorySetup(GameData.Armor obj)
    {
        this.armor = GameData.instance.GetArmor(obj);

        itemName.text = armor.name;
        itemEffect.text = armor.defendChance + "DEF";
        if (weapon.imageSprite != null)
        {
            icon.sprite = armor.imageSprite;
        }
        itemAmmount.text = armor.price.ToString();
    }


}
