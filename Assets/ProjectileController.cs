using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    private Transform t;
    public float speed = 1;
    // Start is called before the first frame update
    void Start()
    {
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
}
