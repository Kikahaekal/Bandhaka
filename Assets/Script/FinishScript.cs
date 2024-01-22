using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishScript : MonoBehaviour
{
    public GameObject endGameUi;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            string SceneName = SceneManager.GetActiveScene().name;

            switch (SceneName)
            {
                case "Level1":
                    if (AreAllEnemiesDestroyed())
                    {
                        SceneManager.LoadScene("Level2");
                    }
                break;
                case "Level2":
                    if (AreAllEnemiesDestroyed())
                    {
                        endGameUi.SetActive(true);
                    }
                break;

            }
        }
    }

    // Fungsi untuk memeriksa apakah tidak ada objek dengan tag "Enemy" di dalam scene
    private bool AreAllEnemiesDestroyed()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        return enemies.Length == 0;
    }
}
