using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float enemyspeed = 2f;
    private Rigidbody2D re;
    private int direction = 1;
    public int enemycolour = 0;

    private SpriteRenderer sr;

    void Start()
    {
        re = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        enemycolour = Random.Range(0, 5);
       
    }

    void Update()
    {
        re.linearVelocity = new Vector2(direction * enemyspeed, re.linearVelocity.y);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            direction *= -1;
        }
    }
}