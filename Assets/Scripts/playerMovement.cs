using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{

    public float moveSpeed = 5f;
    public float normalSize = 5f;
    public float runningSize = 5.5f;
    public float zoomSpeed = 10f;
    public float dashStrenght = 10f;
    public float dashCooldown = 4f;

    public Rigidbody2D rb;

    public Camera cam;

    Vector2 movement;
    Vector2 mousePosition;
    float lastDash;

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        lastDash += Time.deltaTime;

        mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);

        float t = Time.deltaTime * zoomSpeed;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = 7f;
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, runningSize, t);
        }
        else
        {
            moveSpeed = 5f;
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, normalSize, t);
        }

        // if (Input.GetKey(KeyCode.Space) && lastDash > dashCooldown)
        // {
        //     if (movement != Vector2.zero)
        //     {
        //         lastDash = 0;
        //         rb.MovePosition(rb.position + movement * dashStrenght * Time.fixedDeltaTime);
        //     }
        // }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

        Vector2 lookDirecton = mousePosition - rb.position;
        float angle = Mathf.Atan2(lookDirecton.y, lookDirecton.x) * Mathf.Rad2Deg - 90f;
    }
}
