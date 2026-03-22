using UnityEngine;
using System.Collections;

public class enemy : MonoBehaviour
{
    public float speed = 2f;
    public int health = 50;

    private Rigidbody2D body;
    private SpriteRenderer sprite;

    private int direction = 1;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // move left and right
        body.linearVelocity = new Vector2(direction * speed, body.linearVelocity.y);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Wall"))
        {
            direction = direction * -1;
        }
    }

    // called when player punches
    public void takedamage(int damage)
    {
        health = health - damage;

        // flash effect
        StartCoroutine(flash());

        // simple push back
        if (colleft())
        {
            body.linearVelocity = new Vector2(5f, body.linearVelocity.y);
        }
        else
        {
            body.linearVelocity = new Vector2(-5f, body.linearVelocity.y);
        }

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    // small flash effect
    IEnumerator flash()
    {
        sprite.color = Color.white;
        yield return new WaitForSeconds(0.05f);

        sprite.color = Color.red;
        yield return new WaitForSeconds(0.05f);

        sprite.color = Color.white;
    }

    // simple check (player side)
    bool colleft()
    {
        GameObject p = GameObject.FindWithTag("Player");

        if (p == null) return false;

        if (p.transform.position.x < transform.position.x)
            return true;
        else
            return false;
    }
}