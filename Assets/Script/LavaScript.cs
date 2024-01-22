using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Damageable))]
public class LavaScript : MonoBehaviour
{
    // Tidak perlu menambahkan damageAmount di sini
    Damageable playerHealth;

    private void Awake()
    {
        playerHealth = GetComponent<Damageable>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Hanya sebagai contoh, Anda dapat menambahkan efek atau logika lain jika diperlukan
            Debug.Log("Player touched lava");
        }
    }
}
