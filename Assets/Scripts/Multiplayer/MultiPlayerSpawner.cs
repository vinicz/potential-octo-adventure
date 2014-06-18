using UnityEngine;
using System.Collections;

public class MultiPlayerSpawner : PlayerSpawner {

	public override  GameObject createPlayerObject (GameObject newplayerAvatar)
	{
		return (GameObject) Network.Instantiate (newplayerAvatar ,transform.position, transform.rotation, 0);
	}

}
