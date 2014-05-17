using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraTransparencyController : MonoBehaviour
{


    public List<PlayerSpawner> playerSpwanerList;
    public float sphereRadius = 0.5f;

    void Update()
    {
        foreach (PlayerSpawner playerSpawner in playerSpwanerList)
        {
            GameObject playerObject = playerSpawner.getPlayerObject();
            Vector3 playerPosition = playerObject.transform.position;
            Vector3 playerDirection = playerObject.transform.position - this.transform.position;
            float playerDistance = playerDirection.magnitude;
            float sphereOffset = 0;
          


            SphereCollider sphereCollider = playerObject.GetComponent<SphereCollider>();
            if (sphereCollider != null)
            {
                sphereOffset = sphereCollider.radius*2;
            }
           

            RaycastHit[] hits;
            Vector3 top = playerPosition;
            Vector3 bottom = this.transform.position;
           
            hits = Physics.SphereCastAll(new Ray(bottom, playerDirection), sphereRadius, playerDistance - sphereRadius / 2f-sphereOffset);
           


            foreach (RaycastHit hit in hits)
            {
                TransparencyController transparencyController = hit.transform.gameObject.GetComponent<TransparencyController>();
                if (transparencyController != null)
                {
                    transparencyController.makeTransparent();
                }

            }

        }

    }

}
