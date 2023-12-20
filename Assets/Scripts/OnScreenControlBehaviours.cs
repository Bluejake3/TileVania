using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnScreenControlBehaviours : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(Application.isMobilePlatform);   
    }

    public void PressUp(){
        FindObjectOfType<PlayerMovement>().AlternateMovement(0,1);
    }

    public void PressDown(){
        FindObjectOfType<PlayerMovement>().AlternateMovement(0,-1);
    }

    public void PressLeft(){
        FindObjectOfType<PlayerMovement>().AlternateMovement(-1,0);
    }

    public void PressRight(){
        FindObjectOfType<PlayerMovement>().AlternateMovement(1,0);
    }

    public void ReleaseMovementButtons(){
        FindObjectOfType<PlayerMovement>().AlternateMovement(0,0);
    }

    public void PressFire(){
        FindObjectOfType<PlayerMovement>().AlternateFire();
    }

    public void PressJump(){
        FindObjectOfType<PlayerMovement>().AlternateJump(true);
    }
}
