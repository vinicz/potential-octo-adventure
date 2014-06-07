using UnityEngine;
using System.Collections;

public abstract class Skill : MonoBehaviour {

	public abstract string getSkillName();
	public abstract void useSkill();
	public abstract float getCoolDownRemaining(); 

}
