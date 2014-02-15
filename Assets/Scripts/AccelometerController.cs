using UnityEngine;
using System.Collections;

public class AccelometerController : MonoBehaviour
{

		public bool printAccelometerInfo;


		// Update is called once per frame
		void Update ()
		{

				Vector3 mulitpliedAcceleration = Input.acceleration * 10;

				rigidbody.AddForce (mulitpliedAcceleration.x, mulitpliedAcceleration.z, mulitpliedAcceleration.y);
				

		}

		void OnGUI ()
		{
				if (printAccelometerInfo) {
						GUI.Box (new Rect (10, 10, 500, 30), Input.acceleration.x + " " + Input.acceleration.y + " " + Input.acceleration.z);
				}

		}
}
