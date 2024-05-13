using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static AvatarController;
using static GameData;
using static UnityEditor.Progress;

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
    public class Weapon
    {
        public string weaponId;
        public string weaponName;
        public int damage;
        public float criticalStrike;
        public Sprite imageSprite;

        public WeaponType type;
        public GameObject weaponPrefab;
        public int price;

    }
    [System.Serializable]
    public class Armor
    {
        public string armorId;
        public string name;
        public int armorHealth;
        public int defendChance;
        public int price;
        public Sprite imageSprite;


    }

    [System.Serializable]
    public class Item
    {
        public string itemId;
        public string name;
        public ItemEffect effect;
        public int value;
        public int duration;
        public Sprite imageSprite;
        public int price;
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
        if (globalWeapon.Find(x => x.weaponId == weapon.weaponId) != null)
        {
            return globalWeapon.Find(x => x.weaponId == weapon.weaponId);
        }
        else
        {
            return null;
        }
    }
    public Item GetItem(Item item)
    {
        if (globalItem.Find(x => x.itemId == item.itemId) != null)
        {
            return globalItem.Find(x => x.itemId == item.itemId);
        }
        else
        {
            return null;
        }

    }
    public Armor GetArmor(Armor weapon)
    {
        if (globalArmor.Find(x => x.armorId == weapon.armorId) != null)
        {
            return globalArmor.Find(x => x.armorId == weapon.armorId);
        }
        else
        {
            return null;
        }
    }
}
