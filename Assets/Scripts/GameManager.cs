using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    private int playerScore;
    private int playerHighScore;

    public List<GameObject> boolits = new List<GameObject>();
    public List<GameObject> borlSpawns = new List<GameObject>();

    public UIController uic;

    private bool paused;
    private bool freezeControls;

    // Start is called before the first frame update
    void Start()
    {
        GetAllBorlSpawns();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && paused && !freezeControls)
        {
            Respawn();
        }
    }

    public void PlayerDeath()
    {
        PauseGame();

    }

    private void PauseGame()
    {
        Time.timeScale = 0;
        uic.ShowPauseMenu();
        paused = true;
        freezeControls = true;
        StartCoroutine(DeathWait(0.5F));
    }

    private void ResumeGame()
    {
        Time.timeScale = 1;
        paused = false;
    }

    private void Respawn()
    {
        ResetGame();
    }

    private void ResetGame()
    {
        paused = false;
        Time.timeScale = 1;
        uic.HidePauseMenu();
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

    IEnumerator DeathWait(float time)
    {
        yield return new WaitForSecondsRealtime(time);
        freezeControls = false;
    }

    private void GetAllBorlSpawns()
    {
        foreach (GameObject spawn in FindObjectsOfType<GameObject>())
        {
            if (spawn.CompareTag("BorlSpawn"))
            {
                borlSpawns.Add(spawn);
            }
        }
    }
}
