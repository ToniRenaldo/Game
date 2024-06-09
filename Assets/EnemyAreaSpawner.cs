using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAreaSpawner : MonoBehaviour
{
    public List<OpenWorldEnemyController> enemies;
    public float intervalSpawnEnemy;
    private void Start()
    {
        InvokeRepeating("SpawnEnemy", intervalSpawnEnemy, intervalSpawnEnemy);
        InvokeRepeating("SpawnEnemy", intervalSpawnEnemy, intervalSpawnEnemy);

    }


    public void SpawnEnemy()
    {
        List<OpenWorldEnemyController> inActiveEnemy = enemies.FindAll(x => x.gameObject.activeSelf == false);

        if(inActiveEnemy.Count != 0)
        {
            int index = Random.Range(0, inActiveEnemy.Count);
            inActiveEnemy[index].level = Random.Range(1, 4);
            inActiveEnemy[index].enemyCount = Random.Range(1, 5);

            inActiveEnemy[index].gameObject.SetActive(true);
        }


    }

}
