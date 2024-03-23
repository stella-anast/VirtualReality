using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DragonEnemy : MonoBehaviour
{
    [SerializeField] float health;

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
    bool isAttacking = false; // Track if the dragon is currently attacking

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
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
        yield return new WaitForSeconds(Random.Range(5f, 10f));
        animator.SetTrigger("attack");
        isAttacking = false; 
    }

    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;

        if (health <= 0)
        {
            animator.SetTrigger("death");
        }
    }

    public void startFire()
    {
        if (flamethrowerFire != null)
        {
            flamethrowerFire.Play();
        }
    }

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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, aggroRange);
    }
}
