using UnityEngine;
using System.Collections;

public class SmokeStormIncrease : MonoBehaviour {

	public float targetEmission = 100;
	public float time = 100;

	private ParticleSystem particleSystem;
	private float step;

	void Start () {
		particleSystem = GetComponent<ParticleSystem> ();
		step = (targetEmission - particleSystem.emissionRate) / time;
	}

	void Update () {
		if(GameServiceLayer.serviceLayer.gameMaster.getGameState() == GameHandlerScript.GameState.GAME)
			particleSystem.emissionRate += step * Time.deltaTime;
	}
}
