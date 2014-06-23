using UnityEngine;
using System.Collections;

public class JumpSkill : Skill {

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
            targetObject.rigidbody.AddForce(Vector3.up*force);
			cooldownTimer = coolDown;
		}
	}


	public override float getCoolDownRemaining()
	{
		return cooldownTimer;
	}



}
