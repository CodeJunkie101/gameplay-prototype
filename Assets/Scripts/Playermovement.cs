using UnityEngine;

public class Playermovement : MonoBehaviour
{
    public Animator anim;
    public float speedofmovement = 5f;
    public float bounceforce = 5f;
    public float dashforce = 20f;
    public float dashtime = 0.2f;
    public float dashcooldown = 1f;
    public float forceofjump = 10f;
    public int maxjumps = 2;
   
    public Transform firepoint;
   
    public int Gulaal = 0;
    public TrailRenderer trail;

    private Rigidbody2D r;
    private SpriteRenderer sr;
    private bool isgrounded;
    private int jumpcount = 0;
    private bool isdashing = false;
    private float dashtimer;
    private float cooldownofdashtimer;
    private bool hasdashedinair = false;
    private float move;
    private int forward = 1;
    private bool isDead = false;

    void Start()
    {
        r = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        if (trail != null) trail.emitting = false;
    }

    void Update()
    {
        if (isDead) return;

        move = Input.GetAxis("Horizontal");

        if (move > 0)
        {
            sr.flipX = false;
            forward = 1;
        }
        else if (move < 0)
        {
            sr.flipX = true;
            forward = -1;
        }

        HandleAnimations();

        if (cooldownofdashtimer > 0)
            cooldownofdashtimer -= Time.deltaTime;

        if (isdashing)
        {
            dashtimer -= Time.deltaTime;
            if (dashtimer <= 0)
            {
                isdashing = false;
                r.gravityScale = 3;
                if (trail != null) trail.emitting = false;
            }
            return;
        }

        r.linearVelocity = new Vector2(move * speedofmovement, r.linearVelocity.y);

        if (Input.GetKeyDown(KeyCode.Space) && jumpcount < maxjumps)
        {
            r.linearVelocity = new Vector2(r.linearVelocity.x, forceofjump);
            jumpcount++;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && cooldownofdashtimer <= 0)
        {
            if (!isgrounded && hasdashedinair) return;

            isdashing = true;
            dashtimer = dashtime;
            cooldownofdashtimer = dashcooldown;
            r.gravityScale = 0;

            if (!isgrounded) hasdashedinair = true;

            r.linearVelocity = new Vector2(forward * dashforce, 0);

            if (trail != null) trail.emitting = true;

            anim.SetTrigger("dash");
        }

      

           

            

            
        

        
    }

    void HandleAnimations()
    {
        anim.SetBool("jump", r.linearVelocity.y > .1f);
        anim.SetBool("tground", isgrounded);
        anim.SetFloat("yVel", r.linearVelocity.y);
        anim.SetBool("idle", Mathf.Abs(move) < .1f && isgrounded);
        anim.SetBool("run", Mathf.Abs(move) > .1f && isgrounded);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isgrounded = true;
            hasdashedinair = false;
            jumpcount = 0;
        }

        if (collision.gameObject.CompareTag("Enemy") && !isDead)
        {
            Die();
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isgrounded = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bouncepad"))
        {
            r.linearVelocity = new Vector2(r.linearVelocity.x, bounceforce);
            jumpcount = 1;
            hasdashedinair = false;
        }
    }

    void Die()
{
    isDead = true;

    r.linearVelocity = Vector2.zero;
    r.angularVelocity = 0f;

    r.gravityScale = 0;

    r.constraints = RigidbodyConstraints2D.FreezeAll;

    anim.Play("death");

    Invoke(nameof(StopGame), 1.2f);
}

    void StopGame()
    {
        Time.timeScale = 0f;
    }
}