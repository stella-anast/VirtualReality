using UnityEngine;

public class EquipmentSystem : MonoBehaviour
{
    [SerializeField] GameObject weaponHolder;
    [SerializeField] GameObject weapon;
    [SerializeField] GameObject weaponSheath;

    private CombatController combatController; // Reference to the CombatController script

    GameObject currentWeaponInHand;
    GameObject currentWeaponInSheath;

    void Start()
    {
        currentWeaponInSheath = Instantiate(weapon, weaponSheath.transform);
        combatController = GetComponent<CombatController>(); // Get reference to CombatController script
    }

    public void DrawWeapon()
    {
        currentWeaponInHand = Instantiate(weapon, weaponHolder.transform);
        Destroy(currentWeaponInSheath);
        combatController.OnDrawWeapon(); // Notify CombatController that the weapon is drawn
    }

    public void SheathWeapon()
    {
        currentWeaponInSheath = Instantiate(weapon, weaponSheath.transform);
        Destroy(currentWeaponInHand);
    }
    public void StartDealDamage()
    {
        currentWeaponInHand.GetComponentInChildren<DamageDealer>().StartDealDamage();
    }
    public void EndDealDamage()
    {
        currentWeaponInHand.GetComponentInChildren<DamageDealer>().EndDealDamage();
    }
}
