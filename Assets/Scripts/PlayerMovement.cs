using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed = 2f;
    public float runSpeed = 4f;

    private Animator animator;
    private Rigidbody2D rb;
    private SpriteRenderer sr;

    private Vector2 moveInput;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Read WASD input
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        // Flip sprite only on horizontal movement
        if (moveInput.x != 0)
            sr.flipX = moveInput.x < 0;

        // Normalize input to prevent speed boost when moving diagonally
        Vector2 normalizedInput = moveInput.normalized;

        // Determine current speed (Shift = run)
        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;

        // Set "Speed" param to 0 (idle) or walk/run (1 or 2, for example)
        float animSpeed = moveInput.magnitude > 0 ? currentSpeed : 0f;
        animator.SetFloat("Speed", animSpeed);

        // Attack triggers
        if (Input.GetKeyDown(KeyCode.Z)) animator.SetTrigger("Attack1");
        if (Input.GetKeyDown(KeyCode.X)) animator.SetTrigger("Attack2");
        if (Input.GetKeyDown(KeyCode.C)) animator.SetTrigger("Attack3");
        if (Input.GetKeyDown(KeyCode.V)) animator.SetTrigger("Special");
    }

    void FixedUpdate()
    {
        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;
        rb.linearVelocity = moveInput.normalized * currentSpeed;
    }
}
