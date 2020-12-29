using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float runningSpeed = 8f;
    [Header("Running Camera")]
    public float normalSize = 5f;
    public float runningSize = 5.5f;
    public float zoomSpeed = 10f;
    [Header("Dashing")]
    public float dashStrenght = 10f;
    public float dashCooldown = 4f;

    public Rigidbody2D player;

    public Camera cam;

    public Rigidbody2D gun;

    Vector2 movement;
    Vector2 mousePosition;
    float lastDash;

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        lastDash += Time.deltaTime;

        mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);

        Vector2 lookDirecton = mousePosition - player.position;
        float angle = Mathf.Atan2(lookDirecton.y, lookDirecton.x) * Mathf.Rad2Deg;
        gun.rotation = angle;

        float t = Time.deltaTime * zoomSpeed;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = runningSpeed;
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
        //         player.MovePosition(player.position + movement * dashStrenght * Time.fixedDeltaTime);
        //     }
        // }
    }

    void FixedUpdate()
    {
        player.MovePosition(player.position + movement * moveSpeed * Time.fixedDeltaTime);
        gun.position = player.position;
    }
}
