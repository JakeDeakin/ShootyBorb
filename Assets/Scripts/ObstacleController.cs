using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    private GameManager gm;
    public int borlSize;
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
        gm.CheckCurrentBorls();
    }

    private void Split()
    {
        if (borlSize > 1)
        {
            
        }
    }
}
