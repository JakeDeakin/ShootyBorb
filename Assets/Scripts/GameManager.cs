using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    private int playerScore;
    private int playerHighScore;
    
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

    }
    
    private void UpdateScore()
    {
        playerScore++;
    }

    private void UpdateHighScore()
    {
        if (playerScore > playerHighScore)
        {
            playerHighScore = playerScore;
        }
    }
}
