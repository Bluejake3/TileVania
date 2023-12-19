using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehaviours : MonoBehaviour
{
    [SerializeField] GameObject exitPoint;
    [SerializeField] GameObject player;
    Rigidbody2D bossRigidbody;

    void Awake(){
        bossRigidbody = GetComponent<Rigidbody2D>();
    }
    void Update(){
        if((bossRigidbody.velocity.x < 0 && player.transform.position.x > gameObject.transform.position.x) || 
        (bossRigidbody.velocity.x > 0 && player.transform.position.x < gameObject.transform.position.x)){
            gameObject.GetComponent<EnemyMovements>().FlipEnemyFacing();
            gameObject.GetComponent<EnemyMovements>().ChangeMoveSpeed();
        }
        
    }

    void OnDestroy() {
        exitPoint.gameObject.SetActive(true);
    }
}
