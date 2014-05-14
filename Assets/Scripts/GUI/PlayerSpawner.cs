using UnityEngine;
using System.Collections;

public class PlayerSpawner : MonoBehaviour {

    public GameObject playerAvatar;


    void Start()
    {
        spawnPlayer();
    }


	public void spawnPlayer()
    {
        GameObject playerObject = (GameObject) Instantiate(playerAvatar);
        playerObject.transform.parent = this.transform;
        playerObject.transform.position = this.transform.position;
    }
}
