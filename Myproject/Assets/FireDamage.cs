using UnityEngine;

public class FireDamage : MonoBehaviour
{
    [SerializeField] float damagePerSecond = 3f;
    ParticleSystem fireParticleSystem;

    void Start()
    {
        fireParticleSystem = GetComponent<ParticleSystem>();
    }

    private void OnTriggerStay(Collider other)
    {
        // Check if the colliding object has a HealthSystem component and the fire particle system is playing
        HealthSystem healthSystem = other.GetComponent<HealthSystem>();
        if (healthSystem != null && fireParticleSystem.isPlaying)
        {
            // Apply damage per second to the colliding object
            healthSystem.Damage(damagePerSecond * Time.deltaTime);
        }
    }
}
