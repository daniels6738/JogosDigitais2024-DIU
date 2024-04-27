using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy1 : MonoBehaviour
{
    [SerializeField]
    private GameObject master;
    // Start is called before the first frame update */
    /* void Start()
    {
        StartCoroutine(spawnEnemy(cooldown, enemy1));
    } */

  

    public void SpawnEnemy(GameObject enemy, int health){
        GameObject newEnemy = Instantiate(enemy, transform.position, transform.rotation);
        newEnemy.GetComponent<BasicEnemyScript>().SetHealth(health);
    }
    
}
