using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    public UnityEvent<int, Vector2> damageableHit;

    Animator animator;
    GameOverScript gameOver;
    [SerializeField] private int _MaxHealth = 100;

    public bool isPlayer = true;

    public int MaxHealth
    {
        get
        {
            return _MaxHealth;
        }
        set
        {
            _MaxHealth = value;
        }
    }

    [SerializeField] private int _Health = 100;

    public int Health
    {
        get
        {
            return _Health;
        }
        set
        {
            _Health = value;

            if(_Health <= 0)
            {
                isAlive = false;
            }
        }
    }

    [SerializeField] private bool _isAlive = true;
    [SerializeField] private bool _isInvicible = false;

    public bool isInvicible 
    {
        get
        {
            return _isInvicible;
        } set {
            _isInvicible = value;
        }
    }

    private float timeSinceHit = 0;
    public float InvicibilityTimer = 0.5f;

    public bool isAlive
    {
        get
        {
            return _isAlive;
        }
        set
        {
            _isAlive = value;
            animator.SetBool(AnimationStrings.isAlive, value);
            Debug.Log("IsAlive set " + value);

            if (_isAlive == false)
            {
                HandleDeath();
            }
        }
    }

    public bool LockVelocity
    {
        get
        {
            return animator.GetBool(AnimationStrings.lockVelocity);
        }
        set
        {
            animator.SetBool(AnimationStrings.lockVelocity, value);
        }
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        gameOver = FindObjectOfType<GameOverScript>();
    }

    private void Update()
    {
        if(isInvicible)
        {
            if(timeSinceHit > InvicibilityTimer)
            {
                isInvicible = false;
                timeSinceHit = 0;
            }

            timeSinceHit += Time.deltaTime;
        }
    }

    public bool Hit(int damage, Vector2 knockback)
    {
        if(isAlive && !isInvicible)
        {
            Health -= damage;
            isInvicible = true;

            animator.SetTrigger(AnimationStrings.hit);
            LockVelocity = true;
            damageableHit?.Invoke(damage, knockback);
            // CharacterEvents.characterDamaged.Invoke(gameObject, damage);

            return true;
        }

        return false;
    }

     private void HandleDeath()
    {
        if (isPlayer && gameOver != null)
        {
           gameOver.GameOver();
        }
    }
}
