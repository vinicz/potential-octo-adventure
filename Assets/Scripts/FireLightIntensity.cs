using UnityEngine;
using System.Collections;

public class FireLightIntensity : MonoBehaviour {

    public float intensity = 1f;

	float rnd;
	bool burning = true;


	void Start()
	{
		rnd = Random.value * 100;
	}

	void Update()
	{		
		if (burning)
		{
            light.intensity = intensity * Mathf.PerlinNoise(rnd+Time.time,rnd+1+Time.time*1);		
        }
	}

	public void Extinguish()
	{
		burning = false;
		light.enabled = false;
	}
}
