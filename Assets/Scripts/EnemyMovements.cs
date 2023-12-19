using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovements : MonoBehaviour
{
    Rigidbody2D rigidbody2D;
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] float health = 1f;
    [SerializeField] float enemyScale = 1f;
    bool killed = false;
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rigidbody2D.velocity = new Vector2(moveSpeed, 0f);  
    }
    void OnTriggerExit2D(Collider2D other){
        if(other.tag == "Bullets") return;
        ChangeMoveSpeed();
        FlipEnemyFacing();

    }

    void OnCollisionEnter2D(Collision2D other) {
        ChangeMoveSpeed();
        FlipEnemyFacing();
    }

    public void FlipEnemyFacing(){
        transform.localScale = new Vector2 (-(Mathf.Sign(rigidbody2D.velocity.x)) * enemyScale, enemyScale);
    }

    public bool GetKilled(){
        return killed;
    }

    public void SetKilled(bool state){
        killed = state;
    }

    public void TakeDamage(float damage){
        health -= damage;

        if(health <= 0){
            SetKilled(true);
            Destroy(gameObject);
        }
        
    }
    public void ChangeMoveSpeed(){
        moveSpeed = -moveSpeed;
    }
    
}
