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
        Setenemycolour();
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

    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            Bullet bulletScript = other.GetComponent<Bullet>();

            if (bulletScript != null)
            {
                if (bulletScript.bulletcolor == enemycolour)
                {
                    Destroy(other.gameObject); 
                    Destroy(gameObject);     
                }
                else
                {
                    Destroy(other.gameObject);   
                }
            }
        }
    }
    void Setenemycolour()
    {
        if (enemycolour == 0) sr.color = Color.red;
        if (enemycolour == 1) sr.color = Color.green;
        if (enemycolour == 2) sr.color = Color.blue;
        if (enemycolour == 3) sr.color = Color.yellow;
    }
}