using UnityEngine;
using System.Collections;

public class ShockWaveSkill : Skill
{

    public float shockDistance = 5f;
    public float shockWaveForce = 750f;

    public override void useSkill(GameObject targetObject)
    {
        if (cooldownTimer <= 0)
        {
                       
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, shockDistance);
            foreach (var shockHit in hitColliders)
            {
                
                if (shockHit && shockHit.rigidbody != null && shockHit.rigidbody != targetObject.rigidbody)
                {
                    shockHit.rigidbody.AddExplosionForce(shockWaveForce, transform.position, shockDistance, 3.0F);
                }
            }

            cooldownTimer = coolDown;
        }
    }
}
