using UnityEngine;
using System.Collections;

public class DarkIntroLevelPlayerLightHandler : MonoBehaviour {

	private GameObject playerObject;
	private float targetPlayerLightIntensity;

	public float lightIncreaseCount = 3f;
	
	void Start () {
		playerObject = GameServiceLayer.serviceLayer.playerSpawnerList [0].getPlayerObject ();
		targetPlayerLightIntensity = playerObject.light.intensity;
		playerObject.light.intensity = 0.0f;
	}
	
	public void increasePlayerLight() {
		playerObject.light.intensity += targetPlayerLightIntensity / lightIncreaseCount;
	}
}
