using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEffect : ProjectileEffect
{
    [SerializeField] private float armorPenetration;
    [SerializeField] private float damage;
    
    public override void HotHitEffect(Collision collision)
    {
        Armor armor = collision.gameObject.GetComponent<Armor>();
        Health health = collision.gameObject.GetComponent<Health>();

        
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
