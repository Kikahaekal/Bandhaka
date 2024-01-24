using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
// using UnityEngine.UIElements;

public class Health : MonoBehaviour
{
    Damageable damageable;
    public TMP_Text healthBarText;
    // public Slider healthSlider;


    private void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        damageable = player.GetComponent<Damageable>();
    }


    // Start is called before the first frame update
    void Start()
    {
        // healthSlider.value = CalculatedSlidePercentage(damageable.Health, damageable.MaxHealth);
        healthBarText.text = "HP " + damageable.Health + "/" + damageable.MaxHealth;
    }

    private float CalculatedSlidePercentage(float health, float max_health)
    {
        return health / max_health;
    }

    private void OnEnable()
    {
        damageable.healthChanged.AddListener(OnPlayerHealthChanged);
    }

    private void OnDisable()
    {
        damageable.healthChanged.RemoveListener(OnPlayerHealthChanged);
    }


    private void OnPlayerHealthChanged(int Health, int MaxHealth)
    {
        // healthSlider.value = CalculatedSlidePercentage(Health, MaxHealth);
        healthBarText.text = "HP " + Health + "/" + MaxHealth;
    }

}
