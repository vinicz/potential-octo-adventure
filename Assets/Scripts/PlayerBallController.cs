using UnityEngine;
using System.Collections;

public class PlayerBallController : MonoBehaviour
{
 

    void OnTriggerEnter(Collider otherCollider)
	{
		if (otherCollider.gameObject.tag == "Fire")
		{
			GameServiceLayer.serviceLayer.gameMaster.killOneBall(gameObject);
		}

		if (otherCollider.gameObject.tag == "Spike")
		{
			GameServiceLayer.serviceLayer.gameMaster.killOneBall(gameObject);
		}

        if (otherCollider.gameObject.tag == "Diamond")
        {
            GameServiceLayer.serviceLayer.gameMaster.collectOneDiamond(otherCollider.gameObject);
            
        }

    }
}
