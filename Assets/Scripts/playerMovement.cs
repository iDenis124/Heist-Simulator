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
    Vector2 gunInaccuracy;
    private float lastBullet;
    private float moveSpeed;

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        lastBullet += Time.deltaTime;

        mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);

        Vector2 lookDirecton = mousePosition - player.position;
        float angle = Mathf.Atan2(lookDirecton.y, lookDirecton.x) * Mathf.Rad2Deg;

        gun.rotation = angle;
        gun.position = player.position;

        Running();
        Shoot();
    }

    void FixedUpdate()
    {
        player.MovePosition(player.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    void Shoot()
    {
        if (Input.GetMouseButton(0) && lastBullet > fireRate)
        {
            lastBullet = 0;
            GameObject bullet = Instantiate(bulletPrefab, gunTip.transform.position, gunTip.transform.rotation);
            Rigidbody2D rbBullet = bullet.GetComponent<Rigidbody2D>();
            Vector3 shootAngle = gunTip.transform.up;
            shootAngle.x += Random.Range(gunInaccuracy.x, gunInaccuracy.y);
            shootAngle.y += Random.Range(gunInaccuracy.x, gunInaccuracy.y);
            rbBullet.AddForce(shootAngle * bulletForce, ForceMode2D.Impulse);
        }
    }

    void Running()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            gunInaccuracy.x = -0.1f;
            gunInaccuracy.y = 0.1f;
            moveSpeed = runningSpeed;
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, runningSize, Time.deltaTime * zoomSpeed);
        }
        else
        {
            gunInaccuracy.x = -0.07f;
            gunInaccuracy.y = 0.07f;
            moveSpeed = defaultMoveSpeed;
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, normalSize, Time.deltaTime * zoomSpeed);
        }
    }
}
