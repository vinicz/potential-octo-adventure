using UnityEngine;
using System.Collections;

public class DiamondFirstBossMasterScript : DiamondMasterScript {

    public GameObject spawnItem;
    public GameObject spawnPosition;


    public override void collectOneDiamond(GameObject diamond)
    {

        base.collectOneDiamond(diamond);
        GameObject newDiamond = (GameObject)Instantiate(spawnItem);
        newDiamond.transform.position = spawnPosition.transform.position;

        if (isGameOver)
        {
            newDiamond.rigidbody.mass=1000;
        }
    }

	
}
