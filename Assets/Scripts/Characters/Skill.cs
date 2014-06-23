using UnityEngine;
using System.Collections;

public abstract class Skill : MonoBehaviour {

	public abstract void useSkill(GameObject targetObject);
	public abstract float getCoolDownRemaining(); 

}
