using UnityEngine;

public class WaveMove : MonoBehaviour
{
    public float speed = 2f;

    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
           
             Time.timeScale = 0f; 
        }
    }
}

