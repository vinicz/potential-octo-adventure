using UnityEngine;
using System.Collections;

public class FallDownHandler : MonoBehaviour {

    public GameHandlerScript gameHandler;
    public bool killPlayer = true;
    public bool killEnemy = true;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    void OnTriggerEnter (Collider otherCollider)
    {
        if (otherCollider.gameObject.tag == "Player" && killPlayer) {
            gameHandler.killOneBall (otherCollider.gameObject);
            
        }
        
        if (otherCollider.gameObject.tag == "Enemy" && killEnemy) {

            ((DiamondMasterScript)gameHandler).killOneEnemy(otherCollider.gameObject);
            
        }
        
    }


}
