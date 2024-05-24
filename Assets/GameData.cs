using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static AvatarController;
using static GameData;

public class GameData : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameData instance;

    public enum ItemEffect
    {
        IncreaseHealth,
        IncreaseDamage
    }
    public enum WeaponType
    {
        KERIS,
        TOMBAK,
        PANAH,
        PEDANG
    }
    [System.Serializable]
    public class CommonItem
    {
        public string id;
        public string objectName;
        public int price;
    }

    [System.Serializable]
    public class Weapon : CommonItem
    {

        public int damage;
        public float criticalStrike;
        public Sprite imageSprite;

        public WeaponType type;
        public GameObject weaponPrefab;

    }
    [System.Serializable]
    public class Armor : CommonItem
    {
        public int armorHealth;
        public int defendChance;
        public Sprite imageSprite;

    }

    [System.Serializable]
    public class Item : CommonItem
    {

        public ItemEffect effect;
        public int value;
        public int duration;
        public Sprite imageSprite;
    }

    public List<Weapon> globalWeapon;
    public List<Armor> globalArmor;
    public List<Item> globalItem;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public Weapon GetWeapon(Weapon weapon)
    {
        if (globalWeapon.Find(x => x.id == weapon.id) != null)
        {
            return globalWeapon.Find(x => x.id == weapon.id);
        }
        else
        {
            return null;
        }
    }
    public Item GetItem(Item item)
    {
        if (globalItem.Find(x => x.id == item.id) != null)
        {
            return globalItem.Find(x => x.id == item.id);
        }
        else
        {
            return null;
        }

    }
    public Armor GetArmor(Armor weapon)
    {
        if (globalArmor.Find(x => x.id == weapon.id) != null)
        {
            return globalArmor.Find(x => x.id == weapon.id);
        }
        else
        {
            return null;
        }
    }
}
