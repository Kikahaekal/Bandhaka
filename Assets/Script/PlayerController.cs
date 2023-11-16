using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchDirection))]
public class PlayerController : MonoBehaviour
{

    public float WalkSpeed = 5f;
    public float RunSpeed = 8f;
    public float JumpPower = 6f;

    public float plungeSpeed = 20f;
    Vector2 MoveInput;
    Rigidbody2D rb;
    Animator animator;

    TouchDirection touchDirection;

    [SerializeField] private bool _isFacingRight = true;
    [SerializeField] private AudioSource PunchSoundEffect;
    [SerializeField] private AudioSource JumpSoundEffect;

    // private bool triggeredJump
    // {
    //     get
    //     {
    //         return animator.GetBool("jump");
    //     }
    // }

    private bool isAttacking 
    {
        get
        {
            return animator.GetBool(AnimationStrings.isAttacking);
        }
    }

    private bool isPlunging
    {
        get
        {
            return animator.GetBool(AnimationStrings.isPlunging);
        }
    }

    public bool isFacingRight 
    {
        get 
        {
            return _isFacingRight;
        } set
        {
            if(_isFacingRight != value)
            {
                transform.localScale *= new Vector2(-1, 1);
            }

            _isFacingRight = value;
        }
    }

    public bool canMove
    {
        get
        {
            return animator.GetBool(AnimationStrings.canMove);
        }
    }

    [SerializeField] private bool _isMoving = false;

    public bool isMoving
    {
        get
        {
            return _isMoving;
        } set {
            _isMoving = value;
            animator.SetBool(AnimationStrings.isMoving, value);
        }
    }

    [SerializeField] private bool _isRunning = false;

    public bool isRunning
    {
        get
        {
            return _isRunning;
        } 
        set
        {
            _isRunning = value;
            animator.SetBool(AnimationStrings.isRunning, value);
        }
    }

    public float CurrentSpeed
    {
        get
        {
            if(canMove)
            {
                if(isMoving)
                {
                    if(isRunning)
                    {
                        return RunSpeed;
                    } else {
                        return WalkSpeed;
                    }
                } else return 0;
            } else return 0;
        }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        touchDirection = GetComponent<TouchDirection>();
    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(MoveInput.x * CurrentSpeed, rb.velocity.y);

        animator.SetFloat(AnimationStrings.yVelocity, rb.velocity.y);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        MoveInput = context.ReadValue<Vector2>();

        isMoving = MoveInput != Vector2.zero;
        setFacingDirection(MoveInput);
    }

    private void setFacingDirection(Vector2 MoveInput)
    {
        if(MoveInput.x > 0 && !isFacingRight)
        {
            isFacingRight = true;
        } else if(MoveInput.x < 0 && isFacingRight) {
            isFacingRight = false;
        }
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        if(context.started && isMoving)
        {
            isRunning = true;
        } else if(context.canceled) {
            isRunning = false;    
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if(context.started && touchDirection.isGrounded)
        {
            JumpSoundEffect.Play();
            animator.SetTrigger(AnimationStrings.jump);
            rb.velocity = new Vector2(rb.velocity.y, JumpPower);
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if(context.started && !isAttacking)
        {
            animator.SetTrigger(AnimationStrings.attack);
            PunchSoundEffect.Play();
        }
    }

    public void OnPlunge(InputAction.CallbackContext context)
    {
        if(context.started && !touchDirection.isGrounded && !isPlunging)
        {
            StartCoroutine(PlungeSequence());
        }
    }

    private IEnumerator PlungeSequence()
    {
        animator.SetTrigger(AnimationStrings.plunge);
        PunchSoundEffect.Play();

        yield return new WaitForSeconds(0.1f);

        while(isPlunging)
        {
            transform.Translate(Vector2.down * Time.deltaTime * plungeSpeed);
            yield return null;
        }
    }
}
