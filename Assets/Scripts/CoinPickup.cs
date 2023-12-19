using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{

    [SerializeField] AudioClip audioClip;
    [SerializeField] int coinPickupScore = 50;
    bool wasCollected = false;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D other) {
        if(FindObjectOfType<PlayerMovement>().GetPlayerDead()) return;
        if(other.tag == "Player" && !wasCollected){
            wasCollected = true;
            AudioSource.PlayClipAtPoint(audioClip, Camera.main.transform.position);
            FindObjectOfType<GameSession>().AddScore(coinPickupScore);
            Destroy(gameObject);
        }
    }
}
