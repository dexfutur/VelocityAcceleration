using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using System.Security.Cryptography.X509Certificates;

public class Rebote0 : MonoBehaviour
{
    private MyVector position;
    [SerializeField] private MyVector acceleration;
    [SerializeField] private MyVector velocity;
    [Range(0f, 1f)][SerializeField] private float dampingFactor = 0.9f;

    [SerializeField] new Camera camera;
    private MyVector a;
    private float b = 1;

    void Start()
    {
        position = new MyVector(transform.position.x, transform.position.y);
    }
    private void FixedUpdate()
    {
        Move();
    }

    void Update()
    {
        position.Draw(Color.blue);
        velocity.Draw(position, Color.red);
        acceleration.Draw(position, Color.green);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            b *= -1;
            a.y = acceleration.y;
            a.x = acceleration.x;
            acceleration.y = a.x * b;
            acceleration.x = a.y * b;
            velocity *= 0;
        }
    }

    public void Move()
    {
        velocity = velocity + acceleration * Time.fixedDeltaTime;
        position = position + velocity * Time.fixedDeltaTime;

        if (position.x > camera.orthographicSize)
        {
            velocity.x *= -1;
            position.x = camera.orthographicSize;
            velocity *= dampingFactor;
        }
        else if (position.x < -camera.orthographicSize)
        {
            velocity.x *= -1;
            position.x = -camera.orthographicSize;
            velocity *= dampingFactor;
        }

        else if (position.y < -camera.orthographicSize)
        {
            velocity.y *= -1;
            position.y = -camera.orthographicSize;
            velocity *= dampingFactor;
        }
        else if (position.y > camera.orthographicSize)
        {
            velocity.y *= -1;
            position.y = camera.orthographicSize;
            velocity *= dampingFactor;
        }
        transform.position = new Vector3(position.x, position.y);

    }
}
