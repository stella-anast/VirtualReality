using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    bool canDealDamage;
    List<GameObject> hasDealtDamage;

    [SerializeField] float weaponLength;
    [SerializeField] float weaponDamage;
    void Start()
    {
        canDealDamage = false;
        hasDealtDamage = new List<GameObject>();
    }
    //if damage dealer gets in contact with enemy or dragon enemy it causes damage
    void Update()
    {
        if (canDealDamage)
        {
            RaycastHit hit;

            int layerMask = 1 << 7;
            if (Physics.Raycast(transform.position, -transform.up, out hit, weaponLength, layerMask))
            {
                if (hit.transform.TryGetComponent(out Enemy enemy) && !hasDealtDamage.Contains(hit.transform.gameObject))
                {
                    enemy.TakeDamage(weaponDamage);
                    hasDealtDamage.Add(hit.transform.gameObject);
                }else if (hit.transform.TryGetComponent(out DragonEnemy dragonEnemy) && !hasDealtDamage.Contains(hit.transform.gameObject))
                {
                    dragonEnemy.TakeDamage(weaponDamage);
                    hasDealtDamage.Add(hit.transform.gameObject);
                }
            }
        }
    }
    public void StartDealDamage()
    {
        canDealDamage = true;
        hasDealtDamage.Clear();
    }
    public void EndDealDamage()
    {
        canDealDamage = false;
    }
    private void OnDrawGizmos()
    {
   
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position-transform.up * weaponLength);
    }

}
