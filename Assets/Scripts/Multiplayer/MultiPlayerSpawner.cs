using UnityEngine;
using System.Collections;

public class MultiPlayerSpawner : PlayerSpawner
{

    public override  GameObject createPlayerObject(GameObject newplayerAvatar)
    {
        Vector3 positionNoise = new Vector3(Random.Range(-5.0F, 5.0F), 0, Random.Range(-2.0F, 2.0F));

        GameObject newPlayerObject = (GameObject)Network.Instantiate(newplayerAvatar, transform.position+positionNoise, transform.rotation, 0);

        return newPlayerObject;
    }

    public override  GameObject getSelectedPlayerAvatarObject()
    {
        return (GameObject)GameServiceLayer.serviceLayer.characterService.getSelectedPlayerCharacter().multiPlayerCharacter;
    }

}
