using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private LayerMask groundLayer;
    private Rigidbody rb;
    private PlayerSwing pS;
    private int swingPoints = 0;
    private int toSubtract = 0;
    private bool grounded = false, jumping = false;

    public void AddSwingPoint(int amount) { swingPoints += amount; }
    public void SubtractSwingPoint() { toSubtract--; }

    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        pS = this.GetComponent<PlayerSwing>();
        PlayerInputScript.MoveEvent += movementInput;
        PlayerInputScript.RunEvent += runInput;
        PlayerInputScript.JumpEvent += jumpInput;
    }

    void FixedUpdate()
    {
        move();
        handleFall();
    }

    void OnDestroy()
    {
        PlayerInputScript.MoveEvent -= movementInput;
        PlayerInputScript.RunEvent -= runInput;
        PlayerInputScript.JumpEvent -= jumpInput;
    }


    [Space(15), Header("Movement")]
    [SerializeField] private float speed;
    [SerializeField] private float force;
    [SerializeField] private float runMultiplier;
    private float multiplier = 1f;
    private Vector3 input = Vector3.zero;

    private void movementInput(Vector2 input)
    {
        this.input = input;
    }

    private void runInput(bool performed)
    {
        if (performed)
            multiplier = runMultiplier;
        else
            multiplier = 1f;
    }

    private void move()
    {
        Vector3 direction = transform.forward * input.y + transform.right * input.x;
        direction.Normalize();

        if (swingPoints <= 0)
            rb.velocity = new Vector3(direction.x * speed * multiplier, rb.velocity.y, direction.z * speed * multiplier);
        else
            rb.AddForce(direction * force * 10, ForceMode.Force);
    }


    [Space(15), Header("Jumping")]
    [SerializeField] private int extraJumps;
    [SerializeField] private float jumpForce;
    [SerializeField] private float fallMultiplier;
    [SerializeField] private float slowFallMultiplier;
    [SerializeField] private float velThreshold;
    private int jumps = 0;


    private void jumpInput(bool performed)
    {
        jumping = performed;
        if ((grounded || jumps < extraJumps) && jumping)
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
            jumps++;
        }
    }

    private void handleFall()
    {
        checkGround();
        if (swingPoints > 0)
            return;
        if (rb.velocity.y < velThreshold)
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime;
        else if (rb.velocity.y > velThreshold && !jumping)
            rb.velocity += Vector3.up * Physics.gravity.y * (slowFallMultiplier - 1) * Time.fixedDeltaTime;
    }

    private void checkGround()
    {
        grounded = Physics.Raycast(transform.position + Vector3.down * .9f, Vector3.down, .2f, groundLayer);
        if (grounded)
        {
            jumps = 0;
            if (toSubtract < 0)
            {
                AddSwingPoint(toSubtract);
                toSubtract = 0;
            }
        }
    }
}
