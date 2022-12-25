using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    float bulletSpeed = 20f;
    // Start is called before the first frame update
    void Start()
    {
        Move();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Move()
    {
        this.GetComponent<Rigidbody>().velocity = transform.forward * bulletSpeed;
    }
}