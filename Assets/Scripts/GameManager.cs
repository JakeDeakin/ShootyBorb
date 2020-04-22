using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    private int playerScore;
    private int playerHighScore;

    public List<GameObject> boolits = new List<GameObject>();
    public UIController uic;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayerDeath()
    {
        PauseGame();

    }

    private void PauseGame()
    {
        Time.timeScale = 0;
        uic.ShowPauseMenu();
    }

    private void ResumeGame()
    {

    }
    
    public void UpdateScore()
    {
        playerScore++;
        UpdateHighScore();
        uic.UpdateHUD(playerHighScore, playerScore);
    }

    private void UpdateHighScore()
    {
        if (playerScore > playerHighScore)
        {
            playerHighScore = playerScore;
        }
    }
}
