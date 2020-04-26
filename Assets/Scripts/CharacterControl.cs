using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl  : MonoBehaviour
{
    private GameManager gm;
    public GameObject projectile;
    public GameObject boolitSpawn;
   // public List<GameObject> boolits = new List<GameObject>();
    public int fireRate;
    private bool canShoot = true;
    public float movementSpeed;

    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<GameManager>();
        InstantiateBoolits(fireRate * 10);
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
            this.transform.Translate(new Vector3(Input.GetAxis("Horizontal"), 0) * Time.deltaTime * movementSpeed); 
        }



        if (Input.GetButton("Fire1"))
        {
            if (Input.mousePosition.x < Screen.width / 2)
            {
                this.transform.Translate(new Vector3(-1, 0) * Time.deltaTime * movementSpeed);
            }

            if (Input.mousePosition.x > Screen.width / 2)
            {
                this.transform.Translate(new Vector3(1, 0) * Time.deltaTime * movementSpeed);
            }
        }
    }


    private void PlayerShoot()
    {
        
        if (canShoot && fireRate > 0)
        {
            canShoot = false;
            if (gm.boolits.Count == 0)
            {
                InstantiateBoolits(1);
            }

            if (gm.boolits.Count > 0)
            {
                gm.boolits[0].transform.position = boolitSpawn.transform.position;
                gm.boolits[0].SetActive(true);
                StartCoroutine(ShootWait(1 / fireRate));
                gm.boolits.RemoveAt(0);
            }
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

    private void InstantiateBoolits(int num)
    {
        
        for (int i = 0; i < num; i++)
        {
            GameObject boolit = Instantiate(projectile, boolitSpawn.transform.position, boolitSpawn.transform.rotation);
            gm.boolits.Add(boolit);
            boolit.SetActive(false);
        }  
    }
}
