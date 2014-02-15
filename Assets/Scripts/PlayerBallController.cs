using UnityEngine;
using System.Collections;

public class PlayerBallController : MonoBehaviour
{

		public  GameHandlerScript gameHandler;


		// Use this for initialization
		void Start ()
		{


		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}

		void OnTriggerEnter (Collider otherCollider)
		{
				if (otherCollider.gameObject.tag == "Fire") {
						gameHandler.killOneBall (gameObject);

				}

				if (otherCollider.gameObject.tag == "Diamond") {
						gameHandler.collectOneDiamond (otherCollider.gameObject);
			
				}

		}
}
