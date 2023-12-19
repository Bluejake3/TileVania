using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Vector2 moveInput;
    Rigidbody2D rigidbody2D;
    Animator animation;
    bool playerHasHorizontalSpeed;
    bool playerHasVerticalSpeed;
    bool isPlayerGoToRight;
    [SerializeField] float speedMultiplier = 1f; 
    [SerializeField] float jumpSpeed = 1f;
    [SerializeField] float climbSpeed = 1f;
    [SerializeField] float animationSpeed = 1f;
    [SerializeField] float deadKick = 10f;
    [SerializeField] GameObject bullet;
    [SerializeField] Transform weapon;
    [SerializeField] float bodyStopping = 1f;
    CapsuleCollider2D capsuleCollider2D;
    BoxCollider2D boxCollider2D;
    float currentGravity;
    bool playerDead= false;
    bool levelRestarting= false;
    bool doubleJumpDone = false;

    // Start is called before the first frame update
    void Awake() {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animation = GetComponent<Animator>();
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    void Start() {
        currentGravity = rigidbody2D.gravityScale;    
    }

    // Update is called once per frame
    void Update()
    {
        if(levelRestarting) return;
        Die();
        if(playerDead) return; 
        Run();
        FlipSprite();
        ClimbLadder();
    }

    void OnFire(InputValue value){
        if(playerDead){return;}
        Instantiate(bullet, weapon.position, transform.rotation);
    }

    void OnMove(InputValue value){
        if(playerDead){return;}
        moveInput = value.Get<Vector2>();
        if(moveInput.x > 0)moveInput.x = 1;
        else if (moveInput.x < 0)moveInput.x = -1;
        else moveInput.x = 0;
    }

    void OnJump(InputValue value){
        if(playerDead){return;}
        if(doubleJumpDone){
            if(!boxCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground")))return;
            doubleJumpDone = false;
        }
        if(!boxCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground"))) doubleJumpDone = true;
        if(value.isPressed){
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, jumpSpeed);
        }
    }

    void ClimbLadder(){
        if(!capsuleCollider2D.IsTouchingLayers(LayerMask.GetMask("Ladder"))){
            rigidbody2D.gravityScale = currentGravity;
            animation.SetBool("isClimbing", false);
            animation.speed = animationSpeed;
            return;
        }
        Vector2 climbVelocity = new Vector2 (rigidbody2D.velocity.x, moveInput.y * climbSpeed);
        rigidbody2D.gravityScale = 0;
        rigidbody2D.velocity = climbVelocity;

        animation.SetBool("isClimbing", true);

        playerHasVerticalSpeed = Mathf.Abs(rigidbody2D.velocity.y) > Mathf.Epsilon;
        if(playerHasVerticalSpeed){
            animation.speed = animationSpeed;
        }else{
            animation.speed = 0;
        }
    }

    void Run(){
        Vector2 playerVelocity = new Vector2 (moveInput.x * speedMultiplier, rigidbody2D.velocity.y);
        rigidbody2D.velocity = playerVelocity;
        isPlayerGoToRight = (rigidbody2D.velocity.x >= 0);
        if(!playerHasHorizontalSpeed){
            animation.SetBool("isRunning", false);
            animation.SetBool("isFacingLeft", false);
            return;
        }
        animation.SetBool("isRunning", isPlayerGoToRight);
        animation.SetBool("isFacingLeft", !isPlayerGoToRight);
    }
    void FlipSprite(){
        playerHasHorizontalSpeed = Mathf.Abs(rigidbody2D.velocity.x) > Mathf.Epsilon;

        if(playerHasHorizontalSpeed){
            transform.localScale = new Vector2 (Mathf.Sign(rigidbody2D.velocity.x),1f);
        }
    }

    void Die() {
        if (rigidbody2D.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazard")) && !playerDead){
            playerDead = true;
            Vector2 deadSpeed = new Vector2(rigidbody2D.velocity.x, deadKick);
            animation.SetTrigger("Dying");
            rigidbody2D.velocity = deadSpeed;
    
        }
        if (playerDead && !levelRestarting){
            Invoke("StopBody", bodyStopping);
        }
    }

    public bool GetPlayerDead(){
        return playerDead;
    }
    void StopBody(){
        if(boxCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground"))&& !levelRestarting){
            levelRestarting = true;
            rigidbody2D.velocity = Vector2.zero;
            rigidbody2D.bodyType = RigidbodyType2D.Static;
            FindObjectOfType<GameSession>().ProcessPlayerDeath();
        }
    }
    
}
