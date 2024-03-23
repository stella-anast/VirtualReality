using UnityEngine;

public class FireDamage : MonoBehaviour
{
    [SerializeField] float damagePerSecond = 3f;
    ParticleSystem fireParticleSystem;

    void Start()
    {
        fireParticleSystem = GetComponent<ParticleSystem>();
    }
    //get damage from fire using mesh collider
    private void OnTriggerStay(Collider other)
    {
        
        HealthSystem healthSystem = other.GetComponent<HealthSystem>();
        if (healthSystem != null && fireParticleSystem.isPlaying)
        {
            
            healthSystem.Damage(damagePerSecond * Time.deltaTime);
        }
    }
}
