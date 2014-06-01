using UnityEngine;
using System.Collections;

public class PlayerSpawner : MonoBehaviour {

    public GameObject playerAvatar;
    private GameObject playerObject;

    void Start()
    {
		GameServiceLayer.serviceLayer.playerSpawnerList.Add (this);

        spawnPlayer();
    }


	public void spawnPlayer()
    {
        if (playerObject == null)
        {
            playerObject = (GameObject)Instantiate(playerAvatar);
        }

        playerObject.transform.parent = this.transform;
        playerObject.transform.position = this.transform.position;
        playerObject.rigidbody.velocity = Vector3.zero;
        playerObject.rigidbody.angularVelocity = Vector3.zero;
        playerObject.SetActive(true);
    }

    public GameObject getPlayerObject()
    {
        return playerObject;
    }

	public void OnDestroy()
	{
		GameServiceLayer.serviceLayer.playerSpawnerList.Remove(this);
	}
}
