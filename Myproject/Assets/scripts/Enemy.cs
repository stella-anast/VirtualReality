using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour

{
    [SerializeField] float health;

    GameObject player;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        animator = GetComponent<Animator>();
    }
    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
        animator.SetTrigger("damage");
        if(health<=0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(this.gameObject);
    }
}
