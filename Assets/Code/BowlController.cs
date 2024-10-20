using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class BowlController : MonoBehaviour
{
    public static bool is_debug_mode = true;
    public static BowlController instance;

    public Camera mainCamera;
    public LineRenderer _lineRenderer;
    public DistanceJoint2D _distanceJoint;
    public Rigidbody2D _rb;
    public TMP_Text coinsUI;
    public TMP_Text countDownUI;
    public TMP_Text levelDisplay;

    public float InitialSpeed = 10f;
    public float jumpForce = 5f;
    public float playerHeight = 0.7f;
    public float dashForce = 5f;
    Animator animator;

    // outlets for countdown
    private float movementThreshold = 0.05f;
    public float countdownSize;
    // give players a grace period before showing countdown timer.
    public float countdownGracePeriod;


    // State Tracking
    public int coinCount;
    public bool hasCollectedJumpToken;
    private bool isPlayerGrounded;
    private bool isMoving = true;
    public static bool isInDangerZone = false;
    public bool canDash = false;
    public bool isPaused = false;

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

        // display the level
        levelDisplay.text = "Level " + GameDataController.getLevel();
    }

    // Update is called once per frame
    void Update()
    {
        if (isPaused) {
            return;
        }
        // update groundedness state
        updateGrounded();
        updateIsMoving();

        coinsUI.text = coinCount.ToString();
        if (Input.touchCount > 0)
        {
            handleScreenTouch();
        }

        // if player hits ground while grappling, cut the grapple off.
        if (isGrounded() && _lineRenderer.enabled) {
            cutNoodle();
        }

        if (_distanceJoint.enabled)
        {
            _lineRenderer.SetPosition(1, transform.position);
        }

        // Debug.DrawRay(transform.position, Vector2.down * playerHeight);

        // check for pause
        if (Input.GetKeyDown(KeyCode.Escape)) {
            MenuController.instance.show();
        }
    }

    public void handleScreenTouch() {
        Touch touch = Input.GetTouch(0); // First touch
            
        // Convert touch position to world space
        Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
        
        float grappleThreshold = Screen.width * 1/3;

        // If the raycast didn't hit any UI elements, process gameplay input
        if (touch.position.x > grappleThreshold)
        {
            if (touch.phase == TouchPhase.Began && !isPlayerGrounded) {
                grappleOnClosestBlock();
                // while grappling, players should be able to cut noodle by jumping
            } 
            else if (touch.phase != TouchPhase.Began &&
                     touch.phase != TouchPhase.Ended &&
                     Input.touchCount > 1 &&
                     Input.GetTouch(1).position.x < grappleThreshold) {
                // while grappling (after grapple started and before it ended),
                // players should be able to jump.
                jumpIfPossible();
            }
            else if (touch.phase == TouchPhase.Ended) {
                cutNoodle();
            }
        } else {
            jumpIfPossible();
        }
    }

    // TODO: this function and canPlayerDash() are not being used.
    public void dashIfPossible() {
        if (canPlayerDash())
        {
            dash();
        }
    }

    public void jumpIfPossible() {
        if (canPlayerJump())
        {
            if (_distanceJoint.enabled) {
                jump(jumpForce * 1.2f);
                // cut off the noodle if player jumps off of it
                cutNoodle();
            } else {
                hasCollectedJumpToken = false;
                jump(jumpForce);
            }
        }
    }

    bool canPlayerJump()
    {
        // player can jump if on the ground, has a single use jump token or is grappling
        return isPlayerGrounded || hasCollectedJumpToken || _lineRenderer.enabled;
    }

    bool canPlayerDash()
    {
        // if on the ground, reset dashes
        if (isPlayerGrounded) return false;
        if (canDash) return true;
        return false;
    }

    void updateIsMoving()
    {
        isMoving = Mathf.Abs(_rb.velocity.x) > movementThreshold;
    }

    // Updates grounded status for animations
    void updateGrounded()
    {
        isPlayerGrounded = isGrounded();

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
            // no longer in danger zone for animation
            isInDangerZone = false;
        }
    }

    bool isGrounded() {
        return Physics2D.Raycast(transform.position,
            Vector2.down,
            playerHeight,
            LayerMask.GetMask("Ground"));
    }

    void grappleOnClosestBlock()
    {
        int rayCount = 15; // Number of rays in the cone
        float coneAngle = 110f; // Total angle of the cone
        float stepAngle = coneAngle / (float)(rayCount - 1);

        for (int i = 0; i < rayCount; i++)
        {
            // Calculate the current ray direction based on the iteration
            float angle = -coneAngle / 2f + i * stepAngle;
            Vector2 rayDirection = 
                Quaternion.Euler(0, 0, angle) * Vector2.right;

            RaycastHit2D[] hits = Physics2D.RaycastAll(
                transform.position, 
                rayDirection, 
                10f, 
                LayerMask.GetMask("AttachableObject"));

            GameObject closestAttachableObject = null;
            float closestDistance = Mathf.Infinity;

            foreach (RaycastHit2D hit in hits)
            {
                float distance = Vector2.Distance(
                    transform.position, 
                    hit.transform.position);

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestAttachableObject = hit.transform.gameObject;
                }
            }

            // Check if there is a hit and grapple on the object
            if (closestAttachableObject != null)
            {
                Vector2 anchorPoint = new Vector2(closestAttachableObject.transform.position.x, closestAttachableObject.transform.position.y);
                _lineRenderer.SetPosition(
                    0, anchorPoint);
                _lineRenderer.SetPosition(
                    1, transform.position);
                _distanceJoint.connectedAnchor = anchorPoint;
                _distanceJoint.enabled = true;
                _lineRenderer.enabled = true;
            }
        }
    }

    void jump(float force)
    {
        SoundEffectsManager.instance.PlayJumpSound();
        _rb.velocity = new Vector2(_rb.velocity.x, force);
    }

    public void dash()
    {
        // apply a force to the right
        _rb.velocity = new Vector2(1 * dashForce, _rb.velocity.y);
        canDash = false;
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

                        // being under countdown should also mean being in
                        // danger zone
                        isInDangerZone = true;

                    }
                    
                    yield return null; // Yield to the next frame

                    // if it starts moving again, stop counting down
                    if (isMoving)
                    {
                        shouldGameEnd = false;
                        countDownUI.gameObject.SetActive(false);

                        // should also remove danger zone animation
                        isInDangerZone = false;
                        break;
                    }
                }

                // If the object has been static for over countdownSize seconds
                // restart the scene
                if (shouldGameEnd) gameOver();
            }

            // Wait for the next frame before checking again
            countDownUI.gameObject.SetActive(false);
            yield return null;
        }
    }

    void cutNoodle() {
        _distanceJoint.enabled = false;
        _lineRenderer.enabled = false;
    }

    public void stopBowl() {
        _rb.velocity = Vector2.zero;
    }

    public void breakBowl() {
        animator.SetBool("IsBroken", true);
        SoundEffectsManager.instance.PlayBowlBreakSound();
        StartCoroutine(PlayAnimationThenChangeScene());
    }

    IEnumerator PlayAnimationThenChangeScene()
    {
        BowlController.instance.stopBowl();

        // Wait for animation to start
        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).IsName("Broken_Idle") &&
                                           animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0f);
        // Wait until the animation is complete
        yield return new WaitForSeconds(1f);

        // Wait for loss sound to end before changing scene to gameover.
        SoundEffectsManager.instance.PlayLossSound();
        yield return new WaitForSeconds(3f);

        BowlController.instance.gameOver();
    }

    public void gameOver()
    {
        // Update the global total coin count to show on the game over screen
        SceneManager.LoadScene("GameOver");
    }

    public float getDistance(Vector2 point1, Vector2 point2) {
        return (point1 - point2).magnitude;
    }
}