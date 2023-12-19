using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody2D rigidbody2D;
    [SerializeField] float bulletSpeed =1f;
    [SerializeField] float damage = 1f;
    [SerializeField] int shootEnemyScore = 100;
    PlayerMovement player;
    float xSpeed;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerMovement>();
        xSpeed = player.transform.localScale.x * bulletSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        rigidbody2D.velocity = new Vector2(xSpeed, 0f); 
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Enemy"){
            if (other.gameObject.GetComponent<EnemyMovements>().GetKilled()) return;
            other.gameObject.GetComponent<EnemyMovements>().TakeDamage(damage);
            FindObjectOfType<GameSession>().AddScore(shootEnemyScore);
        }
        Destroy(gameObject);
    }
}
