using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl  : MonoBehaviour
{
    private GameManager gm;
    public GameObject projectile;
    public GameObject boolitSpawn;
    private List<GameObject> boolits = new List<GameObject>() ;
    public float fireRate;
    private bool canShoot = true;

    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<GameManager>();
        //InstantiateBoolits();
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
        if (canShoot)
        {
            canShoot = false;
            Instantiate(projectile, boolitSpawn.transform.position, boolitSpawn.transform.rotation);
            StartCoroutine(ShootWait(1/fireRate));
        }
    }

    private IEnumerator ShootWait(float time)
    {
        yield return new WaitForSeconds(time);
        canShoot = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // check that the collision is with an obstacle, not just a wall
        
        gm.PlayerDeath();
    }

    private void InstantiateBoolits()
    {
        
        for (int i = 0; i < fireRate * 10; i++)
        {
            GameObject boolit = Instantiate(projectile, boolitSpawn.transform.position, boolitSpawn.transform.rotation);
            boolits.Add(boolit);
            boolit.SetActive(false);
            
        }  
    }
}
