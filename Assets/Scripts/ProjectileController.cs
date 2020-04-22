using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    private Transform t;
    public float speed = 1;
    private GameManager gm;
    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<GameManager>();
        t = this.transform;
    }

    // Update is called once per frame
    void Update()
    {
        MoveProjectile();
    }

    void MoveProjectile()
    {
        //Move the obstacle to the left
        t.Translate(new Vector3(0f, Time.deltaTime, 0f) * speed);
    }

    private void OnBecameInvisible()
    {
        recycleBoolit();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        recycleBoolit();
    }

    private void recycleBoolit()
    {
        this.gameObject.SetActive(false);
        gm.boolits.Add(this.gameObject);
    }
}
