using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEffect : InteractorEffect
{
    [SerializeField] private float armorPenetration;
    [SerializeField] private float damage;
    
    public override void HotHitEffect(Collision collision)
    {
        HotHitEffect(collision.collider);
    }

    public override void HotHitEffect(Collider other)
    {
        Armor armor = other.gameObject.GetComponent<Armor>();
        Health health = other.gameObject.GetComponent<Health>();

        
        float damageToHealth = damage * armorPenetration;
        float damageToArmor = damage * (1 - armorPenetration);

        if (armor != null && armor.getArmorPoint() > 0)
        {
            if (damageToArmor <= armor.getArmorPoint())
            {
                armor.InstantDamage(damageToArmor);
            }
            else
            {
                float remainingDamage = damageToArmor - armor.getArmorPoint();
                armor.InstantDamage(armor.getArmorPoint());
                health.InstantDamage(remainingDamage);
            }
            health.InstantDamage(damageToHealth);
        }
        else
        {
            health.InstantDamage(damage);
        }
    }
}
