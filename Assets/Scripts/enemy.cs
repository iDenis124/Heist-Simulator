using UnityEngine;

public class enemy : MonoBehaviour
{
    public bool invincible = false;
    public int viewDistance = 2;
    public GameObject player;
    public LayerMask enemyItself;

    private int health = 100;

    void Update()
    {
        RaycastHit2D lineOfSight = Physics2D.Raycast(gameObject.transform.position, player.transform.position, ~enemyItself);
        if (lineOfSight)
        {
            Debug.Log(lineOfSight.transform);
            Debug.Log(lineOfSight.collider);
        }
    }

    public void TakeDamage(int damage)
    {
        if (!invincible) health -= damage;
        if (health <= 0) Destroy(gameObject);
    }
}
