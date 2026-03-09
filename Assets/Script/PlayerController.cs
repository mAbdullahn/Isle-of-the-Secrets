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

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
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
        if(value.isPressed)
        {
            comboCount++;
            anim.SetInteger("combo", comboCount);
        }


    }
}