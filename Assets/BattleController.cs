using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static Unity.Burst.Intrinsics.X86;

public class BattleController : MonoBehaviour
{
    public GameObject BattlePrefab;
    public List<GameObject> bot;
    public GameObject mainCamera;

    [Header("Result")]
    public List<ResultCanvasController.Reward> rewardList;
    public List<ResultCanvasController.Reward> punishmentList;
    public GameObject activeBattle;
    [Header("Icon")]
    [SerializeField] private Sprite goldIcon;
    [ContextMenu("Test Start Battle")]
    public void TestBattle()
    {
        StartBattle();

    }
    public void StartBattle(bool random = true)
    {
        GameObject battle = Instantiate(BattlePrefab);
        activeBattle = battle;
        battle.transform.position = new Vector3(0, 400, 0);


        

        battle.GetComponent<TurnBasedRPG>().setupLeftTeam.Clear();
        battle.GetComponent<TurnBasedRPG>().setupRightTeam.Clear();

        if (random)
        {
            int enemyCount = Random.Range(1, 5);

            for(int i = 1; i<= enemyCount; i++)
            {
                GameObject obj = new GameObject("Bot " + i);
                bot.Add(obj);
                AvatarController ac = obj.AddComponent<AvatarController>();
                ac.choosenAvatar = (AvatarController.AVATAR)Random.Range(0, 4);
                ac.stats.avatarName = "Musuh " + i;
                ac.stats.weapon1 = GameData.instance.globalWeapon[Random.Range(0, GameData.instance.globalWeapon.Count)];

                ac.stats.defaultHP = Random.Range(20, 40);
                ac.stats.currentHP = ac.stats.defaultHP;
                ac.stats.defaultAP = Random.Range(20, 40);
                ac.stats.currentAP = ac.stats.defaultAP;


                battle.GetComponent<TurnBasedRPG>().setupRightTeam.Add(new AvatarStats() { avatar = ac });

                BotController bc = obj.AddComponent<BotController>();
                bc.type = (BotController.BotType) Random.Range(0, 5);


            }


            int goldReward = Random.Range(30, 70);
            int goldPunishment = Random.Range(0,-20);

            rewardList.Clear();
            punishmentList.Clear(); 

            rewardList.Add(new ResultCanvasController.Reward("Gold", goldReward));
            punishmentList.Add(new ResultCanvasController.Reward("Gold", goldPunishment));

            battle.GetComponent<TurnBasedRPG>().rewardList.Clear();
            battle.GetComponent<TurnBasedRPG>().rewardList.AddRange(rewardList);

            battle.GetComponent<TurnBasedRPG>().punishmentList.Clear();
            battle.GetComponent<TurnBasedRPG>().punishmentList.AddRange(punishmentList);

        }


        //Setup Reward

        foreach (var ava in FindObjectOfType<AvatarDatas>().avatars)
        {
            battle.GetComponent<TurnBasedRPG>().setupLeftTeam.Add(new AvatarStats() { avatar = ava });
        }

        //Fade Here

        // Turning Off Camera
        mainCamera.gameObject.SetActive(false);

        battle.GetComponent<TurnBasedRPG>().StartBattle();
    }

    public void CloseBattle(bool win)
    {
        foreach (var item in rewardList)
        {
            if (item.type == "Gold")
            {
                GlobalInventory.instance.gold += item.value;
            }
        }
        mainCamera.gameObject.SetActive(true);

        foreach(var t in bot)
        {
            Destroy(t.gameObject);
        }
        Destroy(activeBattle);
    }
}
