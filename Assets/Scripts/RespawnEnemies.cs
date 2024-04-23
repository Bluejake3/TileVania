using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnEnemies : MonoBehaviour
{
    [SerializeField] Transform spawnPoint;
    [SerializeField] float startingRespawnTime;
    float respawnTime;
    [SerializeField] GameObject enemyPrefab;
    // Start is called before the first frame update
    void Start(){
        respawnTime = startingRespawnTime;
    }

    void Update(){
        respawnTime -= Time.deltaTime;
        if (respawnTime < 0){
            respawningEnemies();
            respawnTime = startingRespawnTime;
        }
    }
    void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Enemy"){
            respawnTime = startingRespawnTime;
        }
    }

    void respawningEnemies(){
        Instantiate(enemyPrefab,spawnPoint.position,transform.rotation);
    }
}
