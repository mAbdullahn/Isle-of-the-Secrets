using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    int comboCount = 0;
    private Vector2 moveInput;
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer spriteRenderer;
    [Header("Combat Settings")]
    [SerializeField] private float comboLeeway = 0.8f; 
    private float lastAttackTime;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        if (comboCount > 0 && Time.time - lastAttackTime > comboLeeway)
        {
            ResetCombo();
        }
    }
    void FixedUpdate()
    {

        rb.linearVelocity = new Vector2(moveInput.x * moveSpeed, rb.linearVelocity.y);

        UpdateAnimation();
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void UpdateAnimation()
    {
        if (moveInput.x != 0)
        {
            anim.SetBool("isRunning", true);
            if (moveInput.x > 0)
            {
                spriteRenderer.flipX = false; 
            }
            else
            {
                spriteRenderer.flipX = true; 
            }
        }
        else
        {
            // Stop moving = Stop running
            anim.SetBool("isRunning", false);
        }
    }
    void OnJump(InputValue value)
    {
        if(value.isPressed)
        {
            Debug.Log("Jump Pressed");
            rb.linearVelocity= new Vector2(rb.linearVelocity.x, 10f);
            anim.SetBool("isGrounded", false);
        }
    }
    void OnAttack(InputValue value) 
    {
        if (value.isPressed)
        {
            lastAttackTime = Time.time; // Update timer on every click
            comboCount++;

            if (comboCount > 3) comboCount = 1; // Loop back to start if mashing

            anim.SetInteger("combo", comboCount);
            anim.SetTrigger("Attack"); // Trigger ensures Any State fires IMMEDIATELY

        }


    }
    void ResetCombo()
    {
        comboCount = 0;
        anim.SetInteger("combo", 0);
    }
}