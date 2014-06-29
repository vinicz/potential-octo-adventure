using UnityEngine;
using System.Collections;

public class DashSkill : Skill {

	public float coolDown;
	public float force = 1;
	
	private float cooldownTimer = 0;
	
	
	void Update () {
		if(cooldownTimer>0)
		{
			cooldownTimer -= Time.deltaTime;
		}
		
	}
	
    public override void useSkill(GameObject targetObject)
	{
		if(cooldownTimer<=0)
		{
            Vector3 dashDirection = targetObject.rigidbody.velocity.normalized;

            targetObject.rigidbody.AddForce(dashDirection*force);

            particleSystem.transform.rotation = Quaternion.FromToRotation( particleSystem.gameObject.transform.forward,dashDirection) *particleSystem.gameObject.transform.rotation;

			cooldownTimer = coolDown;
		}
	}
	
	
	public override float getCoolDownRemaining()
	{
		return cooldownTimer;
	}
	
}
