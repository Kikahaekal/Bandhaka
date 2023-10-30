using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchDirection : MonoBehaviour
{
    public float groundDistance = 0.05f;
    RaycastHit2D[] groundHits = new RaycastHit2D[5];
    public ContactFilter2D castFilter;
    CapsuleCollider2D touchingCol;
    Animator animator;

    [SerializeField] private bool _isGrounded;

    public bool isGrounded
    {
        get
        {
            return _isGrounded;
        } set {
            _isGrounded = value;
            animator.SetBool(AnimationStrings.isGrounded, value);
        }
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        touchingCol = GetComponent<CapsuleCollider2D>();
    }

    void Start()
    {

    }

    void FixedUpdate()
    {
        isGrounded = touchingCol.Cast(Vector2.down, castFilter, groundHits, groundDistance) > 0;
    } 
}