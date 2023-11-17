using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchDirection : MonoBehaviour
{
    public ContactFilter2D castFilter;
    public float groundDistance = 0.05f;
    public float wallDistance = 0.2f;
    public float cellingDistance = 0.05f;
    CapsuleCollider2D touchingCol;
    RaycastHit2D[] groundHits = new RaycastHit2D[5];
    RaycastHit2D[] wallHits = new RaycastHit2D[5];
    RaycastHit2D[] cellingHits = new RaycastHit2D[5];
    Animator animator;

    private Vector2 wallCheckDirections => gameObject.transform.localScale.x > 0 ? Vector2.right : Vector2.left;


    [SerializeField] private bool _isGrounded;
    public bool isGrounded { get { return _isGrounded; } set 
        {
            _isGrounded = value;
            animator.SetBool(AnimationStrings.isGrounded, value);
        } 
    }

    private bool _isOnWall;

    public bool isOnWall { get { return _isOnWall; } set 
        {
            _isOnWall = value;
            animator.SetBool(AnimationStrings.isOnWall, value);
        } 
    }

    private bool _isOnCelling;

    public bool isOnCelling { get { return _isOnCelling; } set 
        {
            _isOnCelling = value;
            animator.SetBool(AnimationStrings.isOnCelling, value);
        } 
    }

    private void Awake()
    {
        touchingCol = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        isGrounded = touchingCol.Cast(Vector2.down, castFilter, groundHits, groundDistance) > 0;
        isOnWall = touchingCol.Cast(wallCheckDirections, castFilter, wallHits, wallDistance) > 0;
        isOnCelling = touchingCol.Cast(Vector2.up, castFilter, cellingHits, cellingDistance) > 0;
    }
}