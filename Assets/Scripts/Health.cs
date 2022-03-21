using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable] public class HurtEvent : UnityEvent<int> {}
[System.Serializable] public class HealEvent : UnityEvent<int> {}


public class Health : MonoBehaviour
{
    [SerializeField] int maxHealth = 1;
    int currentHealth;

    [SerializeField] HurtEvent onHurt;
    [SerializeField] HealEvent onHeal;
    [SerializeField] UnityEvent onDeath;

    private void Awake() {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount) {
        currentHealth -= amount;
        if (currentHealth <= 0) {
            currentHealth = 0;
            onDeath.Invoke();
        } else {
            onHurt.Invoke(amount);
        }
    }

    public void Heal(int amount) {
        currentHealth += amount;
        if (amount > maxHealth) amount = maxHealth;
        onHeal.Invoke(amount);
    }
}
