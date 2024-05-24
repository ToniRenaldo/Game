using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OpenWorldEnemyController : MonoBehaviour
{
    public bool random;

    public List<ResultCanvasController.Reward> reward;
    public List<ResultCanvasController.Reward> punishment;

    public List<AvatarController> enemyAvatars;
    public UnityEvent OnWin;
    public UnityEvent OnLose;

    public int indexRandomStart;
    public int indexRandomEnd;

    public async void InitiateBattle()
    {
        if (random)
        {
            await FadeCanvasController.instance.FadeOut();
            BattleController.instance.StartBattle(callback: EndBattle, startingRandomIndex:indexRandomStart,endRandomIndex:indexRandomEnd);
        }
        else
        {
            BattleController.instance.StartBattle(false,reward,punishment,enemyAvatars: enemyAvatars , EndBattle);
        }
    }

    public void EndBattle(bool win)
    {
        if (win)
        {
            OnWin.Invoke();
        }
        else
        {
            OnLose.Invoke();
        }
    }
}
