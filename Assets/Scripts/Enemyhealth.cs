using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour
{
    Rigidbody2D rbenemyhealthki;
    public int maxhealthenemyki = 2;
    private int currenthealthenemyki;
    private SpriteRenderer sr;
    private Color originalcolour;
    private Coroutine flashRoutine;

    void Start()
    {
        currenthealthenemyki = maxhealthenemyki;
        sr = GetComponent<SpriteRenderer>();
        originalcolour = sr.color;
        rbenemyhealthki = GetComponent<Rigidbody2D>();
    }

    public void TakeDamage(int damage)
    {
        currenthealthenemyki -= damage;

       
        if (flashRoutine != null)
        {
            StopCoroutine(flashRoutine);
        }

        flashRoutine = StartCoroutine(FlashEffect());

        if (currenthealthenemyki <= 0)
        {
            Die();
        }

        if (rbenemyhealthki != null)
{
    rbenemyhealthki.AddForce(new Vector2(5f, 3f), ForceMode2D.Impulse);
}
    }

    IEnumerator FlashEffect()
    {
       
        for (int i = 0; i < 2; i++)
        {
            sr.color = Color.white;
            yield return new WaitForSeconds(0.05f);

            sr.color = originalcolour;
            yield return new WaitForSeconds(0.05f);
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}