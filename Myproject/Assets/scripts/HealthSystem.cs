using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] float maxHealth = 10;
    private float currentHealth;
    public Image healthBar;
    Animator animator;
    Enemy enemy;
    [SerializeField] GameObject defeatScreen;
    void Start()
    {
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
        enemy = FindObjectOfType<Enemy>();
       
    }

    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;
        animator.SetTrigger("Damage");

        // Update the health bar's fill amount
        healthBar.fillAmount = currentHealth / maxHealth;

        if (currentHealth <= 0)
        {
            animator.SetTrigger("Death");
            if (enemy != null)
            {
                enemy.PlayerDied(); 
            }
           
            
        }
    }
    public void Damage(float damageAmount)
    {
        currentHealth -= damageAmount;
        

        // Update the health bar's fill amount
        healthBar.fillAmount = currentHealth / maxHealth;

        if (currentHealth <= 0)
        {
            animator.SetTrigger("Death");
            if (enemy != null)
            {
                enemy.PlayerDied();
            }


        }
    }

    void Die()
    {
        DefeatScreen.Instance.startDefeatScreen();
        defeatScreen.SetActive(true);
    }
   
}
