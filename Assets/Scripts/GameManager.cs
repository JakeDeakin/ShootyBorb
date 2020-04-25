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

    public List<GameObject> currentBorls = new List<GameObject>();
    public List<GameObject> previousWave = new List<GameObject>();
    public List<GameObject> nextWave = new List<GameObject>();

    public List<GameObject> borlSizes = new List<GameObject>();
    public List<GameObject> reserveBorls = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        GetAllBorlSpawns();
        CreateAllBorls();
        NextWave();
        GrabBorl();
        InitializeWave();
        
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
        Shuffle<GameObject>(borlSpawns);
    }

    private void InitializeWave()
    {
        Shuffle<GameObject>(borlSpawns);
        foreach (GameObject g in currentBorls)
        {
            g.SetActive(true);   
            g.transform.position = borlSpawns[currentBorls.IndexOf(g)].transform.position;
        }
    }
    private List<GameObject> NextWave()
    {
        

        previousWave = new List<GameObject>(nextWave);
        nextWave.Clear();
        int wavecount = previousWave.Count;
        GameObject secondlast = null;
        GameObject last = null;
        int slsize;
        int lsize;
        
        if (wavecount == 0)
        {
            print("Previous wave didn't exist.");
            nextWave.Add(borlSizes[0]);
            return nextWave;
        }

        if (wavecount == 1)
        {
            print("previous wave only had 1 enemy");
            nextWave.Add(previousWave[0]);
            nextWave.Add(borlSizes[0]);
            return nextWave;
        }

        if (wavecount >= 2)
        {
            print("Previous wave had 2 or more enemies");
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
                nextWave.Add(borlSizes[lsize]);
                return nextWave;
            }

            if (slsize != lsize)
            {
                for (int i = 0; i < previousWave.Count; i++)
                {
                    nextWave.Add(previousWave[i]);
                    previousWave[i].SetActive(true);
                }
                nextWave.Add(borlSizes[0]);
                return nextWave;
            }
        }

        return null;
    }

    private void CreateBorl(int size)
    {
        Instantiate(borlSizes[size - 1], borlSpawns[0].transform);
    }

    private void CreateAllBorls()
    {
        foreach (GameObject b in borlSizes)
        {
            float maxnum = Mathf.Pow(2, borlSizes.Count - (1 + borlSizes.IndexOf(b)));
            int a = (int)maxnum;

            for (int i = 0; i < a; i++)
            {
                GameObject temp = Instantiate(b);
                temp.SetActive(false); 
                reserveBorls.Add(temp);
            }
        }
    }

    private void GrabBorl()
    {
        foreach (GameObject b in nextWave)
        {
            foreach (GameObject bo in reserveBorls)
            {
                if (bo.GetComponent<ObstacleController>().borlSize == b.GetComponent<ObstacleController>().borlSize)
                {
                    currentBorls.Add(bo);
                    reserveBorls.Remove(bo);
                    break;
                }
            }
        }
    }

    public void CheckCurrentBorls()
    {
        if (currentBorls.Count == 0)
        {
            BeginNextWave();
        }
    }

    private void BeginNextWave()
    {
        previousWave = nextWave;
        NextWave();
        GrabBorl();
        InitializeWave();
    }


    public void Shuffle<T>(List<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);

            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

}
