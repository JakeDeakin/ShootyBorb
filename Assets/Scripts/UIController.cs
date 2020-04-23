using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    private GameObject hud;
    public GameObject pauseMenu;
    private GameObject mainMenu;

    public Text hudPlayerScore;
    public Text hudPlayerHighScore;

    public int playerScore;
    public int playerHighScore;

    public Text pmPlayerScore;
    public Text pmPlayerHighScore;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateHUD(int highscore, int score)
    {
        playerScore = score;
        playerHighScore = highscore;
        
        hudPlayerScore.text = playerScore.ToString();
        hudPlayerHighScore.text = playerHighScore.ToString();
    }

    public void ShowPauseMenu()
    {
        pauseMenu.gameObject.SetActive(true);
        UpdatePauseMenuScore();
    }

    public void HidePauseMenu()
    {
        pauseMenu.gameObject.SetActive(false);
    }

    public void UpdatePauseMenuScore()
    {
        pmPlayerHighScore.text = "Score: " + playerHighScore.ToString();
        pmPlayerScore.text = "Highscore: " + playerScore.ToString();
    }

}
