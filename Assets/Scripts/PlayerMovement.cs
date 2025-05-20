using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed = 2f;
    public float runSpeed = 4f;

    private Animator animator;
    private Rigidbody2D rb;
    private SpriteRenderer sr;

    private Vector2 moveInput;

    private PlayerEnergy energy;
    private PlayerExperience expSystem;
    private PlayerStamina stamina;


    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        energy = GetComponent<PlayerEnergy>();
        expSystem = GetComponent<PlayerExperience>();
        stamina = GetComponent<PlayerStamina>();
    }

    void Update()
    {
        // Read WASD input
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        // Flip sprite only on horizontal movement
        if (moveInput.x != 0)
            sr.flipX = moveInput.x < 0;

        // Determine running status
        bool wantsToRun = Input.GetKey(KeyCode.LeftShift);
        bool canRun = wantsToRun && stamina.CanRun();

        // Tell stamina system if we're running
        if (canRun && moveInput.magnitude > 0)
        {
            stamina.StartRunning();
        }
        else
        {
            stamina.StopRunning();
        }

        // Determine speed and set animation
        float currentSpeed = canRun ? runSpeed : walkSpeed;
        float animSpeed = moveInput.magnitude > 0 ? currentSpeed : 0f;
        animator.SetFloat("Speed", animSpeed);

        // Attack input with level + energy checks
        if (Input.GetKeyDown(KeyCode.C) && expSystem.CanUseAttack(1) && energy.TryUseEnergy(energy.attack1Cost))
            animator.SetTrigger("Attack1");

        if (Input.GetKeyDown(KeyCode.E) && expSystem.CanUseAttack(2) && energy.TryUseEnergy(energy.attack2Cost))
            animator.SetTrigger("Attack2");

        if (Input.GetKeyDown(KeyCode.Q) && expSystem.CanUseAttack(3) && energy.TryUseEnergy(energy.attack3Cost))
            animator.SetTrigger("Attack3");

        if (Input.GetKeyDown(KeyCode.R) && expSystem.CanUseAttack(4) && energy.TryUseEnergy(energy.specialCost))
            animator.SetTrigger("Special");
    }

    void FixedUpdate()
    {
        bool canRun = Input.GetKey(KeyCode.LeftShift) && stamina.CanRun();
        if (canRun && moveInput.magnitude > 0)
        {
            if (!stamina.DrainRunStamina())
            {
                stamina.StopRunning();
                canRun = false;
            }
        }

        float speed = canRun ? runSpeed : walkSpeed;
        rb.linearVelocity = moveInput.normalized * speed;
    }
}
