using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class HealthBar : MonoBehaviour
{
    Damageable damageable;
    public TMP_Text healthBarText;
    public Slider healthBarSlider;


    private void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        damageable = player.GetComponent<Damageable>();
    }


    // Start is called before the first frame update
    void Start()
    {
        healthBarSlider.value = CalculatedSlidePercentage(damageable.Health, damageable.MaxHealth);
        healthBarText.text = "Hp " + damageable.Health + "/" + damageable.MaxHealth;
    }

    private float CalculatedSlidePercentage(int health, int max_health)
    {
        return health / max_health;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
