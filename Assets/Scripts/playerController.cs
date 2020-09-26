using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{

    public Vector2 movementDirection;
    public float movementSpeed;
    public float movementBaseSpeed = 10.0f;
    public Rigidbody2D rb;

    void Update(){
        processInput();
        Move();
    }

    void processInput(){
        movementDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        movementSpeed = Mathf.Clamp(movementDirection.magnitude, 0.0f, 1.0f);
        movementDirection.Normalize();
    }

    void Move(){
        rb.velocity = movementDirection * movementSpeed * movementBaseSpeed;
    }
}
