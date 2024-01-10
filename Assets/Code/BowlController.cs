using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class BowlController : MonoBehaviour
{
    public static BowlController instance;

    public Camera mainCamera;
    public LineRenderer _lineRenderer;
    public DistanceJoint2D _distanceJoint;
    public Rigidbody2D _rb;
    public TMP_Text coinsUI;
    public TMP_Text countDownUI;

    public float InitialSpeed = 10f;
    public float jumpForce = 5f;
    public float playerHeight = 1.5f;
    public int maxJumps;
    public int maxDashes;
    public float dashForce = 5f;
    Animator animator;

    // outlets for countdown
    private float movementThreshold = 0.05f;
    public float countdownSize;
    // give players a grace period before showing countdown timer.
    public float countdownGracePeriod;


    // State Tracking
    public int coinCount;
    public int jumpsLeft;
    public int dashesLeft;
    private bool isPlayerGrounded;
    private bool isMoving = true;
    public static bool isInDangerZone = false;
    public bool canDoubleJump = false;
    public bool canDash = false;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        _distanceJoint.enabled = false;
        _rb = GetComponent<Rigidbody2D>();
        _rb.velocity = Vector2.right * InitialSpeed;
        animator = GetComponent<Animator>();

        // start the coroutine to check if bowl is static
        StartCoroutine(checkStaticState());

        // update in air animation parameter
        animator.SetBool("IsoffGround", false);
    }

    // Update is called once per frame
    void Update()
    {
        coinsUI.text = coinCount.ToString();

        if (Input.GetKeyDown(KeyCode.Mouse0) && !isPlayerGrounded)
        {
            Vector2 mousePos =
                (Vector2)mainCamera.ScreenToWorldPoint(Input.mousePosition);

            if (isGrappleOnBlock(mousePos))
            {
                _lineRenderer.SetPosition(0, mousePos);
                _lineRenderer.SetPosition(1, transform.position);
                _distanceJoint.connectedAnchor =
                    (Vector2)mainCamera.ScreenToWorldPoint(Input.mousePosition);
                _distanceJoint.enabled = true;
                _lineRenderer.enabled = true;
            }
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            _distanceJoint.enabled = false;
            _lineRenderer.enabled = false;
        }

        if (Input.GetKeyDown(KeyCode.Space) && canPlayerJump())
        {
            jump();
        }

        if (Input.GetKeyDown(KeyCode.D) && canPlayerDash())
        {
            dash();
        }

        if (_distanceJoint.enabled)
        {
            _lineRenderer.SetPosition(1, transform.position);
        }

        Debug.DrawRay(transform.position, Vector2.down * playerHeight);

        // update groundedness state
        updateGrounded();
        updateIsMoving();

        // update animation for dash (hot)
        animator.SetBool("IsHot", canDash);
    }

    bool canPlayerJump()
    {
        // if on the ground, reset jumps
        if (isPlayerGrounded) return true;
        if (jumpsLeft > 1 && canDoubleJump) return true;
        return false;
    }

    bool canPlayerDash()
    {
        // if on the ground, reset dashes
        if (isPlayerGrounded) return false;
        if (dashesLeft > 1 && canDash) return true;
        return false;
    }

    void updateIsMoving()
    {
        isMoving = Mathf.Abs(_rb.velocity.x) > movementThreshold;
    }

    void updateGrounded()
    {
        isPlayerGrounded = Physics2D.Raycast(transform.position,
            Vector2.down,
            playerHeight,
            LayerMask.GetMask("Ground"));

        // Update the idle animation to in-air animation if 
        // necessary.
        if (animator.GetBool("IsoffGround") != !isPlayerGrounded)
        {
            animator.SetBool("IsoffGround", !isPlayerGrounded);
        }
        if (animator.GetBool("IsInDangerZone") != isInDangerZone)
        {
            animator.SetBool("IsInDangerZone", isInDangerZone);
        }

        // reset jumps and dashes if necessary
        if (isPlayerGrounded)
        {
            
            jumpsLeft = maxJumps;
            dashesLeft = maxDashes;
            // no longer in danger zone for animation
            isInDangerZone = false;
        }
    }

    bool isGrappleOnBlock(Vector2 mousePos)
    {
        return Physics2D.Raycast(transform.position,
        (mousePos - (Vector2)transform.position).normalized,
            Mathf.Infinity, LayerMask.GetMask("AttachableObject"));
    }

    void jump()
    {
        _rb.velocity = new Vector2(_rb.velocity.x, jumpForce);
        jumpsLeft--;
    }

    void dash()
    {
        // apply a force to the right
        _rb.velocity = new Vector2(1 * dashForce, _rb.velocity.y);
        dashesLeft--;
    }

    IEnumerator checkStaticState()
    {
        while (true)
        {
            if (!isMoving)
            {
                // If it's not moving, start measuring time
                float timer = 0f;

                float totalCountdownSize = countdownGracePeriod + countdownSize;
                bool shouldGameEnd = true;
                while (timer < totalCountdownSize)
                {
                    timer += Time.deltaTime;

                    if (timer > countdownGracePeriod)
                    {
                        int countdown = Mathf.CeilToInt(
                            totalCountdownSize - timer);
                        countDownUI.gameObject.SetActive(true);
                        countDownUI.text = countdown.ToString();
                    }
                    
                    yield return null; // Yield to the next frame

                    // if it starts moving again, stop counting down
                    if (isMoving)
                    {
                        shouldGameEnd = false;
                        countDownUI.gameObject.SetActive(false);
                        break;
                    }
                }

                // If the object has been static for over countdownSize seconds,
                // restart the scene
                if (shouldGameEnd) gameOver();
            }

            // Wait for the next frame before checking again
            countDownUI.gameObject.SetActive(false);
            yield return null;
        }
    }

    void gameOver()
    {
        SceneManager.LoadScene("GameOver");
    }
}