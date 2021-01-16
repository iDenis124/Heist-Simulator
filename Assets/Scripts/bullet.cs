using UnityEngine;

public class bullet : MonoBehaviour
{
    private float timeAlive;

    void Update()
    {
        timeAlive += Time.deltaTime;
        if (timeAlive >= 2f)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Enemy")
        {
            collision.gameObject.GetComponent<enemy>().TakeDamage(20);
        }
        Destroy(gameObject);
    }
}