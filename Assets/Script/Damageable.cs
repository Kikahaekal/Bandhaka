using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    public UnityEvent<int, Vector2> damageableHit;
    public UnityEvent<int, int> healthChanged;

    Animator animator;
    GameOverScript gameOver;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private int _MaxHealth = 100;

    public bool isPlayer = true;
    public bool isEnemy = true;

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
            healthChanged?.Invoke(_Health, MaxHealth);

            if (_Health <= 0)
            {
                isAlive = false;
            }
        }
    }

    [SerializeField] private bool _isAlive = true;
    [SerializeField] private bool _isInvincible = false;

    public bool isInvincible
    {
        get
        {
            return _isInvincible;
        }
        set
        {
            _isInvincible = value;
        }
    }

    private float timeSinceHit = 0;
    public float InvincibilityTimer = 0.5f;

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
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (isInvincible)
        {
            if (timeSinceHit > InvincibilityTimer)
            {
                isInvincible = false;
                timeSinceHit = 0;
            }

            timeSinceHit += Time.deltaTime;
        }
    }

    public bool Hit(int damage, Vector2 knockback)
    {
        if (isAlive && !isInvincible)
        {
            Health -= damage;
            isInvincible = true;

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

        if (isEnemy)
        {
            StartCoroutine(FadeOut());
        }
    }

    private IEnumerator FadeOut()
    {
        float elapsedTime = 0f;
        float fadeTime = 0.1f; // You can adjust the time it takes to fade out

        while (elapsedTime < fadeTime)
        {
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeTime);
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, alpha);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Make sure the sprite is completely invisible
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0f);

        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Lava"))
        {
            // Handle collision with lava
            int lavaDamage = 100;
            Hit(lavaDamage, Vector2.zero);
        }
    }
}
