using UnityEngine;
using System.Collections;

public class PlayerSpawner : MonoBehaviour {

    private GameObject playerAvatar;
    private GameObject playerObject;

    void Start()
    {
		GameServiceLayer.serviceLayer.playerSpawnerList.Add (this);

        spawnPlayer();
    }


	public void spawnPlayer()
    {
		GameObject newplayerAvatar = GameServiceLayer.serviceLayer.optionsService.getSelectedPlayerCharacter ().playerCharacter;

		if (playerObject == null || newplayerAvatar != playerAvatar)
        {
			playerObject = (GameObject)Instantiate(newplayerAvatar);
			playerAvatar = newplayerAvatar;
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
