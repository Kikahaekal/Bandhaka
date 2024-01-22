using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCount : MonoBehaviour
{
    public GameObject newSpritePrefab; // Prefab sprite baru yang ingin Anda munculkan
    public string enemyTag = "Enemy"; // Tag untuk sprite enemy

    void Update()
    {
        // Memeriksa apakah tidak ada sprite dengan tag "Enemy" yang tersisa
        if (GameObject.FindGameObjectsWithTag(enemyTag).Length == 0)
        {
            // Jika tidak ada sprite "Enemy", munculkan sprite baru
            SpawnNewSprite();
        }
    }

    void SpawnNewSprite()
    {
        // Membuat instance sprite baru dari prefab
        Instantiate(newSpritePrefab, transform.position, Quaternion.identity);
    }
}
