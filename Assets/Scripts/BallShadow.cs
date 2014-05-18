using UnityEngine;
using System.Collections;

public class BallShadow : MonoBehaviour {

	private Quaternion fixedRotation;

	void Start() {
		fixedRotation = transform.rotation;
		//fixedRotation = Quaternion.LookRotation (Vector3.down, Vector3.forward);
	}

	void LateUpdate() {
		transform.rotation = fixedRotation;
	}
	
	// Update is called once per frame
	void Update () {
		//transform.rotation = fixedRotation;	
	}
}
