using UnityEngine;
using System.Collections;

public class PlayerSpawner : MonoBehaviour {

    public GameObject playerAvatar;
    private GameObject playerObject;

    void Start()
    {
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
        playerObject.SetActive(true);
    }

    public GameObject getPlayerObject()
    {
        return playerObject;
    }
}
