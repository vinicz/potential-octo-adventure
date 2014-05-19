using UnityEngine;
using System.Collections;

public class DiamondActivator : MonoBehaviour {
	
	public void ActivateDiamond() {
	}

	public void DeactivateDiamond() {
		renderer.enabled = false;
		collider.enabled = false;
		((Behaviour)this.GetComponent("Halo")).enabled = false;
		particleSystem.Play ();
		audio.Play ();
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
