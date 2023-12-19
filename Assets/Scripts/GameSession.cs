using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;    

public class GameSession : MonoBehaviour
{
    [SerializeField] int playerLives =1;
    [SerializeField] float deathDelay = 1f;
    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] int scorePerLives = 5000;
    int playerScore = 0;
    bool isWin = true;
    void Awake() {
        int numGameSession = FindObjectsOfType<GameSession>().Length;
        if (numGameSession>1){
            Destroy(gameObject);
        }
        else{
            DontDestroyOnLoad(gameObject);
        }
   }

    void Start() {
        livesText.text = playerLives.ToString();
        scoreText.text = playerScore.ToString();
   }

   public void ProcessPlayerDeath(){
    if(playerLives > 1){
        Invoke("TakeLive", deathDelay);
    }else{
        Invoke("LoadEndingMenu", deathDelay);
    }
   }

    void ResetGameSession(){
        FindObjectOfType<ScenePersist>().ResetScenePersist();
        Destroy(gameObject);
   }

   void TakeLive(){
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        playerLives-=1;
        SceneManager.LoadScene(currentSceneIndex);
        livesText.text = playerLives.ToString();
   }

   public void AddScore(int scoreValue){
        playerScore += scoreValue;
        scoreText.text = playerScore.ToString();
        if(playerScore % scorePerLives == 0) AddLive();
   }

   void AddLive(){
        playerLives +=1;
        livesText.text = playerLives.ToString();
   }

   public int GetPlayerScore(){
        return playerScore;
   }

    void LoadEndingMenu(){
        isWin = false;
        FindObjectOfType<ScenePersist>().ResetScenePersist();
        SceneManager.LoadScene(SceneManager.sceneCountInBuildSettings -1);
    }
    public bool GetIsWin(){
        return isWin;
    }
}
