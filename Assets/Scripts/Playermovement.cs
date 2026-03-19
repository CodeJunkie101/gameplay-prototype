using UnityEngine;
using UnityEngine.Rendering;

public class Playermovement : MonoBehaviour
{
    #region variabledeclaration
    public float speedofmovement = 5f;
    private float facingdirection = 1f;
    public int Gulaal=0;
    public float bounceforce = 5f;
    public float dashforce = 20f;
    public float dashtime = 0.2f;
    public float dashcooldown = 1f;
    private bool isdashing = false;
    private Animator animator;
    private float dashtimer;
    private float cooldownofdashtimer;
    public float forceofjump = 10f;
    private Rigidbody2D r;
    private bool isgrounded;
    private int jumpcount = 0;
    public int maxjumps = 2;
    private SpriteRenderer sr;
    private bool hasdashedinair = false;
    public GameObject bulletprefab;
    public Transform firepoint;
    public int colourofbullet = 0;
    #endregion variabledeclaration

    int forward;
    void Start()
    {
        r = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        
    }

    void Update()
    {

    float move = Input.GetAxis("Horizontal");
   
    
    if (move > 0)
    {
       
        forward = 1; 
    }
    else if (move < 0)
    {
        
        forward = -1;
    }

    #region DASHLOGIC_and_movement
    if (cooldownofdashtimer > 0) cooldownofdashtimer -= Time.deltaTime;
    if (isdashing)
    {
        dashtimer -= Time.deltaTime;

        if (dashtimer <= 0)
        {
            isdashing = false;
            r.gravityScale = 3; 
        }
        return; 
    }
    
    r.linearVelocity = new Vector2(move * speedofmovement, r.linearVelocity.y);
    #region JumpandDoublejump
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
        #endregion JumpandDoublejump
        if (!isgrounded) hasdashedinair = true;

        r.linearVelocity = new Vector2(forward * dashforce, 0);
        #endregion DASHLOGIC_and_movement
    }
    #region Bullet_creation_and direction logic of bullet and colour of bullet
    if(Input.GetKeyDown(KeyCode.Z))
    {
        GameObject Bullet = Instantiate(bulletprefab , firepoint.position , Quaternion.identity);
        Bullet bulletScript = Bullet.GetComponent<Bullet>();
        bulletScript.bulletcolor = colourofbullet;  
        Bullet.GetComponent<Rigidbody2D>().linearVelocity =  new Vector2(forward * 10f, 0);

        SpriteRenderer sr = Bullet.GetComponent<SpriteRenderer>();

        if (colourofbullet == 0) sr.color = Color.red;
        if (colourofbullet == 1) sr.color = Color.green;
        if (colourofbullet == 2) sr.color = Color.blue;
        if (colourofbullet == 3) sr.color = Color.yellow;

        Destroy(Bullet, 2f);
            
    }
    
    if (Input.GetKeyDown(KeyCode.Alpha1))
{
    colourofbullet = 0;
}

if (Input.GetKeyDown(KeyCode.Alpha2))
{
    colourofbullet = 1;
}

if (Input.GetKeyDown(KeyCode.Alpha3))
{
    colourofbullet = 2;
}

if (Input.GetKeyDown(KeyCode.Alpha4))
{
    colourofbullet = 3;
}

if(Gulaal == 10)
        {
            Time.timeScale = 0f; 
        }

        
#endregion Bullet_creation_and direction logic of bullet and colour of bullet

}

    #region Allcollisions
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            
            isgrounded = true;
            hasdashedinair = false;
            jumpcount = 0;
        }
        if (collision.gameObject.CompareTag("Enemy"))
    {
        Time.timeScale = 0f; 
        Debug.Log("Game Over");
    }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            isgrounded = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Bouncepad"))
        {
            r.linearVelocity = new Vector2(r.linearVelocity.x,0);
            r.linearVelocity = new Vector2(r.linearVelocity.x  , bounceforce);
            jumpcount = 1;
            hasdashedinair = false;
        }
    }
    
   
    #endregion Allcollisions

}