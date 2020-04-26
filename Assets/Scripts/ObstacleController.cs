using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    private GameManager gm;
    public int borlSize;
    public List<GameObject> splitSpawns = new List<GameObject>();
    private bool quitting = false;

    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gm.PlayerDeath();
        }

        if (collision.gameObject.CompareTag("Projectile"))
        {
            obstacleHit();
            gm.UpdateScore();
        }
    }

    private void obstacleHit()
    {
        this.gameObject.SetActive(false);
        gm.currentBorls.Remove(this.gameObject);
        gm.reserveBorls.Add(this.gameObject);
        Split();
        gm.CheckCurrentBorls();
    }

    private void Split()
    {
        if (borlSize > 1)
        {
            int a = -1;
            for (int i = 0; i < 2; i++)
            {
                foreach (GameObject bo in gm.reserveBorls)
                {
                    if (bo.GetComponent<ObstacleController>().borlSize == borlSize - 1)
                    {
                        gm.currentBorls.Add(bo);
                        gm.reserveBorls.Remove(bo);
                        bo.SetActive(true);
                        bo.transform.position = splitSpawns[i].transform.position;

                        bo.GetComponent<Rigidbody2D>().AddForce(new Vector2 (a * 20f, a * 20f));
                        a = 1;
                        break;
                    }
                }
            }
            
        }
    }

    private void OnBecameInvisible()
    {
        if (!quitting && this.gameObject.activeInHierarchy)
        {
            transform.position = gm.borlSpawns[0].transform.position;
        }
        
    }

    private void OnApplicationQuit()
    {
        quitting = true;
    }
}
