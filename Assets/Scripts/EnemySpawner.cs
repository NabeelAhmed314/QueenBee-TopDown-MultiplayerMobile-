using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefab;
    public Transform[] spawnLoc;
    public float respawnTime = 1;
    public int Random_spawn;
    public int Random_enemy;
    public static EnemySpawner ES;
    public int enemyCount;
    private void OnEnable()
    {
        if (EnemySpawner.ES == null)
        {
            EnemySpawner.ES = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(enemyWave());
    }
    private void spawnEnemy()
    {
        Random_spawn = (int)Random.Range(0, 8);
        Random_enemy = (int)Random.Range(0, 5);
        PhotonNetwork.InstantiateSceneObject(enemyPrefab[Random_enemy].name , spawnLoc[Random_spawn].position , spawnLoc[Random_spawn].rotation);
        EnemySpawner.ES.enemyCount++;
    }
    IEnumerator enemyWave()
    {
        while (true)
        {
            yield return new WaitForSeconds(respawnTime);
            spawnEnemy();
        }
    }
}
