using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraTransparencyController : MonoBehaviour
{


    public List<PlayerSpawner> playerSpwanerList;
    public float capsuleSize = 0.5f;
    public float capsuleRadius = 0.5f;

    void Update()
    {
        foreach (PlayerSpawner playerSpawner in playerSpwanerList)
        {
            GameObject playerObject = playerSpawner.getPlayerObject();
            Vector3 playerPosition = playerObject.transform.position;
            Vector3 playerDirection = this.transform.position - playerObject.transform.position;
            float palyerDistance = playerDirection.magnitude;

            RaycastHit[] hits;
            Vector3 top = playerPosition + Vector3.up * capsuleSize;
            Vector3 bottom = playerPosition + Vector3.up * -capsuleSize;
            hits = Physics.CapsuleCastAll(top, bottom, capsuleRadius, playerDirection, palyerDistance);

            foreach (RaycastHit hit in hits)
            {
                TransparencyController transparencyController = hit.transform.gameObject.GetComponent<TransparencyController>();
                if(transparencyController!= null)
                {
                    transparencyController.makeTransparent();
                }

            }

        }

    }

}
