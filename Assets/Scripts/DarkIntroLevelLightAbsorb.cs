using UnityEngine;
using System.Collections;

public class DarkIntroLevelLightAbsorb : MonoBehaviour {
	
	public GameObject lightToDecrease;
	public float decreaseAmount = 0f;
	public GameObject lightCone;
	public GameObject playerLightHandler;

	void OnTriggerEnter(Collider other) {
		
		if (other.gameObject.tag == "Player") {
			lightToDecrease.light.intensity -= decreaseAmount;
			lightCone.SetActive(false);

			playerLightHandler.GetComponent<DarkIntroLevelPlayerLightHandler>().increasePlayerLight();
		}
	}
}
