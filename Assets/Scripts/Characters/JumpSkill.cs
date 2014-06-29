using UnityEngine;
using System.Collections;

public class JumpSkill : Skill {

	public float force = 1;


    public override void useSkill(GameObject targetObject)
	{
		if(cooldownTimer<=0)
		{
            targetObject.rigidbody.AddForce(Vector3.up*force);
			cooldownTimer = coolDown;
		}
	}
  

}
