using UnityEngine;
using System.Collections;

public class AccelometerController : MonoBehaviour {
	void Start()
	{
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
	}



	// Update is called once per frame
	void Update () {

		Vector3 mulitpliedAcceleration = Input.acceleration * 10;

		rigidbody.AddForce (mulitpliedAcceleration.x, mulitpliedAcceleration.z, mulitpliedAcceleration.y);

		if (Input.GetKeyDown(KeyCode.Escape))
			Application.Quit(); 


	}


	void OnGUI()
	{
		GUI.Box(new Rect(10,10,500,30), Input.acceleration.x+" "+Input.acceleration.y+" "+Input.acceleration.z);

	}
}
