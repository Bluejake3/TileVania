using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalIn : MonoBehaviour
{
    [SerializeField] Transform portalExit;
    Rigidbody2D rigidbody2D;
    Vector2 portalExitDestination;
    void Awake() {
        portalExitDestination = new Vector2(portalExit.position.x, portalExit.position.y);
    }
    void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player"){
            rigidbody2D = other.GetComponent<Rigidbody2D>();
            rigidbody2D.MovePosition(portalExitDestination);
        }
    }

}
