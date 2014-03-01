using UnityEngine;
using System.Collections;

public class BowlingBallController : MonoBehaviour {

    private float gravityAcceleration= -20f;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    
        Vector3 mulitpliedAcceleration = Input.acceleration * 10;
        
        rigidbody.AddForce (mulitpliedAcceleration.x, mulitpliedAcceleration.z, mulitpliedAcceleration.y);

        gravityAcceleration -= Time.deltaTime;
	}


   
}
