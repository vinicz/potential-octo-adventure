using UnityEngine;
using System.Collections;

public class TargetTransformAnimator : MonoBehaviour {

    public Transform finalTransform;
    public Transform followTarget;
    public float folllowTargetOffset = 5f;
    public float changeCameraTime=2f;
      
	
	// Update is called once per frame
	void Update () {
        if (changeCameraTime >= 0)
        {
            this.transform.position = new Vector3(this.transform.position.x,followTarget.transform.position.y+folllowTargetOffset,this.transform.position.z);
            this.transform.LookAt(followTarget.transform.position);

            changeCameraTime -= Time.deltaTime;

        } else
        {
            this.transform.position = Vector3.Lerp(this.transform.position, finalTransform.position, Time.deltaTime);
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, finalTransform.rotation, Time.deltaTime);
        }

      
	}
}
