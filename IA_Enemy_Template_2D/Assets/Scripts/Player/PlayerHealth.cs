using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{

    [SerializeField] private Image healthBar;
    [SerializeField] private float maxHealth;
    [SerializeField] private float currentHealth;
    
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.fillAmount = currentHealth / maxHealth;
    }

    
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            healthBar.fillAmount = currentHealth / maxHealth;
            // Lógica MUERTE ....
        }
        else
        {
            healthBar.fillAmount = currentHealth / maxHealth;
        }
    }
    
}
