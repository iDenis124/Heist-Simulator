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
    public float bulletForce = 20f;

    [Space]
    public Rigidbody2D player;
    public Rigidbody2D gun;
    public GameObject gunTip;
    public GameObject bulletPrefab;
    public Camera cam;

    Vector2 movement;
    Vector2 mousePosition;
    private float lastDash;
    private float lastBullet;
    private float moveSpeed;

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        lastBullet += Time.deltaTime;
        lastDash += Time.deltaTime;

        mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);

        Vector2 lookDirecton = mousePosition - player.position;
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
            Shoot();
            lastBullet = 0;
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, gunTip.transform.position, gunTip.transform.rotation);
        Rigidbody2D rbBullet = bullet.GetComponent<Rigidbody2D>();
        rbBullet.AddForce(gunTip.transform.up * bulletForce, ForceMode2D.Impulse);
    }

    void FixedUpdate()
    {
        player.MovePosition(player.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
