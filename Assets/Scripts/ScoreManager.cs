using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text EnemiesCount;
    public Text KillsCount;
    public static int Kills = 0;
    public static int Enemies = 0;

    // Start is called before the first frame update
    void Start()
    {
        KillsCount.text = (Kills).ToString();
        EnemiesCount.text = EnemySpawner.ES.enemyCount.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        KillsCount.text = (Kills).ToString();
        EnemiesCount.text = EnemySpawner.ES.enemyCount.ToString();
    }
}
