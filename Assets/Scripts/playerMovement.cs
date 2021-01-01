using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float defaultMoveSpeed = 6f;
    public float runningSpeed = 8f;

    [Header("Running Camera")]
    public float normalSize = 5f;
    public float runningSize = 5.5f;
    public float zoomSpeed = 10f;

    [Header("Dashing")]
    public float dashStrenght = 10f;
    public float dashCooldown = 4f;

    [Header("Gun")]
    public float fireRate = 0.3f;

    [Space]
    public Rigidbody2D player;
    public Rigidbody2D gun;
    public GameObject gunTip;
    public Camera cam;
    public LineRenderer lineRend;

    Vector2 movement;
    Vector2 mousePosition;
    private float lastDash;
    private float lastBullet;
    private float moveSpeed;

    void Start()
    {
        lineRend = GetComponent<LineRenderer>();
        lineRend.positionCount = 2;
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        lastBullet += Time.deltaTime;
        lastDash += Time.deltaTime;
    
        mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);

        Vector2 lookDirecton = mousePosition - player.position;
        // Vector2 bulletTraceEnd = Vector2.Lerp(gunTip.transform.position, mousePosition, Time.deltaTime);
        float angle = Mathf.Atan2(lookDirecton.y, lookDirecton.x) * Mathf.Rad2Deg;

        gun.rotation = angle;
        gun.position = player.position;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = runningSpeed;
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, runningSize, Time.deltaTime * zoomSpeed);
        }
        else
        {
            moveSpeed = defaultMoveSpeed;
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, normalSize, Time.deltaTime * zoomSpeed);
        }

        if (Input.GetMouseButton(0) && lastBullet > fireRate)
        {
            lastBullet = 0;
            lineRend.SetPosition(0, gunTip.transform.position);
            lineRend.SetPosition(1, mousePosition);
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
    }
}
