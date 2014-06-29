using UnityEngine;
using System.Collections;

public abstract class Skill : MonoBehaviour {

    public float coolDown;
    
    protected float cooldownTimer = 0;

    protected virtual void Update () {
        if(cooldownTimer>0)
        {
            cooldownTimer -= Time.deltaTime;
        }
        
    }

	public abstract void useSkill(GameObject targetObject);

    public virtual float getCoolDownRemaining()
    {
        return cooldownTimer;
    }

}
