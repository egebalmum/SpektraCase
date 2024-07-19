using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEffect : InteractorEffect
{
    public float armorPenetration;
    public float damage;
    
    public override void HotHitEffect(Collision collision)
    {
        HotHitEffect(collision.collider);
    }

    public override void HotHitEffect(Collider other)
    {
        Armor armor = other.gameObject.GetComponent<Armor>();
        Health health = other.gameObject.GetComponent<Health>();

        if (health == null)
        {
            return;
        }

        if (armorPenetration > 1)
        {
            armorPenetration = 1;
        }
        float damageToHealth = damage * armorPenetration;
        float damageToArmor = damage * (1 - armorPenetration);

        if (armor != null && armor.GetArmorPoint() > 0)
        {
            if (damageToArmor <= armor.GetArmorPoint())
            {
                armor.ApplyDamageInstant(damageToArmor);
            }
            else
            {
                float remainingDamage = damageToArmor - armor.GetArmorPoint();
                armor.ApplyDamageInstant(armor.GetArmorPoint());
                health.ApplyDamageInstant(remainingDamage);
            }
            health.ApplyDamageInstant(damageToHealth);
        }
        else
        {
            health.ApplyDamageInstant(damage);
        }
    }
}
