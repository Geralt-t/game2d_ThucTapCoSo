using Assets;
using JetBrains.Annotations;
using System.Collections;
using TMPro;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rb;
    public float maxHealth = 200;
    [SerializeField]
    private float currentHealth;

    public bool isDashing = false;
    private float dashTimeleft;
    private float lastImageXpos;
    private float lashDash = -100f;
    private bool canFlip = true;
    private bool canMove = true;
    private bool canJump = true;
    private bool PlayerIsDead;

    public int speed = 4;
    public float leftRight;
    public bool isFacingRight = true;
    public int movementDirection = 1;
    public Animator anim;
    public float jump;
    public bool isGrounded = false;
    private bool isTouchingWall;
    private bool isWallSliding;

    public float dashSpeed = 24f;
    public float dashCooldown = 3f;
    public float dashTime = 1.2f;
    public float distanceBetweenImages = 0.1f;

    public Transform wallCheck;
    public Transform groundCheck;
    public float wallCheckDistance;
    public LayerMask whatIsGround;
    public float groundCheckRadius;
    public float wallSlideSpeed;

    public float healthBuffAmount=70f;

    private bool knockback;
    private float knockbackStartTime;
    [SerializeField]
    private float knockbackDuration;
    [SerializeField]
    private Vector2 knockbackSpeed;

    [SerializeField] private int numberOfAttack;
    [SerializeField] private float attackCounterResetCoolDown;

    private int currentAttackCounter;
    public bool checkAttackRepeat = false;
    private Timer attackCounterResetTimer;
    public AttackDetails attackDetails;

    public Vector2 wallJumpPower=new Vector2(8f,16f);
    public float wallJumpDirection;
    public float wallJumpTime=0.2f;
    public float wallJumpCounter;
    public float wallJumpDuration = 1f;
    private bool isWallJumping;
    private bool canFlipwhenWallJump = false;
    private bool isNormalJumping;
   
    //Normal attack
   
    [SerializeField]
    private float  normalAttackRadius, normalAttackDamage;
    [SerializeField]
    private Transform normalAttackHitBoxPos;
    [SerializeField]
    private LayerMask whatIsDamageable;
    private bool  isAttacking, isFirstAttack;

     public FloatingHealthBar healthBar;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        attackCounterResetTimer = new Timer(attackCounterResetCoolDown);
        currentHealth = maxHealth;
    }

    // Update is called once per frame

    void Update()
    {
        NormalAttack();
        NormalJump();
        Dash();
        checkDash();
        CheckIfWallSliding();
        Moving();
        WallJump();
        CheckKnockback();
        //Dead();
        
    }
    private void FixedUpdate()
    {
        CheckSurroundings();
        
    }

    private void CheckSurroundings()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
        isTouchingWall = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, whatIsGround);
        if(isGrounded)
        {
            canFlipwhenWallJump = false;
           
        }
    }

    private void checkDash()
    {
        if (isDashing)
        {
            if (dashTimeleft > 0)
            {
                canFlip = false;
                canMove = false;
                rb.velocity = new Vector2(dashSpeed * leftRight, 0.0f);
                dashTimeleft -= Time.deltaTime;
                if (Mathf.Abs(transform.position.x - lastImageXpos) > distanceBetweenImages)
                {
                    AfterImagePool.Instance.GetFromPool();
                    lastImageXpos = transform.position.x;
                }
            }
            if (dashTimeleft <= 0)
            {
                isDashing = false;
                canFlip = true;
                canMove = true;
            }
        }
    }
    private void AttempToDash()
    {
        isDashing = true;
        dashTimeleft = dashTime;
        lashDash = Time.time;
        AfterImagePool.Instance.GetFromPool();
        lastImageXpos = transform.position.x;

    }
    //animationEvent
    public void isAttackFinish()
    {
        currentAttackCounter++;
        checkAttackRepeat = false;

    }
    public void EnableFlip()
    {
        canFlip = true;
        canJump = true;
    }
    public void DisableFLip()
    {
        canFlip = false;
        canJump = false;
    }
    private void ResetAttackCounter()
    { currentAttackCounter = 0; }
    //draw in scene
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y, wallCheck.position.z));
        Gizmos.DrawWireSphere(normalAttackHitBoxPos.position, normalAttackRadius);
    }
    private void CheckIfWallSliding()
    {
        if (isTouchingWall && !isGrounded && rb.velocity.y < 0)
        {
            isWallSliding = true;
        }
        else
        {
            isWallSliding = false;
        }
    }

   
    private void Moving()
    {
        if (canMove)
        {
            
            
            if (isGrounded )
            {
                leftRight = Input.GetAxisRaw("Horizontal");
                anim.SetFloat("moving", Mathf.Abs(leftRight));
                if (!isWallJumping)
                {
                    rb.velocity = new Vector2(speed * leftRight, rb.velocity.y);
                }
            }
            
            if (isWallSliding)
            {
                anim.SetFloat("moving", 0);
                if (rb.velocity.y < -wallSlideSpeed)
                {
                    rb.velocity = new Vector2(rb.velocity.x, -wallSlideSpeed);
                }
            }

        }

        if (isFacingRight == true && leftRight == -1 && canFlip)
        {
            Flip();
        }
        if (isFacingRight == false && leftRight == 1 && canFlip)
        {
            Flip();
        }
    }

    private void Flip()
    {
        if (!isWallSliding)
        {
            transform.Rotate(0.0f, 180.0f, 0.0f);
            isFacingRight = !isFacingRight;
            movementDirection = -movementDirection;
        }
    }
    private void NormalAttack()
    {
        attackCounterResetTimer.Tick();
        attackCounterResetTimer.OnTimerDone += ResetAttackCounter;
        if (Input.GetKeyDown(KeyCode.Z) && checkAttackRepeat == false&&isWallSliding==false)
        {


            attackCounterResetTimer.StartTimer();
            anim.SetInteger("counter", currentAttackCounter);
            anim.SetTrigger("normalAttack");
            checkAttackRepeat = true;
        }
        
        if (currentAttackCounter == numberOfAttack)
        {
            currentAttackCounter = 0;

        }
        if (currentAttackCounter == 0) isFirstAttack = true;
        else isFirstAttack = false;
        anim.SetBool("firstAttack", isFirstAttack);
    }
    private void NormalJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && canJump && !isWallSliding && isNormalJumping==false)
        {
            rb.AddForce(new Vector2(rb.velocity.x, jump));
            anim.SetTrigger("Jump");
            isNormalJumping = true;
        }
        if(isNormalJumping==true&& isGrounded == true)
        {
            isNormalJumping = false;
        }
        
    }
    private void WallJump()
    {
        
        if(isWallSliding)
        {
            isWallJumping = false;
            wallJumpDirection = -movementDirection;
            wallJumpCounter = wallJumpTime;
            CancelInvoke(nameof(StopWallJumping));
            canFlipwhenWallJump = false;
        }
        else
        {
            wallJumpCounter -= Time.deltaTime;
            if (movementDirection != wallJumpDirection&& wallJumpCounter<-0.2f &&  canFlipwhenWallJump==true)
            {
                transform.Rotate(0.0f, 180.0f, 0.0f);
                isFacingRight = !isFacingRight;
                movementDirection = -movementDirection;
            }
        }
        if(Input.GetKeyDown(KeyCode.Space)&& wallJumpCounter>0f)
        {
            isWallJumping = true;
            rb.velocity = new Vector2(wallJumpPower.x * wallJumpDirection, wallJumpPower.y);
            wallJumpCounter = 0f;
            
                canFlipwhenWallJump = true;
            

            Invoke(nameof(StopWallJumping), wallJumpDuration);
        }
    }
    private void StopWallJumping()
    {
        isWallJumping = false;
    }
    private void Dash()
    {
        if (Input.GetKeyDown(KeyCode.C) &&isWallSliding==false && isGrounded)
        {
            if (Time.time >= (lashDash + dashCooldown))
            {
                AttempToDash();
            }
        }
    }
    private void CheckAttackHitBox()
    {
        Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(normalAttackHitBoxPos.position, normalAttackRadius, whatIsDamageable);
        attackDetails.damageAmount=normalAttackDamage;
        attackDetails.position = transform.position;
        
        foreach (Collider2D collider in detectedObjects)
        {
            collider.transform.parent.SendMessage("Damage", attackDetails);
        }
    }
  
   
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag ("teleport"))
        {
            rb.position = new Vector2(192, -69);
            Debug.Log("TOUCHING");
            currentHealth = maxHealth;
            healthBar.UpdateHealthBar(currentHealth, maxHealth);
        }
        else if (other.gameObject.CompareTag("health potion"))
        {
            if (currentHealth < maxHealth - healthBuffAmount)
            {
                currentHealth = currentHealth + healthBuffAmount;
            }
            else
            {
                currentHealth = maxHealth;
            }
                Destroy(other.gameObject);
            healthBar.UpdateHealthBar(currentHealth, maxHealth);
            Debug.Log("TOUCHING");
            
        }
        else if(other.gameObject.CompareTag("strength potion"))
        {
            normalAttackDamage += 5;
            Destroy(other.gameObject);
            Debug.Log("TOUCHING");
        }
    }
    public void Damage(AttackDetails attackDetails)
    {

        currentHealth -= attackDetails.damageAmount;
        healthBar.UpdateHealthBar(currentHealth, maxHealth);
        int direction;
        if (attackDetails.position.x < transform.position.x)
        {
            direction = 1;
        }
        else
        {
            direction = -1;
        }
        
        Knockback(direction);
    }
    private void Dead()
    {
        if(currentHealth < 0)
        {
            anim.SetBool("Dead",true);
            PlayerIsDead = true;
        }
    }
     
    public void Knockback(int direction)
    {
        knockback = true;
        knockbackStartTime = Time.time;
        rb.velocity = new Vector2(knockbackSpeed.x * direction, knockbackSpeed.y);
    }

    private void CheckKnockback()
    {
        if (Time.time >= knockbackStartTime + knockbackDuration && knockback)
        {
            knockback = false;
            rb.velocity = new Vector2(0.0f, rb.velocity.y);
        }
    }
    public float GetDashCoolDown()
    {
        return dashCooldown;
    }
}


