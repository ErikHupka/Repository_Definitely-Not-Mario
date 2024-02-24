using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLaser : MonoBehaviour
{
    [SerializeField] float laserSpeed = 1000;
    [SerializeField] float destroyLaserDelay = 2f;

    //Components
    Rigidbody2D rigidBody;
    GameObject player;

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerMovement>().gameObject;
    }

    void Start()
    {
        CheckPlayerRotation();
    }

    void Update()
    {
        LaserMovement();
    }

    void LaserMovement()
    {
        float movementX = Time.deltaTime * laserSpeed;
        Vector2 movement = new(movementX, 0f);

        rigidBody.velocity = movement;

        Destroy(gameObject, destroyLaserDelay);
    }

    void CheckPlayerRotation()
    {
        if (player.transform.localScale.x < 0) //Facing Left
        {
            laserSpeed = -laserSpeed;
        }
    }
}
