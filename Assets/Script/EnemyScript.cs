using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchDirection), typeof(Damageable))]
public class EnemyScript : MonoBehaviour
{
    public float walkSpeed = 3f;
    public float walkStopRate = 0.05f;
    Rigidbody2D rb;
    TouchDirection touchDirection;

    Animator animator;
    Damageable damageable;
    public DetectionZoneScript attackZone;
    public DetectionZoneScript cliffZone;
    public enum WalkableDirection { Right, Left };
    private WalkableDirection _enemyDirection;
    private Vector2 enemyDirectionVector = Vector2.right;

    public WalkableDirection EnemyDirection
    {
        get
        {
            return _enemyDirection;
        }
        set
        {
            if (_enemyDirection != value)
            {
                gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y);

                if (value == WalkableDirection.Right)
                {
                    enemyDirectionVector = Vector2.right;
                }
                else if (value == WalkableDirection.Left)
                {
                    enemyDirectionVector = Vector2.left;
                }

            }

            _enemyDirection = value;
        }
    }

    private bool _hasTarget = false;

    public bool hasTarget
    {
        get
        {
            return _hasTarget;
        }
        set
        {
            _hasTarget = value;
            animator.SetBool(AnimationStrings.hasTarget, value);
        }
    }

    public float attackCooldown
    {
        get
        {
            return animator.GetFloat(AnimationStrings.attackCooldown);
        }
        set
        {
            animator.SetFloat(AnimationStrings.attackCooldown, Mathf.Max(value, 0));
        }
    }

    public bool canMove
    {
        get
        {
            return animator.GetBool(AnimationStrings.canMove);
        }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        touchDirection = GetComponent<TouchDirection>();
        animator = GetComponent<Animator>();
        damageable = GetComponent<Damageable>();
    }

    // Update is called once per frame
    void Update()
    {
        hasTarget = attackZone.detectedColliders.Count > 0;

        if (attackCooldown > 0)
        {
            attackCooldown -= Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        if (touchDirection.isGrounded && touchDirection.isOnWall)
        {
            FlipDirection();
        }

        if (!damageable.LockVelocity)
        {
            if (canMove)
            {
                rb.velocity = new Vector2(walkSpeed * enemyDirectionVector.x, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, 0, walkStopRate), rb.velocity.y);
            }
        }
    }

    public void OnHit(int damage, Vector2 knockback)
    {
        rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);
    }

    private void FlipDirection()
    {
        if (EnemyDirection == WalkableDirection.Right)
        {
            EnemyDirection = WalkableDirection.Left;
        }
        else if (EnemyDirection == WalkableDirection.Left)
        {
            EnemyDirection = WalkableDirection.Right;
        }
    }

    public void OnCliffDetected()
    {
        if(touchDirection.isGrounded)
        {
            FlipDirection();
        }
    }
}
