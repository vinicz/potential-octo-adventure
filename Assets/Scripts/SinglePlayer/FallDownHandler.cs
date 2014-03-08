using UnityEngine;
using System.Collections;

public class FallDownHandler : MonoBehaviour {

    public GameHandlerScript gameHandler;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    void OnTriggerEnter (Collider otherCollider)
    {
        if (otherCollider.gameObject.tag == "Player") {
            gameHandler.killOneBall (otherCollider.gameObject);
            
        }
        
        if (otherCollider.gameObject.tag == "Enemy") {
            gameHandler.killOneEnemy(otherCollider.gameObject);
            
        }
        
    }


}
