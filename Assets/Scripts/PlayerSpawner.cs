using UnityEngine;
using System.Collections;

public class PlayerSpawner : MonoBehaviour
{
    public bool spawnOnStart = true;
	public bool spawnWithLight = false;

    private GameObject playerAvatar;
    private GameObject playerObject;

    void Start()
    {
        GameServiceLayer.serviceLayer.playerSpawnerList.Add(this);

        if (spawnOnStart)
        {
            spawnPlayer();
        }
    }

    public void spawnPlayer()
    {
        GameObject newplayerAvatar = getSelectedPlayerAvatarObject();

        if (playerObject == null || newplayerAvatar != playerAvatar)
        {
            playerObject = createPlayerObject(newplayerAvatar);
            playerAvatar = newplayerAvatar;
        }

        playerObject.transform.parent = this.transform;
        playerObject.transform.position = this.transform.position;
        playerObject.rigidbody.velocity = Vector3.zero;
        playerObject.rigidbody.angularVelocity = Vector3.zero;

		addLightToPlayerIfNeeded ();

        playerObject.SetActive(true);
    }

    public virtual  GameObject createPlayerObject(GameObject newplayerAvatar)
    {
        return (GameObject)Instantiate(newplayerAvatar);
    }

    public virtual  GameObject getSelectedPlayerAvatarObject()
    {
        return (GameObject)GameServiceLayer.serviceLayer.characterService.getSelectedPlayerCharacter().playerCharacter;
    }

    public GameObject getPlayerObject()
    {
        return playerObject;
    }

    public virtual void OnDestroy()
    {
        GameServiceLayer.serviceLayer.playerSpawnerList.Remove(this);
    }

	private void addLightToPlayerIfNeeded()
	{
		if (spawnWithLight)
		{
			playerObject.light.enabled = true;
		}
	}
}
