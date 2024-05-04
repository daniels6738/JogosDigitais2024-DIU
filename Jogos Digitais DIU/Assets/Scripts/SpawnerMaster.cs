using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SpawnerMaster : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject spawner1;
    [SerializeField]
    private GameObject spawner2;
    [SerializeField]
    private GameObject spawner3;
    [SerializeField]
    private float cooldown;
    [SerializeField]
    private GameObject enemy;
    private System.Random ran;
    private GameObject[] spawners;

    [SerializeField]
    private float enemyHealth;
    private int spawnCount = 0;

    void Start(){
        ran = new System.Random();
        spawners = new GameObject[3];
        spawners[0] = spawner1;
        spawners[1] = spawner2;
        spawners[2] = spawner3;
        StartCoroutine(SpawnEnemy(cooldown, enemy));
        enemyHealth = 2;
    }
    
    public IEnumerator SpawnEnemy(float interval, GameObject enemy){
        yield return new WaitForSeconds(interval);
        enemyHealth = 2 + (spawnCount/4);
        spawnCount++;
        spawners[ran.Next(0,spawners.Length)].GetComponent<SpawnEnemy1>().SpawnEnemy(enemy, enemyHealth);
        StartCoroutine(SpawnEnemy(interval, enemy));
    }


}
