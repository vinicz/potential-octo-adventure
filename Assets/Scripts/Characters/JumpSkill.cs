using UnityEngine;
using System.Collections;

public class JumpSkill : Skill {

	public float coolDown;
	public float force = 1;

	public static string JUMP_SKILL_NAME = "jump_skill";

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
			rigidbody.AddForce(Vector3.up*force);
			cooldownTimer = coolDown;
		}
	}


	public override float getCoolDownRemaining()
	{
		return cooldownTimer;
	}

	public override string getSkillName()
	{
		return JUMP_SKILL_NAME;
	}


}
