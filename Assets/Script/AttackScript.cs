using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript : MonoBehaviour
{
    public int attackDamage = 10;
    public Vector2 knockback = Vector2.zero;

    void Start()
    {

    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable damageable = collision.GetComponent<Damageable>();

        if(damageable != null)
        {
            Vector2 knockbackRange = transform.parent.localScale.x > 0 ? knockback : new Vector2(-knockback.x, knockback.y);

            damageable.Hit(attackDamage, knockbackRange);
        }
    }
}
