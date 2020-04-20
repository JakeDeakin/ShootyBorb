using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl  : MonoBehaviour
{
    private GameManager gm;
    public GameObject projectile;
    public GameObject boolitSpawn;

    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerShoot();
        PlayerMovement();
    }

    private void PlayerMovement()
    {
        if (Input.GetButton("Horizontal"))
        {

        }
    }


    private void PlayerShoot()
    {
        if (Input.GetButton("Fire1"))
        {
            Instantiate(projectile, boolitSpawn.transform.position, boolitSpawn.transform.rotation);
            
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // check that the collision is with an obstacle, not just a wall
        
        gm.PlayerDeath();
    }
}
