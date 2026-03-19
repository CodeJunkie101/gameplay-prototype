using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int bulletcolor;
    void OnTriggerEnter2D(Collider2D other)
{
    if (other.CompareTag("Ground") || other.CompareTag("Enemy"))
    {
        Destroy(gameObject);
    }
}
}


