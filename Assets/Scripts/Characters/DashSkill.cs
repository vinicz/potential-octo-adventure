using UnityEngine;
using System.Collections;

public class DashSkill : Skill {

	public float coolDown;
	public float force = 1;
	
	public static string DASH_SKILL_NAME = "dash_skill";
	
	private float cooldownTimer = 0;
	
	
	void Update () {
		if(cooldownTimer>0)
		{
			cooldownTimer -= Time.deltaTime;
		}
		
	}
	
	public override void useSkill()
	{
		if(cooldownTimer<=0)
		{
			Vector3 dashDirection = rigidbody.velocity.normalized;

			rigidbody.AddForce(dashDirection*force);
			cooldownTimer = coolDown;
		}
	}
	
	
	public override float getCoolDownRemaining()
	{
		return cooldownTimer;
	}
	
	public override string getSkillName()
	{
		return DASH_SKILL_NAME;
	}

}
