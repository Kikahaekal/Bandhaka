using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{

    public float WalkSpeed = 5f;
    public float RunSpeed = 8f;
    Vector2 MoveInput;
    Rigidbody2D rb;
    Animator animator;

    [SerializeField] private bool _isFacingRight = true;

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
            if(isMoving)
            {
                if(isRunning)
                {
                    return RunSpeed;
                } else {
                    return WalkSpeed;
                }
            } else return 0;
        }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
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
}
