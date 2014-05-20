using UnityEngine;
using System.Collections;

public class SmokeStormIncrease : MonoBehaviour {
	
	public float targetAlpha = 255;
	public float time = 100;

	private Material material;
	private Color tintColor;
	private float step;
	
	void Start () {
		material = GetComponent<ParticleSystem> ().renderer.material;
		tintColor = material.GetColor("_TintColor");

		step = (targetAlpha / 255 - tintColor.a) / time;
	}
	
	void Update () {
		if (GameServiceLayer.serviceLayer.gameMaster.getGameState () == GameHandlerScript.GameState.GAME) {
			tintColor.a += step * Time.deltaTime;
			if(tintColor.a > 1.0)
				tintColor.a = 1.0f;
			material.SetColor("_TintColor", tintColor);
		}
	}
}
