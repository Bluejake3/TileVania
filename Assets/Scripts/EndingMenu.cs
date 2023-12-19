using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class EndingMenu : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI messageText;
    [SerializeField] TextMeshProUGUI scoreText;
    int finalScore;
    bool isWin;
    void Awake()
    {
        finalScore = FindObjectOfType<GameSession>().GetPlayerScore();
        isWin = FindObjectOfType<GameSession>().GetIsWin();
        Debug.Log(isWin);
        Destroy(FindObjectOfType<GameSession>().gameObject);
    }
    public void RestartGame(){
        SceneManager.LoadScene(0);
    }

    void Start(){
        if(isWin) messageText.text = "Congratulation";
        else messageText.text = "You Failed";

        scoreText.text = "Your Score = " + finalScore.ToString();
    }

}
