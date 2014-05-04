using UnityEngine;
using System.Collections;

public class SwitchOffLightOnTriggerEnter : MonoBehaviour {

	public GameObject light;

	void OnTriggerEnter(Collider other) {
		
		if (other.gameObject.tag == "Player") {
			light.SetActive(false);			
		}
	}

}
