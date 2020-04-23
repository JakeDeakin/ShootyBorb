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

    public List<GameObject> availableBorls = new List<GameObject>();

    public List<GameObject> currentBorls = new List<GameObject>();
    public List<GameObject> previousWave = new List<GameObject>();
    public List<GameObject> nextWave = new List<GameObject>();

    public List<GameObject> borlSizes = new List<GameObject>();

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

    private void InitializeWave()
    {

    }
    private List<GameObject> NextWave()
    {
        int wavecount = previousWave.Count;
        GameObject secondlast = null;
        GameObject last = null;
        int slsize;
        int lsize;
        

        if (wavecount == 0)
        {
            nextWave.Add(availableBorls[0]);
            return nextWave;
        }

        if (wavecount > 2)
        {
            secondlast = previousWave[wavecount - 2];
            last = previousWave[wavecount - 1];
            slsize = secondlast.GetComponent<ObstacleController>().borlSize;
            lsize = last.GetComponent<ObstacleController>().borlSize;

            if (slsize == lsize)
            {
                for (int i = 0; i<previousWave.Count - 2; i++)
                {
                    nextWave.Add(previousWave[i]);
                    previousWave[i].SetActive(true);
                }
                nextWave.Add(availableBorls[lsize + 2]);
                return nextWave;
            }

            if (slsize != lsize)
            {
                for (int i = 0; i < previousWave.Count; i++)
                {
                    nextWave.Add(previousWave[i]);
                    previousWave[i].SetActive(true);
                    nextWave.Add(availableBorls[0]);
                }
                return nextWave;
            }
        }

        return null;
    }
}
