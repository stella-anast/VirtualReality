using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class DragonEnemy : MonoBehaviour
{
    [SerializeField] float maxHealth = 10f;
    public Slider healthSlider;
    public float currentHealth;

    [Header("Combat")]
    [SerializeField] float attackCD = 30f;
    [SerializeField] float attackRange = 1f;
    [SerializeField] float aggroRange = 3f;

    [SerializeField] ParticleSystem flamethrowerFire;
    GameObject player;
    NavMeshAgent agent;
    Animator animator;
    float timePassed;
    float newDestinationCD = 0.5f;
    bool isAttacking = false;
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
        //check if dragon is attacking & the attack cooldown
        if (!isAttacking && timePassed >= attackCD)
        {
            if (Vector3.Distance(player.transform.position, transform.position) <= attackRange)
            {
                StartCoroutine(DelayedAttack());
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


    public void AttackAnimationEvent()
    {
        StartCoroutine(DelayedAttack());
    }

    IEnumerator DelayedAttack()
    {
        isAttacking = true;
        yield return new WaitForSeconds(5f);
        animator.SetTrigger("attack");
        isAttacking = false; 
    }
    //Handle damage 
    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;
        UpdateHealthBar();

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            animator.SetTrigger("death");
        }
    }
    void UpdateHealthBar()
    {
        if (healthSlider != null)
        {
            healthSlider.value = currentHealth / maxHealth;
        }
    }
    //Start the fire attack
    public void startFire()
    {
        if (flamethrowerFire != null)
        {
            flamethrowerFire.Play();
        }
    }
    //Stop the fire attack
    public void stopFire()
    {
        if (flamethrowerFire != null)
        {
            flamethrowerFire.Stop();
        }
    }

    public void StartDealDamage()
    {
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
    public void PlayerDied()
    {
        playerIsAlive = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, aggroRange);
    }
}
