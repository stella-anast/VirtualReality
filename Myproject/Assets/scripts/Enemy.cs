using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField] float maxHealth = 10f;
    [SerializeField] GameObject defeatScreen;

    public Slider healthSlider;
    public float currentHealth;

    [Header("Combat")]
    [SerializeField] float attackCD = 2f;
    [SerializeField] float attackRange = 1f;
    [SerializeField] float aggroRange = 3f;

    GameObject player;
    NavMeshAgent agent;
    Animator animator;
    float timePassed;
    float newDestinationCD = 0.5f;
    private float targetHealthValue;
    bool playerIsAlive = true;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    void Update()
    {
        
        if (!playerIsAlive)
            return;

        animator.SetFloat("speed", agent.velocity.magnitude / agent.speed);

        if (timePassed >= attackCD)
        {
            if (Vector3.Distance(player.transform.position, transform.position) <= attackRange)
            {
                animator.SetTrigger("attack");
                timePassed = 0;
            }
        }
        timePassed += Time.deltaTime;

        if (newDestinationCD <= 0 && Vector3.Distance(player.transform.position, transform.position) <= aggroRange)
        {
            newDestinationCD = 0.5f;
            agent.SetDestination(player.transform.position);
        }
        newDestinationCD -= Time.deltaTime;
        transform.LookAt(player.transform);
    }

    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;
        animator.SetTrigger("damage");
        UpdateHealthBar();

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            animator.SetTrigger("death");
            playerIsAlive = false; // Ensure playerIsAlive is set to false when the enemy dies
            defeatScreen.SetActive(true);
        }
       
    }


    void UpdateHealthBar()
    {
        if (healthSlider != null)
        {
            healthSlider.value = currentHealth / maxHealth;
        }
        else
        {
            Debug.LogWarning("Health Slider is not assigned to Enemy script.");
        }
    }

    public void StartDealDamage()
    {
        if (!playerIsAlive)
            return;

        GetComponentInChildren<EnemyDamageDealer>().StartDealDamage();
    }

    public void EndDealDamage()
    {
        GetComponentInChildren<EnemyDamageDealer>().EndDealDamage();
    }

    void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, aggroRange);
    }

    public void PlayerDied()
    {
        playerIsAlive = false;
    }
}
