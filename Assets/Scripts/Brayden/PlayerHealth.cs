using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int maxHealth;
    private int currentHealth;

    [SerializeField] private TMP_Text healthText;


    void Start()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if (healthText)
        {
            healthText.text = ($"Health: {currentHealth}");
        }
    }

    private void OnEnable()
    {
        Actions.OnPlayerAttacked += TakeDamage;
    }

    private void OnDisable()
    {
        Actions.OnPlayerAttacked -= TakeDamage;
    }

    private void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Player Died");
        Actions.OnPlayerDied?.Invoke();
    }
}
